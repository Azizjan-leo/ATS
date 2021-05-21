using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Teacher.Pages.Lessons
{
    public class DetailsModel : PageModel
    {
        private readonly ATS.WEB.Data.ApplicationDbContext _context;

        public DetailsModel(ATS.WEB.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Lesson Lesson { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lesson = await _context.Lessons.FirstOrDefaultAsync(m => m.Id == id);

            if (Lesson == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
