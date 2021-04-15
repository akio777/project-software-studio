using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabReservation.Data;
using LabReservation.Models;
using LabReservation.Services;
using Newtonsoft.Json;

namespace LabReservation.Controllers
{
	public class LabManageController : Controller
    {
        private readonly LabReservationContext _context;
        private readonly ILabService LAB;

        public LabManageController(LabReservationContext context, ILabService labservice)
        {
            _context = context;
            LAB = labservice;
        }

        // GET: Labinfo
        public async Task<IActionResult> Index()
        {
            var lab_info = LAB.LabManageInfo();
            return View(lab_info.Data);
        }

        public async Task<IActionResult> EditCancel(int id)
        {
            var labinfo = await _context.Labinfo.FirstOrDefaultAsync(m => m.id == id);
            var equipment = await _context.Equipment.FirstOrDefaultAsync(m => m.lab_id == id);
            var reservePageList = LAB.LabManage(id);

            var labManageInfoProps = new LabManageInfoProps(labinfo, equipment, reservePageList.Data);

            return View(labManageInfoProps);
        }

        // [HttpPost]
        public IActionResult Confirm(ReservedInput reservedInput)
        {
            var reservedList = new List<Reserved>();
            var mapReservedInput = new List<dynamic>();
            foreach (PropertyInfo propertyInfo in reservedInput.GetType().GetProperties())
            {
                mapReservedInput.Add(propertyInfo.GetValue(reservedInput, null));
            }

            for (var i = 0; i < mapReservedInput.Count(); i++)
            {
                for (var j = 0; j < mapReservedInput[i].Length; j++)
                {
                    if (mapReservedInput[i][j])
                    {
                        var reservedObject = new Reserved();
                        reservedObject.day = j;
                        reservedObject.time = i;
                        reservedObject.lab_id = 0;
                        reservedList.Add(reservedObject);
                    }
                }
            }

            Console.WriteLine(JsonConvert.SerializeObject(reservedList, Formatting.Indented));

            return RedirectToAction("Index", "Home");
        }

    }
}