using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabReservation.Data;
using Microsoft.EntityFrameworkCore;

namespace LabReservation.ViewComponents {
  public class CancelSelectedReserveViewComponent : ViewComponent
  {
    private readonly LabReservationContext _context;

    public CancelSelectedReserveViewComponent(LabReservationContext context)
        {
            _context = context;
        }
    public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View();
        }
  }

}