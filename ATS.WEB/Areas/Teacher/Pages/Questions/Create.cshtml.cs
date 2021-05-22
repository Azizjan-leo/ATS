using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Teacher.Pages.Questions {
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id, string backUrl)
        {
            Input.LessonId = id;
            Input.BackUrl = backUrl;
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel {
            public int LessonId { get; set; }
            public string QuestionText { get; set; }
            public string BackUrl { get; set; }
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var question = new Question() {
                QuestionText = Input.QuestionText,
                Lesson = await _context.Lessons.FindAsync(Input.LessonId)
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return RedirectToPage($"/Lessons/{Input.BackUrl}", new { id = Input.LessonId });
        }
    }
}
