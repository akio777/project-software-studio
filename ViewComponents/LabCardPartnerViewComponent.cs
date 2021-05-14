using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabReservation.Data;
using Microsoft.EntityFrameworkCore;
using LabReservation.Models;

namespace LabReservation.ViewComponents
{
    public class LabCardPartnerViewComponent : ViewComponent
    {
        private readonly LabReservationContext _context;

        public LabCardPartnerViewComponent(LabReservationContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }

}