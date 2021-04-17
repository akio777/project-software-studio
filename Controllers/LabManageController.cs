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
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace LabReservation.Controllers
{
    [Authorize(Roles = "0")]
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
            bool[] checkList = new bool[cancelUserList.Data.data.Length];
            //Console.WriteLine(JsonConvert.SerializeObject(cancelUserList.Data, Formatting.Indented));

            var labManageInfoProps = new LabManageInfoProps(labinfo, equipment, reservePageList.Data, true);
            labManageInfoProps.cancelUserList = cancelUserList.Data;
            labManageInfoProps.checkedList = checkList;

            labManageInfoProps.id = (lab_id * 100) + (time * 10) + (day);

            // Console.WriteLine(JsonConvert.SerializeObject(labManageInfoProps.labManageOutputProps.cancelUserList, Formatting.Indented));
            // Console.WriteLine(JsonConvert.SerializeObject(labManageInfoProps.labManageOutputProps.checkedList, Formatting.Indented));
           
            return View("Views/LabManage/EditCancel.cshtml", labManageInfoProps);
        }

        public IActionResult SubmitCancel(bool[] checkedList, int id)
        {
            // init Reserved for use with CancelList()
            var reservedObject = new Reserved();
            reservedObject.lab_id = id/100;
            reservedObject.time = (id % 100) / 10;
            reservedObject.day = (id % 100) % 10;
            // Console.WriteLine(JsonConvert.SerializeObject(reservedObject, Formatting.Indented));
            
            var cancelUserList = LAB.CancelList(reservedObject);
            var userList = cancelUserList.Data.data;
            // Console.WriteLine(JsonConvert.SerializeObject(userList, Formatting.Indented));
            // Console.WriteLine(userList.GetType());

            List<CancelReserved> cancelReserveds = new List<CancelReserved>();
            int i = 0;
            foreach (var item in userList) {
                if (checkedList[i]) {
                    var tmp = new CancelReserved(item.reserved_id);
                    cancelReserveds.Add(tmp);
                }
                i++;
            }

            LAB.Cancel(cancelReserveds.ToArray());
            // Console.WriteLine(JsonConvert.SerializeObject(cancelReserveds, Formatting.Indented));
            // Console.WriteLine(JsonConvert.SerializeObject(checkedList, Formatting.Indented));
            return RedirectToAction("Index", "LabManage");
        }
    }
}