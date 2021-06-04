using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;
using System.IO;

namespace ATS.WEB.Areas.Teacher.Pages.Materials
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Material Material { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == id);

            if (Material == null)
            {
                return NotFound();
            }
            return Page();       
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Material = await _context.Materials.FindAsync(id);

            if (Material != null)
            {
                _context.Materials.Remove(Material);
                await _context.SaveChangesAsync();

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/materials", Material.FileName);

                System.IO.File.Delete(path);

            }


            return RedirectToPage("./Index");
        }
    }
}
