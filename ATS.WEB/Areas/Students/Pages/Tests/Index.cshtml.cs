using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Students.Pages.Tests {
    public class TestModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TestModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Lesson> Lessons { get;set; }

        public async Task OnGetAsync()
        {
            Lessons = await _context.Lessons.ToListAsync();
        }
    }
}
