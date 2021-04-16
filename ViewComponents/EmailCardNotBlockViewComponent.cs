using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabReservation.Data;
using Microsoft.EntityFrameworkCore;
using LabReservation.Models;

namespace LabReservation.ViewComponents
{
    public class EmailCardNotBlockViewComponent : ViewComponent
    {
        private readonly LabReservationContext _context;

        public EmailCardNotBlockViewComponent(LabReservationContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(UserEmail lab)
        {
            return View(lab);
        }
    }

}