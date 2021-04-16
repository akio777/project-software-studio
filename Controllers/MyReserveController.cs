using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabReservation.Data;
using LabReservation.Models;
using LabReservation.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LabReservation.Controllers
{
    public class MyReserveController : Controller
    {
        private readonly LabReservationContext _context;
        private readonly ILabService LAB;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyReserveController(ILabService labservice, LabReservationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            LAB = labservice;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: 
        public async Task<IActionResult> Index()
        {
            var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.Clone().FindFirst("Id").Value);
            var cancel_read = LAB.ReadCancel(userId);
            var cancelMyReservedInput = new CancelMyReservedInput();
            var tempCancelMapOutput = new CancelMapOutput(new CancelMap());

            int t = 0;
            foreach (PropertyInfo p in cancelMyReservedInput.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                CancelMapOutput[] temptime = new CancelMapOutput[] {
                    tempCancelMapOutput, tempCancelMapOutput,
                    tempCancelMapOutput, tempCancelMapOutput,
                    tempCancelMapOutput, tempCancelMapOutput,
                    tempCancelMapOutput };
                for (int d = 0; d < 7; d++)
                {
                    foreach (dynamic data in cancel_read.Data)
                    {
                        if (d == data.day && t == data.time)
                        {
                            temptime[d] = new CancelMapOutput(data);
                        }
                    }

                }
                t += 1;
                p.SetValue(cancelMyReservedInput, temptime.ToArray());
            }

            var myReserveProps = new MyReserveProps(cancelMyReservedInput);
            return View(myReserveProps);
        }

        // [HttpPost]
        public IActionResult Confirm(ReservedInput reservedInput)
        {
            Console.WriteLine(JsonConvert.SerializeObject(reservedInput, Formatting.Indented));
            return RedirectToAction("Index", "Home");
        }
    }
}