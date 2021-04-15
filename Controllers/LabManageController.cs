using System;
using System.Collections.Generic;
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

            var labManageInfo = new LabManageInfoProps(labinfo, equipment, reservePageList.Data);         

            return View(labManageInfo);
        }
    }
}