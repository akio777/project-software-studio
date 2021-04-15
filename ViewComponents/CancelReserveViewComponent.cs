using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabReservation.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LabReservation.ViewComponents
{
    public class CancelReserveViewComponent : ViewComponent
    {
        private readonly LabReservationContext _context;

        public CancelReserveViewComponent(LabReservationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}