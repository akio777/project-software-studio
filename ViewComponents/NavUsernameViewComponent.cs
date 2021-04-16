using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LabReservation.Data;
using LabReservation.Services;
using LabReservation.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace LabReservation.ViewComponents
{
    public class NavUsernameViewComponent : ViewComponent
    {
        private readonly LabReservationContext _context;
        private readonly ILabService LAB;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NavUsernameViewComponent(ILabService labservice, LabReservationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            LAB = labservice;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.Clone().FindFirst("Id").Value);
            var username = LAB.GetEmailUser(userId);
            var navbarProps = new NavBarProps(username.Data.email);
            return View(navbarProps);
        }
    }
}