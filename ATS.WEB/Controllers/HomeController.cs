using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATS.WEB.Controllers {
    [Authorize]
    public class HomeController : Controller {
        public IActionResult Cabinet() {
            return View();
        }
    }
}
