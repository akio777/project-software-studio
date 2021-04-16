using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LabReservation.Data;
using LabReservation.Models;
using Newtonsoft.Json;

namespace LabReservation.ViewComponents
{
    public class CheckBoxViewComponent : ViewComponent
    {
        private readonly LabReservationContext _context;

        public CheckBoxViewComponent(LabReservationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(WhoReserved whoReserved)
        {
            Console.WriteLine(JsonConvert.SerializeObject(whoReserved, Formatting.Indented));
            return View(whoReserved);
        }
    }
}