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

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            var labmanage_info = new LabManageCard();
            labmanage_info.labinfo = await _context.Labinfo.FirstOrDefaultAsync(m => m.id == id);
            labmanage_info.equipinfo = await _context.Equipment.FirstOrDefaultAsync(m => m.lab_id == id);
            return View(labmanage_info);
        }
    }
}