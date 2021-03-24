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
    public class LabstoreController : Controller
    {
        private readonly LabReservationContext _context;

        public LabstoreController(LabReservationContext context)
        {
            _context = context;
        }

        // GET: Labstore
        public async Task<IActionResult> Index()
        {
            return View(await _context.Labstore.ToListAsync());
        }

        // GET: Labstore/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labstore = await _context.Labstore
                .FirstOrDefaultAsync(m => m.id == id);
            if (labstore == null)
            {
                return NotFound();
            }

            return View(labstore);
        }

        // GET: Labstore/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Labstore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,lad_id,tool_id,update_by,created_by,created_date,update_date")] Labstore labstore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labstore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labstore);
        }

        // GET: Labstore/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labstore = await _context.Labstore.FindAsync(id);
            if (labstore == null)
            {
                return NotFound();
            }
            return View(labstore);
        }

        // POST: Labstore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,lad_id,tool_id,update_by,created_by,created_date,update_date")] Labstore labstore)
        {
            if (id != labstore.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labstore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabstoreExists(labstore.id))
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
            return View(labstore);
        }

        // GET: Labstore/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labstore = await _context.Labstore
                .FirstOrDefaultAsync(m => m.id == id);
            if (labstore == null)
            {
                return NotFound();
            }

            return View(labstore);
        }

        // POST: Labstore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labstore = await _context.Labstore.FindAsync(id);
            _context.Labstore.Remove(labstore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabstoreExists(int id)
        {
            return _context.Labstore.Any(e => e.id == id);
        }
    }
}
