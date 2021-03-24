using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabReservation.Data;
using LabReservation.Models;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace LabReservation.Controllers
{
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
            var token = Request.Headers["Authorization"];
            // var token = Request.Cookies;
            if (token.Count < 1)
            {
                return View("~/Views/NoPermission.cshtml");
            }
            
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
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Microsoft.AspNetCore.Mvc.Bind("id,user,password,role,update_by,created_by,created_date,update_date")] Userinfo userinfo)
        {
            Console.WriteLine(userinfo.id);
            Console.WriteLine(userinfo.password);
            // if (ModelState.IsValid)
            // {
            //     _context.Add(userinfo);
            //     await _context.SaveChangesAsync();
            //     return RedirectToAction(nameof(Index));
            // }
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
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Microsoft.AspNetCore.Mvc.Bind("id,user,password,role,update_by,created_by,created_date,update_date")] Userinfo userinfo)
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
        [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.ActionName("Delete")]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
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
    }
}
