using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabReservation.Data;
using LabReservation.Models;
using Microsoft.AspNetCore.Authorization;

namespace LabReservation.Controllers
{
    [Authorize(Roles = "0")]
    public class UserinfoController : Controller
    {
        private readonly LabReservationContext _context;

        public UserinfoController(LabReservationContext context)
        {
            _context = context;
        }

        // GET: Userinfo
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Userinfo.ToListAsync());
        }
        

        // GET: Userinfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo
                .FirstOrDefaultAsync(m => m.id == id);
            if (userinfo == null)
            {
                return NotFound();
            }

            return View(userinfo);
        }

        // GET: Userinfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Userinfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,email,password,role")] Userinfo userinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userinfo);
        }

        // GET: Userinfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo.FindAsync(id);
            if (userinfo == null)
            {
                return NotFound();
            }
            return View(userinfo);
        }

        // POST: Userinfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,email,password,role")] Userinfo userinfo)
        {
            if (id != userinfo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserinfoExists(userinfo.id))
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
            return View(userinfo);
        }

        // GET: Userinfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfo
                .FirstOrDefaultAsync(m => m.id == id);
            if (userinfo == null)
            {
                return NotFound();
            }

            return View(userinfo);
        }

        // POST: Userinfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userinfo = await _context.Userinfo.FindAsync(id);
            _context.Userinfo.Remove(userinfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserinfoExists(int id)
        {
            return _context.Userinfo.Any(e => e.id == id);
        }
        
        // [AllowAnonymous]
        // [Route("{*url:regex(^(?!api).*$)}", Order = 999)]
        // public IActionResult CatchAll()
        // {
        //     return RedirectToAction("Index", "NoPermission");
        // }
    }
    
}
