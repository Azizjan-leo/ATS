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
            this.Student = await _context.Students
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
            var lastTests = await _context.TestResults
                .Include(x => x.Reviewer).ThenInclude(x => x.User)
                .FirstOrDefaultAsync(tr => tr.Answerer == Student && tr.Topic.Id == Lesson.Id);
            if (lastTests == null)
            {
                TestResult = new TestResult()
                {
                    Reviewer = Lesson.Teacher,
                    Answerer = Student,
                    Topic = Lesson,
                };
                return Page();
            }
            if (lastTests.Score.HasValue)
            {
                DisbaleRunTest = true;
            }
            TestResult = lastTests;
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
            this.Student = await _context.Students.Include(x => x.Cathedra).FirstOrDefaultAsync(st => st.User == curentUser);
            if (this.Student == null)
            {
                return NotFound();
            }
            Lesson = await _context.Lessons
                .Include(x => x.Questions).ThenInclude(x => x.Answers)
                .Include(x => x.Teacher)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            var lastTests = await _context.TestResults.FirstOrDefaultAsync(tr => tr.Answerer == Student && tr.Topic.Id == Lesson.Id);
            if (lastTests != null)
            {
                return RedirectToPage("./Pass", new { id = id.ToString(), q = Lesson.Questions.First().Id.ToString() });
            }

            var testres = new TestResult();
            testres.Answerer = Student;
            testres.Reviewer = Lesson.Teacher;
            testres.PassDate = DateTime.Now;
            testres.Topic = await _context.Lessons.FirstAsync(l => l.Id == Lesson.Id);
            var ansvers = new List<Answer>();
            foreach (var question in Lesson.Questions)
            {
                foreach (var answer in question.Answers.Where(a => !a.TestResultId.HasValue))
                {
                    var _answer = new Answer()
                    {
                        TestResultQuestionId = question.Id,
                        AnswerText = answer.AnswerText,
                        IsRight = answer.IsRight,
                        RightStudent = false,
                    };
                    ansvers.Add(_answer);
                }
            }
            testres.Answers = ansvers;
            var q = Lesson.Questions.First().Id.ToString();
            _context.TestResults.Add(testres);
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync();
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
