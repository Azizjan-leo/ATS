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

namespace ATS.WEB.Pages.Feedback
{
    public class IndexModel : PageModel
    {
        private readonly ATS.WEB.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ATS.WEB.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<IGrouping<Cathedra, Teacher>> Teachers { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var AllTeachers = await _context.Teachers.Include(x => x.User).Include(x => x.Cathedra).ToListAsync();
            Teachers = AllTeachers.GroupBy(tch => tch.Cathedra).ToList();
            return Page();
        }
    }
}
