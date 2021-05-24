
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using LabReservation.Models;
using LabReservation.Services;
using Newtonsoft.Json;

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

        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("0"))
                {
                    return RedirectToAction("Index", "LabManage");
                }
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel data)
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
                // Console.WriteLine(claims[1].Value);
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal ReClaims = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ReClaims);
                if (temp.Data.role == 0) return RedirectToAction("Index", "LabManage");
                else return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public ActionResult Register(RegisterModel data)
        {
            // Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
            if (data.Email == null || data.Password == null || data.ConfirmPassword == null)
            {
                ModelState.AddModelError(String.Empty, "กรุณากรอกข้อมูลให้ครบถ้วน");
                return View(data);
            }
            else
            {
                bool isRexMatch = Regex.IsMatch(data.Email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                                            + "@"
                                                            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
                if (isRexMatch)
                {
                    var temp = user_service.CheckRegister(data);
                    if (temp.Error)
                    {
                        ModelState.AddModelError(String.Empty, temp.Data);
                        return View(data);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Authen");
                    }
                }
                else
                {
                    
                    ModelState.AddModelError(String.Empty, "รูปแบบ Email ไม่ถูกต้อง");
                    return View(data);
                }
            }
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

        // [Authorize(Roles = "1")]
        // [AllowAnonymous]
        // [Route("")]
        // public IActionResult CatchAll()
        // {
        //     return RedirectToAction("Index", "NoPermission");
        // }
    }
}