using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models.ViewModels
{
    public class ProfileViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        [Key]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Enter a valid email")]
        [Display(Name = " Email Address : ")]
        public string UserID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string ProfilePicture { get; set; } 
        [Display(Name ="Name : ")]
        public string FullName
        { 
            get
            {
                return FirstName + " " + LastName;
            }
        }
        [Display(Name="Old Password")]
       [DataType(DataType.Password)]
       [Required(ErrorMessage ="Enter Password")]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name ="New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name ="Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = ("Password Mismatches"))]
        public string ConfirmPassword { get; set; }

         [Display(Name = "Select Image")]
         [Required(ErrorMessage = " Please Select an Image")]
         public IFormFile Location { get; set; }


    }
}
