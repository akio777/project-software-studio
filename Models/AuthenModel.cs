using System;
using System.ComponentModel.DataAnnotations;

namespace LabReservation.Models
{
    public class AuthenModel
    {
        [RegularExpression("^(.+@.+[.].+|[aA]dmin)$", ErrorMessage = "รูปแบบ email ไม่ถูกต้อง")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "6 ถึง 50 ตัวอักษร", MinimumLength = 6)]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string password { get; set; }
    }

    public class RegisterModel
    {

        [RegularExpression("^(.+@.+[.].+|[aA]dmin)$", ErrorMessage = "รูปแบบ email ไม่ถูกต้อง")]
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