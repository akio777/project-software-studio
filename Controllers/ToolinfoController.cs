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
    public class ToolinfoController : Controller
    {
        private readonly LabReservationContext _context;

        public ToolinfoController(LabReservationContext context)
        {
            _context = context;
        }

        // GET: Toolinfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Toolinfo.ToListAsync());
        }

        // GET: Toolinfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toolinfo = await _context.Toolinfo
                .FirstOrDefaultAsync(m => m.id == id);
            if (toolinfo == null)
            {
                return NotFound();
            }

            return View(toolinfo);
        }

        // GET: Toolinfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Toolinfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,type,update_by,created_by,created_date,update_date")] Toolinfo toolinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toolinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toolinfo);
        }

        // GET: Toolinfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toolinfo = await _context.Toolinfo.FindAsync(id);
            if (toolinfo == null)
            {
                return NotFound();
            }
            return View(toolinfo);
        }

        // POST: Toolinfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,type,update_by,created_by,created_date,update_date")] Toolinfo toolinfo)
        {
            if (id != toolinfo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toolinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToolinfoExists(toolinfo.id))
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
            return View(toolinfo);
        }

        // GET: Toolinfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toolinfo = await _context.Toolinfo
                .FirstOrDefaultAsync(m => m.id == id);
            if (toolinfo == null)
            {
                return NotFound();
            }

            return View(toolinfo);
        }

        // POST: Toolinfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toolinfo = await _context.Toolinfo.FindAsync(id);
            _context.Toolinfo.Remove(toolinfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToolinfoExists(int id)
        {
            return _context.Toolinfo.Any(e => e.id == id);
        }
    }
}
