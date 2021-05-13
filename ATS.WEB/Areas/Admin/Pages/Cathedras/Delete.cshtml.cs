using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ATS.WEB.Data.ApplicationDbContext _context;

        public DeleteModel(ATS.WEB.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cathedra Cathedra { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cathedra = await _context.Cathedras.FirstOrDefaultAsync(m => m.Id == id);

            if (Cathedra == null)
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

            Cathedra = await _context.Cathedras.FindAsync(id);

            if (Cathedra != null)
            {
                _context.Cathedras.Remove(Cathedra);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
