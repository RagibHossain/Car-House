using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models.ViewModels
{
    public class CarViewModel
    {
        [Required]
        public string CarID { get; set; }
        
        public string Description { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [Display(Name ="Brand")]
        public int BrandID { get; set; }
        
        public Brand Brand { get; set; }
        
        [Required]
        public string Color { get; set; }

        [Required]
        public Transmission Transmission { get; set; }
        [Required]
        public Condition Condition { get; set; }
        [Required]
        [Display(Name ="Fuel Type")]
        public FuelType FuelType { get; set; }
        [Required]
        [Display(Name = "Gear Type")]
        public GearType GearType { get; set; }
        [Required]
        [Display(Name = "Body Type")]
        public string BodyType { get; set; }
        [Display(Name = "Engine Size")]
        public string EngineSize { get; set; }
        [Required]
        [Display(Name = "Number of seats")]
        public int NoOfSeats { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

       
        public decimal Mileage { get; set; }
        public Category Category { get; set; }

        [Display(Name ="Upload Photos")]
        public List<IFormFile> Images { get; set; }
        public string DisplayImage { get; set; }

        //  public ICollection<Image> Images { get; set; }


    }
}
