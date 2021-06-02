using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Teacher.Pages.Materials
{
    public class CreateModel : PageModel
    {
        private readonly ATS.WEB.Data.ApplicationDbContext _context;

        public CreateModel(ATS.WEB.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Topic { get; set; }
            public string Content { get; set; }

        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Materials.Add(Material);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
