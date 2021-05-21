using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel.Design;
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
using Microsoft.AspNetCore.Http;

namespace LabReservation.Controllers
{
    [Authorize]
    public class ReserveinfoController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILabService LAB;
        private readonly LabReservationContext _context;

        public ReserveinfoController(ILabService labservice, LabReservationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            LAB = labservice;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Reserveinfo
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Lab(int id, string msg = "")
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.Clone().FindFirst("Id").Value);
                var reservePageList = LAB.Read(id, userId);
                var labinfo = await _context.Labinfo.FirstOrDefaultAsync(m => m.id == id);
                // Console.WriteLine(JsonConvert.SerializeObject(reservePageList, Formatting.Indented));

                var reserveinfoProps = new ReserveinfoProps(reservePageList.Data, labinfo);
                if (msg != "")
                {
                    reserveinfoProps.status = true;
                    reserveinfoProps.msg = msg;
                }
                return View("Index", reserveinfoProps);
            }
        }

        // [HttpPost]
        public IActionResult Confirm(ReservedInput reservedInput, int id)
        {
            var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.Clone().FindFirst("Id").Value);
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
                        reservedObject.lab_id = id;
                        reservedList.Add(reservedObject);
                    }
                }
            }
            var str = LAB.Confirm(reservedList.ToArray(), userId).Data;
            // LAB.Confirm(reservedList.ToArray(), userId);
            // Console.WriteLine(LAB.Confirm(reservedList.ToArray(), userId).Data);
            return RedirectToAction("Lab", "Reserveinfo", new { id = id, msg = str });
        }

        // GET: Reserveinfo/Details/5
        [Authorize(Roles = "0")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserveinfo = await _context.Reserveinfo
                .FirstOrDefaultAsync(m => m.id == id);
            if (reserveinfo == null)
            {
                return NotFound();
            }

            return View(reserveinfo);
        }

        // GET: Reserveinfo/Create
        [Authorize(Roles = "0")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reserveinfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "0")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,lab_id,reserve_by,start_time,end_time")] Reserveinfo reserveinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserveinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserveinfo);
        }

        // GET: Reserveinfo/Edit/5
        [Authorize(Roles = "0")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserveinfo = await _context.Reserveinfo.FindAsync(id);
            if (reserveinfo == null)
            {
                return NotFound();
            }
            return View(reserveinfo);
        }

        // POST: Reserveinfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "0")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,lab_id,reserve_by,start_time,end_time")] Reserveinfo reserveinfo)
        {
            if (id != reserveinfo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserveinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveinfoExists(reserveinfo.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reserveinfo);
        }

        // GET: Reserveinfo/Delete/5
        [Authorize(Roles = "0")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserveinfo = await _context.Reserveinfo
                .FirstOrDefaultAsync(m => m.id == id);
            if (reserveinfo == null)
            {
                return NotFound();
            }

            return View(reserveinfo);
        }

        // POST: Reserveinfo/Delete/5
        [Authorize(Roles = "0")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserveinfo = await _context.Reserveinfo.FindAsync(id);
            _context.Reserveinfo.Remove(reserveinfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "0")]
        private bool ReserveinfoExists(int id)
        {
            return _context.Reserveinfo.Any(e => e.id == id);
        }

        [AllowAnonymous]
        [Route("{*url:regex(^(?!api).*$)}", Order = 999)]
        public IActionResult CatchAll()
        {
            return RedirectToAction("Index", "NoPermission");
        }
    }
}
