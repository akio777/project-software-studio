using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LabReservation.Data;
using LabReservation.Models;

namespace LabReservation.ViewComponents
{
    public class LabManageCardViewComponent : ViewComponent
    {
        private readonly LabReservationContext _context;

        public LabManageCardViewComponent(LabReservationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(LabManageInfo? lab_info)
        {
            return View(lab_info);
        }
    }
}