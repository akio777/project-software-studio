using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReservation.Controllers
{
    [AllowAnonymous]
    public class NoPermissionController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}