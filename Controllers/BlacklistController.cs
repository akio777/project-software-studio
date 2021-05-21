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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LabReservation.Controllers
{
    [Authorize(Roles = "0")]
    public class BlacklistController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LabReservationContext _context;

        private readonly ILabService LAB;

        public BlacklistController(LabReservationContext context, ILabService lab, IHttpContextAccessor httpContextAccessor)
        {
            LAB = lab;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Blacklist
        public async Task<IActionResult> Index()
        {
            var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.Clone().FindFirst("Id").Value);
            var blacklist = LAB.BlackListInfo(userId);
            return View(blacklist.Data);
        }

        // GET: Blacklist/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blacklist = await _context.Blacklist
                .FirstOrDefaultAsync(m => m.id == id);
            if (blacklist == null)
            {
                return NotFound();
            }

            return View(blacklist);
        }

        // GET: Blacklist/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blacklist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,user_id")] Blacklist blacklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blacklist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blacklist);
        }

        // GET: Blacklist/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blacklist = await _context.Blacklist.FindAsync(id);
            if (blacklist == null)
            {
                return NotFound();
            }
            return View(blacklist);
        }

        // POST: Blacklist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,user_id")] Blacklist blacklist)
        {
            if (id != blacklist.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blacklist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlacklistExists(blacklist.id))
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
            return View(blacklist);
        }

        // GET: Blacklist/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blacklist = await _context.Blacklist
                .FirstOrDefaultAsync(m => m.id == id);
            if (blacklist == null)
            {
                return NotFound();
            }

            return View(blacklist);
        }

        // POST: Blacklist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blacklist = await _context.Blacklist.FindAsync(id);
            _context.Blacklist.Remove(blacklist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlacklistExists(int id)
        {
            return _context.Blacklist.Any(e => e.id == id);
        }

        public IActionResult AddToBlock(int userid)
        {
            LAB.ForceBlock(userid);
            return RedirectToAction("Index", "Blacklist");
        }

        public IActionResult UnBlock(int userid)
        {
            LAB.UnBlock(userid);
            return RedirectToAction("Index", "Blacklist");
        }
    }
}
