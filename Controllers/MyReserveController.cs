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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LabReservation.Controllers
{
    [Authorize]
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
            Labinfo[] tempFilterList = _context.Labinfo.ToListAsync().Result.ToArray();
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
            myReserveProps.filterList = tempFilterList;
            return View(myReserveProps);
        }

        // [HttpPost]
        public IActionResult Confirm(ReservedInput reservedInput)
        {
            var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.Clone().FindFirst("Id").Value);
            var cancel_read = LAB.ReadCancel(userId);
            var cancelMyReservedInput = new CancelMyReservedInput();
            var tempCancelMapOutput = new CancelMapOutput(new CancelMap());
            Labinfo[] tempFilterList = _context.Labinfo.ToListAsync().Result.ToArray();

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

            //New Map

            List<CancelMap> cancelReservedModalList = new List<CancelMap>();

            int tIndex = 0;
            foreach (PropertyInfo p in reservedInput.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                for (int dIndex = 0; dIndex < 7; dIndex++)
                {
                    object anArray = p.GetValue(reservedInput, null);
                    IEnumerable<bool> enumerable = anArray as IEnumerable<bool>;
                    if (enumerable.ToArray()[dIndex])
                    {
                        foreach (dynamic data in cancel_read.Data)
                        {
                            if (data.time == tIndex && data.day == dIndex)
                            {
                                cancelReservedModalList.Add(data);
                            }
                        }
                    }

                }
                tIndex += 1;
            }
            // Console.WriteLine(JsonConvert.SerializeObject(cancelReservedModalList, Formatting.Indented));

            var myReserveProps = new MyReserveProps(cancelMyReservedInput);
            myReserveProps.modalOpen = true;
            myReserveProps.cancelReservedModalInput = cancelReservedModalList.ToArray();
            myReserveProps.filterList = tempFilterList;

            return View("Views/MyReserve/Index.cshtml", myReserveProps);
            // return RedirectToAction("Index", "Home");
        }

        // [HttpPost]
        public IActionResult ConfirmCancel(CancelMap[] cancelReservedModalInput)
        {
            Console.WriteLine(JsonConvert.SerializeObject(cancelReservedModalInput, Formatting.Indented));
            List<CancelReserved> reserveIdList = new List<CancelReserved>();
            for (int i = 0; i < cancelReservedModalInput.Length; i++)
            {
                reserveIdList.Add(new CancelReserved(cancelReservedModalInput[i].reserve_id));
            }

            LAB.Cancel(reserveIdList.ToArray());
            return RedirectToAction("Index", "MyReserve");
        }
    }
}