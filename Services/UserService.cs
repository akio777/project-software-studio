using System.Linq;

using LabReservation.Models;
using LabReservation.Data;
using RegisterModel = LabReservation.Models.RegisterModel;


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
            if (data.email==null || data.password==null)
            {
                return new Return
                {
                    Error = true,
                    Data =  "กรุณากรอกข้อมูล"
                };
            }
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
            if (data.Email==null || data.Password==null || data.ConfirmPassword == null)
            {
                return new Return
                {
                    Error = true,
                    Data =  "กรุณากรอกข้อมูล"
                };
            }
            else if (data.Password.Length<6 || data.ConfirmPassword.Length<6)
            {
                return new Return
                {
                    Error = true,
                    Data =  "รหัสผ่านต้องมีความยาวตั้งแต่ 6 ตัวขึ้นไป"
                };
            }
            else if (!data.Password.Equals(data.ConfirmPassword))
            {
                return new Return
                {
                    Error = true,
                    Data =  "รหัสผ่านไม่ตรงกัน"
                };
            }
            var c_user = db.Userinfo.FirstOrDefault(userinfo => userinfo.email == data.Email);
            if (c_user != null)
            {
                return new Return
                {
                    Error = true,
                    Data = "ไม่สามารถใช้ Email นี้ได้, มี email อยู่ในระบบแล้ว"
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