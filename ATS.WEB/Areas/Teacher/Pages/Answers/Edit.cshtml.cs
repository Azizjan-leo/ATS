using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;
using System;
using System.Linq;

namespace ATS.WEB.Areas.Teacher.Pages.Answers {
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Answer Answer { get; set; }
        [BindProperty]
        public int QuestionId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int questionId)
        {
            Answer = await _context.Answers.FirstOrDefaultAsync(m => m.Id == id);

            if (Answer == null)
            {
                return NotFound();
            }
            QuestionId = questionId;

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
                Answer.QuestionId = QuestionId;
                _context.Attach(Answer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               throw;     
            }

            return RedirectToPage($"/Questions/Edit", new { id = QuestionId });
        }
    }
}
