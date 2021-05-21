using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LabReservation.Data;
using LabReservation.Models;
using LabReservation.Services;

namespace LabReservation.Controllers{
    public class LabPartnerController : Controller{
        private readonly LabReservationContext _context;

        public IActionResult Index(){
            return View();
        }
    }
}