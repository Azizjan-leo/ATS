using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ATS.WEB.Areas.Students.Pages.Tests
{
    public class Index1Model : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Index1Model(ApplicationDbContext context) {
            _context = context;
        }

        [BindProperty]
        public Lesson Lesson { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Lesson = await _context.Lessons
                .Include(x => x.Questions).ThenInclude(x => x.Answers)
                .Include(x => x.Teacher)
                .FirstOrDefaultAsync(x => x.Id == id);

            //Lesson.Questions = Lesson.Questions.Where(x => x.Answers.Any()).ToList();

            return Page();
        }
    }
}
