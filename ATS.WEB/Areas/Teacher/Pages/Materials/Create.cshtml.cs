using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ATS.WEB.Areas.Teacher.Pages.Materials
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public CreateModel(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var file = Path.Combine(_environment.ContentRootPath, "wwwroot/materials", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            var user = await _context.Users.FirstAsync(x => x.Email == User.Identity.Name);
            var teacher = await _context.Teachers.FirstAsync(x => x.User == user);
            Material material = new()
            {
                Name = Name,
                FileName = Path.GetFileName(file),
                Teacher = teacher,
                UploadDate = DateTime.UtcNow
            };
            



            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
