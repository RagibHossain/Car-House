using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models
{
    public class Brand
    {
        public int BrandID { get; set; }

        [Display(Name ="Brand")]
        [Required]
        public string BrandName { get; set; }
        public ICollection<Car> Cars { get; set; }


    }
}
