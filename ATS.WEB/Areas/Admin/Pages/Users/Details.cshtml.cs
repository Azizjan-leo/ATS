using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Admin.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly ATS.WEB.Data.ApplicationDbContext _context;

        public DetailsModel(ATS.WEB.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser2 ApplicationUser2 { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser2 = await _context.Users2.FirstOrDefaultAsync(m => m.Id == id);

            if (ApplicationUser2 == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
