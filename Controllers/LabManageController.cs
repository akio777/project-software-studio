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
        public async Task<IActionResult> Confirm(string cancelReservedInput)
        {
            int lab_id = cancelReservedInput[0] - '0';
            int time = cancelReservedInput[1] - '0';
            int day = cancelReservedInput[2] - '0';
        
            var labinfo = await _context.Labinfo.FirstOrDefaultAsync(m => m.id == lab_id);
            var equipment = await _context.Equipment.FirstOrDefaultAsync(m => m.lab_id == lab_id);
            var reservePageList = LAB.LabManage(lab_id);

            // init Reserved for use with CancelList()
            var reservedObject = new Reserved();
            reservedObject.lab_id = lab_id;
            reservedObject.time = time;
            reservedObject.day = day;
            // Console.WriteLine(JsonConvert.SerializeObject(reservedObject, Formatting.Indented));

            var cancelUserList = LAB.CancelList(reservedObject);
            // Console.WriteLine(JsonConvert.SerializeObject(cancelUserList, Formatting.Indented));

            var labManageInfoProps = new LabManageInfoProps(labinfo, equipment, reservePageList.Data, true);
            labManageInfoProps.cancelUserList = cancelUserList.Data;

            //Console.WriteLine(JsonConvert.SerializeObject(cancelReservedInput, Formatting.Indented));

            return View("Views/LabManage/EditCancel.cshtml", labManageInfoProps);
        }
    }
}