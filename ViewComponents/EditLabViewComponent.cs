using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabReservation.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using LabReservation.Models;

namespace LabReservation.ViewComponents
{
    public class EditLabViewComponent : ViewComponent
    {
        private readonly LabReservationContext _context;

        public EditLabViewComponent(LabReservationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(LabManageInfo labManageInfo)
        {
            return View(labManageInfo);
        }
    }
}