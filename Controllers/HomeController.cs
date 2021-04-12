using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LabReservation.Models;
using LabReservation.Services;
using Microsoft.AspNetCore.Http;


namespace LabReservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILabService LAB;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ILabService lab, IHttpContextAccessor httpContextAccessor)
        {
            LAB = lab;
            _httpContextAccessor = httpContextAccessor;
        }
        

        public IActionResult Index()
        {
            var token = Request.Headers["Cookie"];
            
            // Console.WriteLine("LOG : "+User.Identity.Name);
            return View();
        }
        public IActionResult Test()
        {
            // var token = Request.Headers["Cookie"];
            var temp = LAB.Read();
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