using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Car_House.Data;
using Car_House.Models;
using Car_House.Models.NecessaryClasses;
using Car_House.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Car_House.Controllers
{
    public class AdminController : Controller
    {
        private readonly Car_HouseContext _context;

        public IWebHostEnvironment HostEnvironment { get; }

        //  private object webHostEnvironment;

        public AdminController(Car_HouseContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            HostEnvironment = hostEnvironment;
        }
        // GET: Admin
        public ActionResult Profile()
        {
            string email = HttpContext.Session.GetString("Email");
          //  if (email == null) email = "ragibibnehossain@gmail.com";
            User user = _context.User.FirstOrDefault(x => x.UserID == email);
            if(user == null) 
            {
                // Entering this condition means session has been cleared so this user is not logged in
                return RedirectToAction("Login", "Users");
            }

            // ProfilePicture
            string pp = null;
            if (user.ProfilePicture == null) pp = "Person.jpg";
            else pp = user.ProfilePicture;

            ProfileViewModel pm = new ProfileViewModel
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = pp
            };

            ViewData["Brands"] =  _context.Brands.ToList();

            return View(pm);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
       //    ViewBag.NewPass = false;
            return RedirectToAction("Login","Users");
        }

        //ChangePassword View

            public ActionResult ChangePassword()
            {

         //   ViewBag.Verified = true;
         //   ViewBag.NewPass = false;
            return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ProfileViewModel pvm)
        {

            string mail = HttpContext.Session.GetString("Email");

            User user = _context.User.FirstOrDefault(x => x.UserID == mail);

            

            if(user == null)
            {
                // ViewBag.Verified = false;
                return RedirectToAction("Login", "Users");
            }

            // verifying old password that was inserted by user
            bool verified = PasswordEncryption.VerifyPasswordHash(pvm.OldPassword, user.PasswordHash,  user.PasswordSalt);

            if(!verified)
            {
                //This viewbag is checked in the view to display Wrong password error
               // ViewBag.Verified = false;
                return View(pvm);
            }

            PasswordEncryption.CreatePasswordHash(pvm.ConfirmPassword, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            try
            {
                _context.User.Update(user);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }

            // this view is declared to show login again with new pass in Login Page
         
            return RedirectToAction("Login", "Users");
        }

        // GET: Admin/Edit/5
        public ActionResult Update()
        {
            string mail = HttpContext.Session.GetString("Email");
            User user = _context.User.FirstOrDefault(x => x.UserID == mail);
            ProfileViewModel pmv = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(pmv);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProfileViewModel pmv,string id)
        {

            string mail = HttpContext.Session.GetString("Email");
            User user = _context.User.FirstOrDefault(x => x.UserID ==mail);

            //UploadFile function Creates a filename and Saves the image to images folder of wwwroot directory

            string uniqueFileName = null;
            if (pmv.Location!=null) uniqueFileName = UploadedFile(pmv);
            if (user == null)
            {
                return NotFound();
            }
         //   user.UserID = pmv.UserID;
            user.FirstName = pmv.FirstName;
            user.LastName = pmv.LastName;
          if(pmv.Location!=null)  user.ProfilePicture = uniqueFileName;

            try
            {

                _context.User.Update(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Profile));
            }
            catch
            {
                return View(pmv);
            }
        }

        private string UploadedFile(ProfileViewModel model)
        {
            string uniqueFileName = null;

            if (model.Location != null)
            {
                string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Location.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    
                    model.Location.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public ActionResult AddCar()
        {

            return View();
        }

        public ActionResult UploadCarBrands()
        {
            IQueryable<string> brands = from m in _context.Brands select m.BrandName;
            ViewData["Brands"] = _context.User.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadCarBrands(Brand brand)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Brands.Add(brand);
                    _context.SaveChanges();
                    return View();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }


            return View(brand);
        }
        // GET: Admin/Delete/5

    }
}
