using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Teacher.Pages.Lessons
{
    public class IndexModel : PageModel
    {
        private readonly ATS.WEB.Data.ApplicationDbContext _context;

        public IndexModel(ATS.WEB.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Lesson> Lesson { get;set; }

        public async Task OnGetAsync()
        {
            var user = await _context.Users.FirstAsync(x => x.Email == User.Identity.Name);
            var teacher = await _context.Teachers.FirstAsync(x => x.User == user);
            Lesson = await _context.Lessons.Where(x => x.Teacher == teacher).ToListAsync();
        }
    }
}
