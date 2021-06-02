using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Teacher.Pages.Answers
{
    public class IndexModel : PageModel
    {
        private readonly ATS.WEB.Data.ApplicationDbContext _context;

        public IndexModel(ATS.WEB.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Answer> Answer { get;set; }

        public async Task OnGetAsync()
        {
            Answer = await _context.Answers.ToListAsync();
        }
    }
}
