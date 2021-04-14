using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabReservation.Data;
using LabReservation.Models;

namespace LabReservation.Controllers
{
    public class LabManageController : Controller
    {
        private readonly LabReservationContext _context;

        public LabManageController(LabReservationContext context)
        {
            _context = context;
        }

        // GET: Labinfo
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}