
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LabReservation.Data;
using LabReservation.Models;
using Newtonsoft.Json;

namespace LabReservation.ViewComponents
{
    public class ReservationTableViewComponent : ViewComponent
    {
        private readonly LabReservationContext _context;
        public ReservationTableViewComponent(LabReservationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(ReserveinfoProps reserveinfoProps)
        {
            return View(reserveinfoProps);
        }
    }
}