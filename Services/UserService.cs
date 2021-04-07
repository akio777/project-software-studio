using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using LabReservation.Models;
using LabReservation.Data;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;


namespace LabReservation.Services
{
    public interface IUserService
    {
        Return Check(AuthenModel data);
    }
    
    public class UserService : IUserService
    {
        private readonly LabReservationContext db;
        public UserService(LabReservationContext context)
        {
            db = context;
        }

        public Return Check(AuthenModel data)
        {
            var c_user = db.Userinfo.FirstOrDefault(userinfo => userinfo.email == data.email);
            if (c_user != null)
            {
                Console.WriteLine("FOUND");
                return new Return
                {
                    Error = false,
                    Data = c_user
                };
            }
            else
            {
                Console.WriteLine("NOT FOUND");
                return new Return
                {
                    Error = true,
                    Data = "ไม่พบผู้ใช้นี้ในระบบ"
                };
            }
        }

    }
}