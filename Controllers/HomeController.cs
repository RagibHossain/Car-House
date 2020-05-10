using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Car_House.Models;
using Car_House.Data;
using Microsoft.EntityFrameworkCore;
using Car_House.Models.ViewModels;

namespace car_house.Controllers
{
    public class HomeController : Controller
    {
       // private readonly ILogger<HomeController> _logger;
        private readonly Car_HouseContext _context;

        public HomeController(Car_HouseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult about()
        {
            return View();
        }


        public IActionResult service()
        {
            return View();
        }

        public IActionResult contact()
        {
            return View();
        }

        public IActionResult drvn()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
