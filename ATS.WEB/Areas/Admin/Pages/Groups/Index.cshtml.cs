using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ATS.WEB.Data;
using ATS.WEB.Data.Entities;

namespace ATS.WEB.Areas.Admin.Pages.Groups
{
    public class IndexModel : PageModel
    {
        private readonly ATS.WEB.Data.ApplicationDbContext _context;

        public IndexModel(ATS.WEB.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Group> Group { get;set; }

        public async Task OnGetAsync()
        {
            Group = await _context.Groups.ToListAsync();
        }
    }
}
