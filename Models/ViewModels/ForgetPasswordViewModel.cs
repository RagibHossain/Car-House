using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Enter a valid email")]
        [Display(Name = "Enter Your Email Address : ")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "Please Answer The Question")]
        [Display(Name = "What is your first phone Number ?")]
        public string SecurityQuestion1 { get; set; }

        [Display(Name = "What's your first love's name ?")]
        [Required(ErrorMessage = "Please Answer The Question")]
        public string SecurityQuestion2 { get; set; }
        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = ("Password Mismatches"))]
        public string ConfirmPassword { get; set; }
    }
}
