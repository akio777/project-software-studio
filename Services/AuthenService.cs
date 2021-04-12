using System.Linq;

using LabReservation.Models;
using LabReservation.Data;

namespace LabReservation.Services
{
    public interface IUserService
    {
        Return CheckLogin(LoginModel data);
        Return CheckRegister(RegisterModel data);
    }

    public class UserService : IUserService
    {
        private readonly LabReservationContext db;
        public UserService(LabReservationContext context)
        {
            db = context;
        }
        
        public Return CheckLogin(LoginModel data)
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
            var c_user = db.Userinfo.FirstOrDefault(userinfo => userinfo.email == data.Email);
            if (c_user != null)
            {
                return new Return
                {
                    Error = true,
                    Data = "ไม่สามารถใช้ email นี้ได้"
                };
            }
            else
            {
                var new_user = new Userinfo
                {
                    email = data.Email, password = data.Password, role = 1
                };
                db.Userinfo.Add(new_user);
                db.SaveChanges();
                return new Return
                {
                    Error = false,
                    Data = c_user
                };
            }
        }
    }
}