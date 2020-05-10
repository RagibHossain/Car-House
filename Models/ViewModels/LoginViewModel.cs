using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models.ViewModels
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        [Key]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Enter a valid email")]
        [Display(Name = " Email Address")]
        public string UserID { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
