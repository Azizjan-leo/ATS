using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Teacher.Pages.Questions
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var question = await _context.Questions.FirstOrDefaultAsync(m => m.Id == Input.QuestionId);
                question.QuestionText = Input.QuestionText;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(Input.QuestionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage($"/Lessons/{Input.BackUrl}", new { id = Input.LessonId });
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
