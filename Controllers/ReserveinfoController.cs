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
    public class ReserveinfoController : Controller
    {
        private readonly LabReservationContext _context;

        public ReserveinfoController(LabReservationContext context)
        {
            _context = context;
        }

        // GET: Reserveinfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserveinfo.ToListAsync());
        }
        
        
        [HttpPost]
        [Route("[action]")]
        public IActionResult Confirm(Reserve_confirm data)
        {
            return RedirectToAction("Index");
        }
        
        // GET: Reserveinfo/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reserveinfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserveinfo = await _context.Reserveinfo.FindAsync(id);
            _context.Reserveinfo.Remove(reserveinfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveinfoExists(int id)
        {
            return _context.Reserveinfo.Any(e => e.id == id);
        }
    }
}
