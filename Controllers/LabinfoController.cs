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
    public class LabinfoController : Controller
    {
        private readonly LabReservationContext _context;

        public LabinfoController(LabReservationContext context)
        {
            _context = context;
        }

        // GET: Labinfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Labinfo.ToListAsync());
        }

        // GET: Labinfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labinfo = await _context.Labinfo
                .FirstOrDefaultAsync(m => m.id == id);
            if (labinfo == null)
            {
                return NotFound();
            }

            return View(labinfo);
        }

        // GET: Labinfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Labinfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,equip")] Labinfo labinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labinfo);
        }

        // GET: Labinfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labinfo = await _context.Labinfo.FindAsync(id);
            if (labinfo == null)
            {
                return NotFound();
            }
            return View(labinfo);
        }

        // POST: Labinfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,equip")] Labinfo labinfo)
        {
            if (id != labinfo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabinfoExists(labinfo.id))
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
            return View(labinfo);
        }

        // GET: Labinfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labinfo = await _context.Labinfo
                .FirstOrDefaultAsync(m => m.id == id);
            if (labinfo == null)
            {
                return NotFound();
            }

            return View(labinfo);
        }

        // POST: Labinfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labinfo = await _context.Labinfo.FindAsync(id);
            _context.Labinfo.Remove(labinfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabinfoExists(int id)
        {
            return _context.Labinfo.Any(e => e.id == id);
        }
    }
}
