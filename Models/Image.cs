using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public string ImageLocation { get; set; }
        public string CarID { get; set; }
        public Car Car { get; set; }
     
    }
}
