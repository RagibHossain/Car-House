using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models.ViewModels
{
    public class BrandViewModel
    {
        public IEnumerable<Brand> Brands { get; set; }
        [Required]
        [Display(Name = "Brand Name:")]
        public string BrandName { get; set; }
    }
}
