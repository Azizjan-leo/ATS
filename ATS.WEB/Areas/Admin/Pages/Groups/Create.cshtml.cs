using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ATS.WEB.Areas.Admin.Pages.Groups
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            await initializeDropDowns();
            return Page();
        }

        private async Task initializeDropDowns() {
            Cathedras = await _context.Cathedras.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToListAsync();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel {
            public string Name { get; set; }
            public int CathedraId { get; set; }
        }

        public List<SelectListItem> Cathedras { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                await initializeDropDowns();
                return Page();
            }
            var group = new Group() { Name = Input.Name };

            group.Cathedra = await _context.Cathedras.FindAsync(Input.CathedraId);
            if (group == null)
                return NotFound();
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


    }
}
