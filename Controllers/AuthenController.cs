      
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using LabReservation.Data;
using LabReservation.Models;
using LabReservation.Controllers;
using LabReservation.Services;
using System.Security.Claims;

namespace LabReservation.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IUserService user_service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            user_service = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        
        
        [AllowAnonymous]
        [Route("")]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        [Route("[action]")]
        [HttpPost]  
        public async Task<ActionResult> Login(AuthenModel data)
        {   
            var temp = user_service.CheckLogin(data);
            if (temp.Error)
            {
                ModelState.AddModelError(String.Empty, temp.Data);
                return View(data);
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim("Id", temp.Data.id.ToString()),
                    new Claim(ClaimTypes.Role, temp.Data.role.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal ReClaims = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ReClaims);
                return RedirectToAction("Index","Userinfo");   
            }
        }
        
        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]  
        public ActionResult Register(RegisterModel data)
        {   
            // Console.WriteLine(data.Email+", "+ data.Password+", "+ data.ConfirmPassword);
            var temp = user_service.CheckRegister(data);
            if (temp.Error)
            {
                ModelState.AddModelError(String.Empty, temp.Data);
                return View(data);
            }
            else
            {
                return View();
            }
            // if (temp.Error)
            // {
            //     ModelState.AddModelError(String.Empty, temp.Data);
            //     return View(data);
            // }
            // else
            // {
            //     var claims = new List<Claim>
            //     {
            //         new Claim("Id", temp.Data.id.ToString()),
            //         new Claim(ClaimTypes.Role, temp.Data.role.ToString())
            //     };
            //     var claimsIdentity = new ClaimsIdentity(
            //         claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //     ClaimsPrincipal ReClaims = new ClaimsPrincipal(claimsIdentity);
            //     await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ReClaims);
            //     return RedirectToAction("Index","Userinfo");   
            // }
            
        }
        
        [Route("[action]")]
        [HttpGet]  
        public ActionResult Login()  
        {
                return View();  
        }
        
        [Route("[action]")]
        [HttpGet]  
        public ActionResult Register()  
        {
            return View();  
        }
        
        [Route("[action]")]
        [HttpGet]  
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}