using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models
{
    public class User
    {
        // UserID is actually the email of user . Named it UserID
        // So that the column name in database is UserID
        [DataType(DataType.EmailAddress)]
        [Required]
        [Key]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Enter a valid email")]
        [Display(Name = " Email Address")]
        public string UserID { get; set; } 

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Display(Name = "First Phone Number")]
        [Required(ErrorMessage ="Please Answer The Question")]
        public string SecurityQuestion1 { get; set; }

        [Display(Name ="First Love")]
        [Required(ErrorMessage = "Please Answer The Question")]
        public string SecurityQuestion2 { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string ProfilePicture { get; set; } 

    }
}
