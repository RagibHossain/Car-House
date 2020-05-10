using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models.ViewModels
{
    public class CarHouseViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}
