using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabReservation.Data;
using Microsoft.EntityFrameworkCore;

namespace LabReservation.ViewComponents
{
    public class CancelReserveViewComponent : ViewComponent
    {
        private readonly LabReservationContext _context;

        public CancelReserveViewComponent(LabReservationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            if (id == null)
            {
                string model = "<strong>some custom html</strong>";
                return View("", model);
            }

            var reserveinfo = await _context.Reserveinfo
                .FirstOrDefaultAsync(m => m.id == id);
            if (reserveinfo == null)
            {
                string model = "<strong>some custom html</strong>";
                return View("", model);
            }

            return View(reserveinfo);
        }
    }
}