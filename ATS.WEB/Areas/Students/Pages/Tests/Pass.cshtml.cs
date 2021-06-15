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

        [BindProperty]
        public Lesson Lesson { get; set; }

        public Student Student { get; private set; }

        public bool DisbalePrev { get; private set; }

        public bool DisbaleNext { get; private set; }

        public async Task<IActionResult> OnGet(int id, int q)
        {
            Lesson = await _context.Lessons
                .Include(x => x.Questions).ThenInclude(x => x.Answers)
                .Include(x => x.Teacher)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (Lesson == null)
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
            var lastTests = await GetTestResult();
            if (lastTests == null)
            {
                return NotFound();
            }
            Lesson.Questions = Lesson.Questions.Where(qq => qq.Id == q).ToList();
            if (!Lesson.Questions.Any())
            {
                return NotFound();
            }
            Lesson.Questions.First().Answers = lastTests.Answers.Where(a => a.TestResultQuestionId == q).ToList() ;
            //Lesson.Questions = Lesson.Questions.Where(x => x.Answers.Any()).ToList();
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
            var lastTests = await _context.TestResults.Include(x=>x.Answers).FirstOrDefaultAsync(tr => tr.Answerer == Student && tr.Topic.Id == Lesson.Id);
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
            //Lesson = par;
            var lastTests = await GetTestResult();
            if (lastTests == null)
            {
                return NotFound();
            }
            Lesson = await _context.Lessons
                .Include(x => x.Questions)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (Lesson == null)
            {
                return NotFound();
            }
            int rightquestions = 0;
            foreach (var question in Lesson.Questions)
            {
                var personanswer = lastTests.Answers.Where(pa => pa.TestResultQuestionId == question.Id).ToList();
                var rightanswer = true;
                for (int i = 0; i < personanswer.Count(); i++)
                {
                    if (personanswer[i].IsRight != personanswer[i].RightStudent)
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
            lastTests.Score = 100 - (Lesson.Questions.Count / rightquestions);
            lastTests.PassDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToPage("./TestIntro", new { id });
        }

        public async Task<IActionResult> OnPostPrevAsync(int id, int q)
        {
            if (!ModelState.IsValid)
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
            var answers = lastTests.Answers.Where(a => a.TestResultQuestionId == q).ToList();
            if (!answers.Any())
            {
                return NotFound();
            }
            for (int i = 0; i < Lesson.Questions.First().Answers.Count(); i++)
            {
                answers[i].RightStudent = Lesson.Questions.First().Answers[i].RightStudent;
            }
            await _context.SaveChangesAsync();
            if (_Lesson.Questions.Select(x => x.Id).Any() && _Lesson.Questions.Select(x => x.Id).First() == q)
            {
                return RedirectToPage("./Pass", new { id, q });
            }
            var questionid = _Lesson.Questions.Select(x => x.Id).TakeWhile(n => n != q).Last();
            return RedirectToPage("./Pass", new { id, q = questionid });
        }

        public async Task<IActionResult> OnPostNextAsync(int id, int q)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Lesson.Questions.First().Answers.Count(a=>a.RightStudent)>1)
            {
                ModelState.AddModelError(string.Format("", q, 0), "¬ыбрано более одного правильного ответа");
                return Page();
            }
            if (Lesson.Questions.First().Answers.Count(a=>a.RightStudent)<1)
            {
                ModelState.AddModelError(string.Format("", q, 0), "Ќе выбрано ни одного правильного ответа");
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
            var answers = lastTests.Answers.Where(a => a.TestResultQuestionId == q).ToList();
            if (!answers.Any())
            {
                return NotFound();
            }
            for (int i = 0; i < Lesson.Questions.First().Answers.Count(); i++)
            {
                answers[i].RightStudent = Lesson.Questions.First().Answers[i].RightStudent;
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
