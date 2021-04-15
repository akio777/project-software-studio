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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LabReservation.Controllers
{
    public class MyReserveController : Controller {
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
            return View(cancel_read);
        }
    }
}