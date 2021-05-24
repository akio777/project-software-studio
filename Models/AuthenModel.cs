using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class LoginModel
    {
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                           + "@"
                           + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "รูปแบบ email ไม่ถูกต้อง")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "6 ถึง 50 ตัวอักษร", MinimumLength = 6)]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string password { get; set; }
    }

    public class RegisterModel
    {

        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                           + "@"
                           + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "รูปแบบ email ไม่ถูกต้อง")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "6 ถึง 50 ตัวอักษร", MinimumLength = 6)]
        [Required(ErrorMessage = "กรุณากรอกข้อมูลให้ครบถ้วน")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "รหัสผ่านไม่ตรงกัน")]
        [StringLength(50, ErrorMessage = "6 ถึง 50 ตัวอักษร", MinimumLength = 6)]
        [Required(ErrorMessage = "กรุณากรอกข้อมูลให้ครบถ้วน")]
        public string ConfirmPassword { get; set; }
    }
}