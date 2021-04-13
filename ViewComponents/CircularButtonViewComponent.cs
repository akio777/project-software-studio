
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LabReservation.Data;
using LabReservation.Models;

namespace LabReservation.ViewComponents
{
    public class CircularButtonViewComponent : ViewComponent
    {
        private readonly LabReservationContext _context;
        public CircularButtonViewComponent(LabReservationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}