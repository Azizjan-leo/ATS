using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Teacher.Pages.Answers
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
            public int QuestionId { get; set; }
            public int AnswerId { get; set; }
            public string AnswerText { get; set; }
            public bool IsRight {get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id, int questionId)
        {

            var answer = await _context.Answers.FirstOrDefaultAsync(m => m.Id == id);

            if (answer is null)
            {
                return NotFound();
            }

            Input.AnswerId = answer.Id;
            Input.AnswerText = answer.AnswerText;
            Input.IsRight = answer.IsRight;
            Input.QuestionId = questionId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var answer = await _context.Answers.FindAsync(Input.AnswerId);

            if (answer != null)
            {
                _context.Answers.RemoveRange(_context.Answers.Where(a => a.TestResultQuestionId == Input.QuestionId && a.AnswerText == answer.AnswerText));
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage($"/Questions/Edit", new { id = Input.QuestionId });
        }
    }
}
