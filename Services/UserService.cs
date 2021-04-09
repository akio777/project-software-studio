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
using RegisterModel = LabReservation.Models.RegisterModel;


namespace LabReservation.Services
{
    public interface IUserService
    {
        Return CheckLogin(AuthenModel data);
        Return CheckRegister(RegisterModel data);
    }
    
    public class UserService : IUserService
    {
        private readonly LabReservationContext db;
        public UserService(LabReservationContext context)
        {
            db = context;
        }

        public Return CheckLogin(AuthenModel data)
        {
            var c_user = db.Userinfo.FirstOrDefault(userinfo => userinfo.email == data.email);
            if (c_user != null)
            {
                if (c_user.password.Equals(data.password))
                {
                    return new Return
                    {
                        Error = false,
                        Data = c_user
                    };
                    
                }
                return new Return
                {
                    Error = true,
                    Data = "รหัสผ่านไม่ถูกต้อง"
                };
            }
            else
            {
                return new Return
                {
                    Error = true,
                    Data = "ไม่พบผู้ใช้นี้ในระบบ"
                };
            }
        }

        public Return CheckRegister(RegisterModel data)
        {
            // Console.WriteLine(data);
            var c_user = db.Userinfo.FirstOrDefault(userinfo => userinfo.email == data.Email);
            if (c_user != null)
            {
                return new Return
                {
                    Error = true,
                    Data = "มีผู้ใช้นี้อยู่ในระบบอยู่แล้ว"
                };
            }
            return new Return
            {
                Error = false,
                Data = c_user
            };
            
        }
    }
}