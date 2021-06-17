using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ATS.WEB.Areas.Students.Pages.Tests
{
    public class Index1Model : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Index1Model(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Lesson Lesson { get; set; }

        public string QuestionText { get; set; }

        [BindProperty]
        public IList<TestAnswer> TestAnswers { get; set; }

        public Student Student { get; private set; }

        public bool DisbalePrev { get; private set; }

        public bool DisbaleNext { get; private set; }

        public async Task<IActionResult> OnGet(int id, int q)
        {
            Lesson = await _context.Lessons
                .Include(x=>x.Questions)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (Lesson == null)
            {
                return NotFound();
            }
            var lastTest = await GetTestResult();
            if (lastTest == null)
            {
                return NotFound();
            }
            if (Lesson.Questions.First().Id == q) 
            {
                DisbalePrev = true;
            }
            if (Lesson.Questions.Last().Id == q) 
            {
                DisbaleNext = true;
            }

            TestAnswers = lastTest.Answers.Where(a => a.QuestionId == q).ToList();
            var question = Lesson.Questions.FirstOrDefault(_q => _q.Id == q);

            QuestionText = question == null?"Не удается загрузить текст вопроса": question.QuestionText;
            return Page();
        }


        async Task<TestResult> GetTestResult()
        {
            var curentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (curentUser == null)
            {
                return null;
            }
            this.Student = await _context.Students.Include(x => x.Cathedra).FirstOrDefaultAsync(st => st.User == curentUser);
            if (this.Student == null)
            {
                return null;
            }
            var lastTests = await _context.TestResults.Include(x=>x.Answers).ThenInclude(x=>x.Answer).FirstOrDefaultAsync(tr => tr.Answerer == Student && tr.Topic.Id == Lesson.Id);
            if (lastTests == null)
            {
                return null;
            }
            return lastTests;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Lesson = await _context.Lessons
                .Include(x => x.Questions).ThenInclude(x=>x.Answers)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            var lastTest = await GetTestResult();
            if (lastTest == null)
            {
                return NotFound();
            }
            if (Lesson == null || Lesson.Questions == null || !Lesson.Questions.Any())
            {
                return NotFound();
            }
            int rightquestions = 0;

            foreach (var group_answer in lastTest.Answers.GroupBy(a=>a.Question))
            {
                var rightanswer = true;
                foreach (var item in group_answer)
                {
                    if (item.UserAnswer != item.Answer.IsRight) 
                    { 
                        rightanswer = false;
                        break;
                    }
                }
                if (rightanswer)
                {
                    rightquestions += 1;
                }
            }
            lastTest.Score = 100 * ( rightquestions/Lesson.Questions.Count );
            lastTest.PassDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToPage("./TestIntro", new { id });
        }

        public async Task<IActionResult> OnPostPrevAsync(int id, int q)
        {
            if (!ModelState.IsValid || !CheckModelError())
            {
                return Page();
            }
            //Lesson = par;
            var lastTests = await GetTestResult();
            if (lastTests == null)
            {
                return NotFound();
            }
            var _Lesson = await _context.Lessons
                .Include(x => x.Questions)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (Lesson == null || !Lesson.Questions.Any())
            {
                return NotFound();
            }
            var answers = lastTests.Answers.Where(a => a.QuestionId == q).ToList();
            if (!answers.Any())
            {
                return NotFound();
            }

            for (int i = 0; i < TestAnswers.Count; i++)
            {
                var userAnswer = lastTests.Answers.FirstOrDefault(a=>a.AnswerId == TestAnswers[i].Id);
                if (userAnswer == null)
                {
                    continue;
                }
                TestAnswers[i].UserAnswer = userAnswer.UserAnswer;
            }
            await _context.SaveChangesAsync();
            if (_Lesson.Questions.Select(x => x.Id).Any() && _Lesson.Questions.Select(x => x.Id).First() == q)
            {
                return RedirectToPage("./Pass", new { id, q });
            }
            var questionid = _Lesson.Questions.Select(x => x.Id).TakeWhile(n => n != q).Last();
            return RedirectToPage("./Pass", new { id, q = questionid });
        }

        public bool CheckModelError() 
        {
            if (TestAnswers.Count(a=>a.UserAnswer)>1)
            {
                ModelState.AddModelError(string.Format(""), "Выбрано более одного ответа");
                return false;
            }
            if (TestAnswers.Count(a=>a.UserAnswer)<1)
            {
                ModelState.AddModelError(string.Format(""), "Не выбрано ни одного ответа");
                return false;
            }
            return true;
        }

        public async Task<IActionResult> OnPostNextAsync(int id, int q)
        {
            if (!ModelState.IsValid || !CheckModelError())
            {
                return Page();
            }
            //Lesson = par;
            var lastTests = await GetTestResult();
            if (lastTests == null)
            {
                return NotFound();
            }
            var _Lesson = await _context.Lessons
                .Include(x => x.Questions)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (Lesson == null || !Lesson.Questions.Any())
            {
                return NotFound();
            }
            var answers = lastTests.Answers.Where(a => a.QuestionId == q).ToList();
            if (!answers.Any())
            {
                return NotFound();
            }

            for (int i = 0; i < TestAnswers.Count; i++)
            {
                var userAnswer = lastTests.Answers.FirstOrDefault(a => a.AnswerId == TestAnswers[i].Id);
                if (userAnswer == null)
                {
                    continue;
                }
                TestAnswers[i].UserAnswer = userAnswer.UserAnswer;
            }

            await _context.SaveChangesAsync();
            if (_Lesson.Questions.Select(x => x.Id).Last() == q)
            {
                return RedirectToPage("./Pass", new { id, q });
            }
            var questionid = _Lesson.Questions.Select(x => x.Id).SkipWhile(n => n != q).Skip(1).First();
            return RedirectToPage("./Pass", new { id, q = questionid });
        }
    }
}
