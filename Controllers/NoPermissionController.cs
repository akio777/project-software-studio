using Microsoft.AspNetCore.Mvc;

namespace LabReservation.Controllers
{
    public class NoPermissionController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}