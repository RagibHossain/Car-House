using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Car_House.Data;
using Car_House.Models;
using Car_House.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;

namespace Car_House.Controllers
{
    public class CarsController : Controller
    {
        private readonly Car_HouseContext _context;
        public IWebHostEnvironment HostEnvironment { get; }

        //  private object webHostEnvironment;

        public CarsController(Car_HouseContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            HostEnvironment = hostEnvironment;
        }
        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var car_HouseContext = _context.Cars.Include(c => c.Brand).Include(i => i.Images);
            CarListViewModel Cars = new CarListViewModel
            {
                Cars = await car_HouseContext.ToListAsync()
            };
            ViewData["Cars"] = new SelectList(car_HouseContext, "CarID", "Model");

            return View(Cars);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Index(CarListViewModel car)
        {
            Car Car = _context.Cars.FirstOrDefault(x => x.CarID == car.SelectedCarID);

            return RedirectToAction("AddExistingCar",Car);
        }

        public IActionResult AddExistingCar(Car car)
        {

            CarViewModel carvm = new CarViewModel
            {
                CarID = car.CarID,
                Description = car.Description,
                Model = car.Model,
                Category = car.Category,
                BrandID = car.BrandID,
                Color = car.Color,
                Transmission = car.Transmission,
                Condition = car.Condition,
                FuelType = car.FuelType,
                GearType = car.GearType,
                BodyType = car.BodyType,
                EngineSize = car.EngineSize,
                NoOfSeats = car.NoOfSeats,
                Price = car.Price,
                DisplayImage = car.DisplayImage

            };
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName",carvm.BrandID);
            return View(carvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult AddExistingCar([Bind("CarID,Description,Model,BrandID,Color,Transmission,Condition,FuelType,GearType,BodyType,EngineSize,NoOfSeats,Price,Mileage,Category,Images")]CarViewModel car)
        {
            if(ModelState.IsValid)
            {
                UploadCarInformation(car);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(i => i.Images)
                .FirstOrDefaultAsync(m => m.CarID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarID,Description,Model,BrandID,Color,Transmission,Condition,FuelType,GearType,BodyType,EngineSize,NoOfSeats,Price,Mileage,Category,Images")] CarViewModel carvm)
        {
            if (ModelState.IsValid)
            {
                UploadCarInformation(carvm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", carvm.BrandID);
            return View(carvm);
        }

        private void UploadCarInformation(CarViewModel carvm)
        {
            Car car = new Car
            {

                CarID = carvm.CarID,
                Description = carvm.Description,
                Model = carvm.Model,
                Category = carvm.Category,
                BrandID = carvm.BrandID,
                Transmission = carvm.Transmission,
                Condition = carvm.Condition,
                FuelType = carvm.FuelType,
                GearType = carvm.GearType,
                BodyType = carvm.BodyType,
                EngineSize = carvm.EngineSize,
                NoOfSeats = carvm.NoOfSeats,
                Mileage = carvm.Mileage,
                Price = carvm.Price,
                Color = carvm.Color

            };

            // uploads all the image to image folder and returns a list of image location
            List<string> images = UploadedFile(carvm);

            try
            {
                foreach (var image in images)
                {
                    Image photo = new Image
                    {
                        ImageLocation = image,
                        CarID = carvm.CarID
                    };
                    _context.Images.Add(photo);

                }
            }
            catch (Exception ex)
            {
                throw ex;
               
            }
            if (images != null)
            {
                car.DisplayImage = images.ElementAt(0);
            }


            _context.Cars.Add(car);
            
        }

        private List<string> UploadedFile(CarViewModel model)
        {
            List<string> uniqueFileName = new List<string>();
            int i = 0;
            if (model.Images!=null && model.Images.Count > 0)
            {
                foreach (var image in model.Images)
                {


                    string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath, "images");
                    uniqueFileName.Add(Guid.NewGuid().ToString() + "_" + image.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName[i]);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {

                        image.CopyTo(fileStream);
                    }
                    i++;

                }



            }
            else throw new Exception();
            return uniqueFileName;
        }


        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            
            CarViewModel carvm = new CarViewModel
            {
                CarID = car.CarID,
                Description = car.Description,
                Model = car.Model,
                Category = car.Category,
                BrandID = car.BrandID,
                Color = car.Color,
                Transmission = car.Transmission,
                Condition = car.Condition,
                FuelType = car.FuelType,
                GearType = car.GearType,
                BodyType = car.BodyType,
                EngineSize = car.EngineSize,
                NoOfSeats = car.NoOfSeats,
                Mileage = car.Mileage,
                Price = car.Price,
                DisplayImage = car.DisplayImage

            };
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", carvm.BrandID);
            return View(carvm);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CarID,Description,Model,Color,BrandID,Transmission,Condition,FuelType,GearType,BodyType,EngineSize,NoOfSeats,Price,Mileage,Category,Images,DisplayImage")] CarViewModel car)
        {
            if (id != car.CarID)
            {
                return NotFound();
            }

           

            if (ModelState.IsValid)
            {
                try
                {
                    Car Car = new Car 
                    {
                        CarID = car.CarID,
                        Description = car.Description,
                        Model = car.Model,
                        Category = car.Category,
                        BrandID = car.BrandID,
                        Transmission = car.Transmission,
                        Condition = car.Condition,
                        FuelType = car.FuelType,
                        GearType = car.GearType,
                        BodyType = car.BodyType,
                        EngineSize = car.EngineSize,
                        NoOfSeats = car.NoOfSeats,
                        Mileage = car.Mileage,
                        Price = car.Price,
                        DisplayImage = car.DisplayImage,
                        Color = car.Color

                    };

                 List<string> images = null;
                 if (car.Images!= null && car.Images.Count>0)  images = UploadedFile(car);

                    if(images!= null) 
                    {
                        foreach (var image in images)
                        {
                            Image photo = new Image
                            {
                                ImageLocation = image,
                                CarID = car.CarID
                            };
                            _context.Images.Add(photo);

                        }

                    }
                 


                    _context.Update(Car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            } // if model state ends
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", car.BrandID);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string cID)
        {
            var car = await _context.Cars.FindAsync(cID);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(string id)
        {
            return _context.Cars.Any(e => e.CarID == id);
        }


        public IActionResult DeleteImage(int imageID)
        {

            ImageViewModel imgvm = GetImageViewModel(imageID);
         
            return View(imgvm);
        }
        [HttpPost,ActionName("DeleteImage")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteImageConfirmed(int ImageID)
        {
            Image image = _context.Images.FirstOrDefault(i => i.ImageID == ImageID);
            
            if(image != null)
            {
                _context.Images.Remove(image);
                _context.SaveChanges();
                return RedirectToAction("Details",new RouteValueDictionary(
                  new { action = "Details", Id = image.CarID }));
            }

            return View();
        }

        public IActionResult SetDisplayImage(int imageID)
        {
            ImageViewModel imgvm = GetImageViewModel(imageID);

            return View(imgvm);
        }
        [HttpPost,ActionName("SetDisplayImage")]
        [ValidateAntiForgeryToken]
        public IActionResult SetDisplayImageConfirmed(int imageID,string carID)
        {
            Image image = _context.Images.FirstOrDefault(i => i.ImageID == imageID);
            Car car = _context.Cars.FirstOrDefault(c => c.CarID == carID);
            car.DisplayImage = image.ImageLocation;

            try
            {
                _context.Cars.Update(car);
                _context.SaveChanges();
                return RedirectToAction("Details", new RouteValueDictionary(
                 new { action = "Details", Id = image.CarID }));
            }
            catch(Exception ex)
            {
                throw ex;
            }


            
        }

        private ImageViewModel GetImageViewModel(int imageID)
        {
            Image image = _context.Images.Include(x => x.Car).ThenInclude(i => i.Brand).FirstOrDefault(z => z.ImageID == imageID);

            ImageViewModel imgvm = new ImageViewModel
            {
                Image = image,
                CarBrand = image.Car.Brand.BrandName,
                CarModel = image.Car.Model,
                ImageID = imageID,
                CarID = image.CarID
            };

            return imgvm;
        }
    }
}
