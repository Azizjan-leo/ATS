using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ATS.WEB.Areas.Teacher.Pages.Contacts
{


    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Students { get; set; }


        public async Task OnGetAsync()
        {
            //todo : add a mechanism for teacher for a students
            Students = await _context.Students.Include(x => x.Cathedra).Include(x => x.User).Include(x=>x.Group).ToListAsync();
        }
    }
}
