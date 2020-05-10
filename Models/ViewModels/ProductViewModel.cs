using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models.ViewModels
{
    public class ProductViewModel
    {
        public Car Car { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public string BrandName { get; set; }

    }
}
