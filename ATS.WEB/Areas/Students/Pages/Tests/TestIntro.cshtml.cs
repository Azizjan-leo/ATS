using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ATS.WEB.Areas.Students.Pages.Tests
{
    public class TestIntroModel : PageModel
    {
        private readonly ATS.WEB.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public bool DisbaleRunTest { get; set; }
        public TestIntroModel(ATS.WEB.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public TestResult TestResult { get; set; }
        public Student Student { get; private set; }
        public Lesson Lesson { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (curentUser == null)
            {
                return NotFound();
            }
            Student = await _context.Students
                .Include(x => x.Cathedra)
                .FirstOrDefaultAsync(st => st.User == curentUser);

            if (this.Student == null)
            {
                return NotFound();
            }

            Lesson = await _context.Lessons
                .Include(x => x.Questions).ThenInclude(x => x.Answers)
                .Include(x => x.Teacher).ThenInclude(x => x.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (Lesson == null)
            {
                return NotFound();
            }
           
            var lastTest = await _context.TestResults
                .Include(x => x.Reviewer).ThenInclude(x => x.User)
                .FirstOrDefaultAsync(tr => tr.Answerer == Student && tr.Topic.Id == Lesson.Id);

            if (lastTest == null)
            {
                TestResult = new TestResult()
                {
                    Reviewer = Lesson.Teacher,
                    Answerer = Student,
                    Topic = Lesson,
                };
                return Page();
            }

            if (lastTest.Score.HasValue)
            {
                DisbaleRunTest = true;
            }
            TestResult = lastTest;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Lesson = par;
            var curentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (curentUser == null)
            {
                return NotFound();
            }
            Student = await _context.Students.Include(x => x.Cathedra).FirstOrDefaultAsync(st => st.User == curentUser);
            if (Student == null)
            {
                return NotFound();
            }
            Lesson = await _context.Lessons
                .Include(x => x.Questions).ThenInclude(x => x.Answers)
                .Include(x => x.Teacher)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            var lastTest = await _context.TestResults.FirstOrDefaultAsync(tr => tr.Answerer == Student && tr.Topic.Id == Lesson.Id);
            if (lastTest != null)
            {
                return RedirectToPage("./Pass", new { id = id.ToString(), q = Lesson.Questions.First().Id.ToString() });
            }

            var testResult = new TestResult()
            {
                Answerer = Student,
                Reviewer = Lesson.Teacher,
                PassDate = DateTime.Now,
                TopicId = Lesson.Id,
            };

            testResult.Answers = Lesson.Questions.SelectMany(
                q =>q.Answers.Select(a=>
                    new TestAnswer()
                    {
                        AnswerId = a.Id,
                        QuestionId = q.Id,
                        TestResult = testResult,
                        UserAnswer = false,
                    }
                ).ToList()
            ).ToList();
            _context.TestResults.Add(testResult);
            await _context.SaveChangesAsync();
            var q = testResult.Answers.First().Question.Id.ToString();
            return RedirectToPage("./Pass", new { id = id.ToString(), q });
        }

        public string GetTextScore(int? score) 
        {
            if (!score.HasValue)
            {
                return null;
            }
            if (score.Value > 87)
            {
                return "Отлично";
            }
            else if (score.Value > 73)
            {
                return "Хорошо";
            }
            else if (score.Value > 61) 
            {
                return "Удовлетворительно";
            }
            return "Неудовлетворительно";
        }
    }
}
