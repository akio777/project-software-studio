using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LabReservation.Models;
using LabReservation.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace LabReservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILabService LAB;
        public HomeController(ILabService lab)
        {
            LAB = lab;
        }
        

        public IActionResult Index()
        {
            var token = Request.Headers["Cookie"];
            var temp = LAB.Read(1, 1);
            ViewData["data"] = temp.Data;
            return View();
        }
        public IActionResult Test()
        {
            // var token = Request.Headers["Cookie"];
            // var temp = LAB.Read(1, 1);
            
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}