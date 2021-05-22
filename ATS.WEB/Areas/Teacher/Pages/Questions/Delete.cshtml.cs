using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Teacher.Pages.Questions
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel {
            public int LessonId { get; set; }
            public int QuestionId { get; set; }
            public string QuestionText { get; set; }
            public string BackUrl { get; set; }
        }


        public async Task<IActionResult> OnGetAsync(int id, int lessonId, string backUrl)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(m => m.Id == id);

            if (question is null) {
                return NotFound();
            }

            Input.QuestionId = question.Id;
            Input.QuestionText = question.QuestionText;
            Input.BackUrl = backUrl;
            Input.LessonId = lessonId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var question = await _context.Questions.FindAsync(Input.QuestionId);

            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage($"/Lessons/{Input.BackUrl}", new { id = Input.LessonId });
        }
    }
}
