using HaftalıkRaporu.Models;
using HaftalıkRaporu.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HaftalıkRaporu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(SD.EmployeeUser))
            {
                return RedirectToAction("Index", "Empolyee");
            }
            else if(User.IsInRole(SD.AdminUser))
            {
                return RedirectToAction("GelenRaporlar", "Users");
            }
            else
            {
                
                return Redirect("/Identity/Account/Login");

            }

        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
