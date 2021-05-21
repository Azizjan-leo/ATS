using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ATS.WEB.Areas.Teacher.Pages.Lessons
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Topic { get; set; }
            public string Content { get; set; }
            
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                
                return Page();
            }
            var user = await _context.Users.FirstAsync(x => x.Email == User.Identity.Name);
            var teacher = await _context.Teachers.FirstAsync(x => x.User == user);

            var lesson = new Lesson() 
            { 
                Topic = Input.Topic,
                Content = Input.Content,
                Teacher = teacher        
            };

         //   lesson.Teacher = await _context.Teachers.FindAsync(Input.TeacherId);
            if (lesson == null)
                return NotFound();
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
