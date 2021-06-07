using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Teacher.Pages.Answers {
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Input.QuestionId = id;
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            public int QuestionId { get; set; }
            public string AnswerText { get; set; }
            public bool IsRight { get; set; }

        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var answer = new Answer()
            {
                AnswerText = Input.AnswerText,
                IsRight = Input.IsRight,
                Question = await _context.Questions.FindAsync(Input.QuestionId)
            };

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            return RedirectToPage($"/Questions/Edit", new { id = Input.QuestionId });
        }
    }
}
