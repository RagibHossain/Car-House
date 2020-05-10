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
using Microsoft.Extensions.Logging;
using Car_House.Models.NecessaryClasses;
using Microsoft.AspNetCore.Http;
using Car_House.Views.Brands;

namespace Car_House.Controllers
{
    public class UsersController : Controller
    {
        private readonly Car_HouseContext _context;

        public UsersController(Car_HouseContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Login
       // ViewData["User"] = false;
        public IActionResult Login()
        {
            ViewData["User"] = true;
            ViewBag.NewPass = false;
            return View();
        }

        // Post: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {

            User user =await _context.User.FirstOrDefaultAsync(x => x.UserID == viewModel.UserID);
           
            if(user == null)
            {
                ViewData["User"] = false;
                ViewBag.NewPass = false;
                return View();
            }
            if (!PasswordEncryption.VerifyPasswordHash(viewModel.Password,user.PasswordHash,user.PasswordSalt) )
            {
                ViewData["User"] = false;
                ViewBag.NewPass = false;
                return View();
            }
            string fullName = user.FirstName + " " + user.LastName;
            //HttpContext.Session.SetString("UserName",fullName);
            HttpContext.Session.SetString("Email", user.UserID);

            // this is to load all the brands in selectlist 

            //IQueryable<string> brands = from m in _context.Brands select m.BrandName;
             
          
           
            return RedirectToAction("Profile","Admin");
        }



        // GET: Users/Registration
        public IActionResult Registration()
        {
            // This variable is false initially . Its true when user tries to open an account with an email which already exists in database . 
            ViewData["Exists"] = false; 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel UserView)
        {

            byte[] hash=null;
            byte[] salt=null;

            PasswordEncryption.CreatePasswordHash(UserView.ConfirmPassword, out hash, out salt);
            //UserView.Error = false;
                User user = new User
                {
                    UserID = UserView.UserID,
                    FirstName = UserView.FirstName,
                    LastName = UserView.LastName,
                    SecurityQuestion1 = UserView.SecurityQuestion1,
                    SecurityQuestion2 = UserView.SecurityQuestion2,
                    PasswordHash = hash,
                    PasswordSalt = salt
                };
                try 
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Login));
                }
                catch(DbUpdateException ex)
                {

                // Control of code reached catch block means there was an exception Adding user instance which means same email entry
                ViewData["Exists"] = true;
               
                 return View(UserView); // Sending to delete page if an account with same email exists
                }
            
           
        }


        public IActionResult ForgetPassword()
        {
            ViewData["Exists"] = true;
            ViewData["RightAnswer"] = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult ForgetPassword( ForgetPasswordViewModel fpvm)
        {

         /*   if(!ModelState.IsValid)
            {
               //
                return View(fpvm);
            }
*/
            User user = _context.User.FirstOrDefault(x => x.UserID == fpvm.UserID);
            if(user == null)
            {

                ViewData["Exists"] = false;
                return View();
            }

            if(fpvm.SecurityQuestion1 == user.SecurityQuestion1 && fpvm.SecurityQuestion2== user.SecurityQuestion2)
            {

            HttpContext.Session.SetString("userID", fpvm.UserID);
            return  RedirectToAction("NewPassword");
            }


            ViewData["RightAnswer"] = false;
            ViewData["Exists"] = true;
            return View(fpvm);
        }

      //  private string Email;
        // Set password get function
        public IActionResult NewPassword()
        {
        
            return View();
        }

      [HttpPost,ActionName("NewPassword")]
      [ValidateAntiForgeryToken]

      public IActionResult SetPassword([Bind("NewPassword,ConfirmPassword")]SetPasswordViewModel fpvm)
        {

            if(!ModelState.IsValid)
            {
                return View(fpvm);
            }
            string email = HttpContext.Session.GetString("userID");
            User user = _context.User.FirstOrDefault(x => x.UserID == email);

            if(user == null)
            {
                return NotFound();
            }

            PasswordEncryption.CreatePasswordHash(fpvm.ConfirmPassword, out byte[] hash, out byte[] salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            try
            {
                _context.User.Update(user);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {

                throw ex;
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.UserID == id);
        }
    }
}
