using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LabReservation.Models;
using LabReservation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace LabReservation.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Index()
        {
            var json = await GetExternalLabs();
            // if(json is String){
            //     List<String> json = new List<String>();
            // }
            // Console.WriteLine(json);
            try
            {
                var exLabs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PartnerLab>>(json);
                ViewBag.ExternalLabs = exLabs;
            }
            catch (Exception exception)
            {
                // Console.WriteLine(exception);
                ViewBag.ExternalLabs = new List<PartnerLab>();
            }


            var token = Request.Headers["Cookie"];
            var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.Clone().FindFirst("Id").Value);
            var lab = LAB.LabInfo(userId);
            return View(lab.Data);
        }
        public IActionResult Test()
        {
            // var token = Request.Headers["Cookie"];
            // var temp = LAB.Read(1, 1);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task<string> GetExternalLabs()
        {
            string baseUrl = "https://software-studio-ce57.herokuapp.com/api/ExternalAPI";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();
                            if (data == null) return "[]";
                            return data;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                // Console.WriteLine(exception);
                return "[]";
            }
        }
    }
}