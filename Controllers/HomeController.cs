using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VulnerableCoreApp.Models;

namespace VulnerableCoreApp.Controllers
{
    public class HomeController : Controller
        // Vulnerable: SQL Injection
        [HttpPost]
        public IActionResult SqlInjection(string username)
        {
            string query = "SELECT * FROM Users WHERE Username = '" + username + "'";
            // Simulate DB call
            ViewBag.Query = query;
            return View();
        }
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
