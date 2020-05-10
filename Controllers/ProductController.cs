using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car_House.Data;
using Car_House.Models;
using Car_House.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_House.Controllers
{
    public class ProductController : Controller
    {
        private readonly Car_HouseContext _context;
        public ProductController(Car_HouseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Product(string CarID)
        {


            IQueryable<Image> images = from m in _context.Images select m;
            IEnumerable<Image> CarImages = images.Where(i => i.CarID == CarID).ToList();
            Car car = _context.Cars.Include(b => b.Brand).FirstOrDefault(x => x.CarID == CarID);
            ProductViewModel pvm = new ProductViewModel
            {
                Car = car,
                Images = CarImages,
                BrandName = car.Brand.BrandName

            };

            return View(pvm);
        }

        public IActionResult Products()
        {
            ViewData["Title"] = "Home Page";
            CarHouseViewModel carViewModel = new CarHouseViewModel
            {
                Cars = _context.Cars.ToList()
            };

            return View(carViewModel);
        }

        public IActionResult ProductCategory(string selectedCategory)
        {

            Category category;
            if (selectedCategory == "Sedan") category = Category.Sedan;
            else if (selectedCategory == "Suv") category = Category.Suv;
            else if (selectedCategory == "Sport") category = Category.Sport;
            else category = Category.Minivan;
            IQueryable<Car> cars = from m in _context.Cars select m;


            CarHouseViewModel carViewModel = new CarHouseViewModel
            {
                Cars = cars.Where(x => x.Category == category).ToList()
            };

            return View(carViewModel);
        }


    }
}