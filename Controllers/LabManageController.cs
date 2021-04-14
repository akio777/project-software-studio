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

        public async Task<IActionResult> EditCancel(int? id)
        {
            if (id == null) {
                return NotFound();
            }

            var labmanage_info = new LabManageCard();
            labmanage_info.labinfo = await _context.Labinfo.FirstOrDefaultAsync(m => m.id == id);
            labmanage_info.equipinfo = await _context.Equipment.FirstOrDefaultAsync(m => m.lab_id == id);

            if (labmanage_info.labinfo == null || labmanage_info.equipinfo == null) {
                return NotFound();
            }

            return View(labmanage_info);
        }
    }
}