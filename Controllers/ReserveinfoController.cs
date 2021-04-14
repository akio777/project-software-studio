using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public class ReserveinfoController : Controller
    {
        private readonly ILabService LAB;
        private readonly LabReservationContext _context;

        public ReserveinfoController(ILabService labservice, LabReservationContext context)
        {
            _context = context;
            LAB = labservice;
        }

        // GET: Reserveinfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserveinfo.ToListAsync());
        }
        
        
        // [HttpPost]
        // public IActionResult Confirm(Reserve_confirm data)
        public IResourceService Confirm(Reserved[] data)
        {
            // int userid = 1;
            // var mock = new Reserve_confirm
            // {
            //     confirm = new Reserved[]
            //     {
            //         new Reserved {time = 0, day = 0, lab_id = 1},
            //         new Reserved {time = 1, day = 2, lab_id = 1},
            //         new Reserved {time = 2, day = 3, lab_id = 1},
            //         new Reserved {time = 3, day = 4, lab_id = 1},
            //         new Reserved {time = 4, day = 5, lab_id = 1},
            //         new Reserved {time = 5, day = 6, lab_id = 1},
            //         
            //     }
            // };
            // var temp = LAB.Confirm(mock, userid);
            var temp = LAB.ReadCancel( 1);
            return null;
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
