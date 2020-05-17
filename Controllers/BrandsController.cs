using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Car_House.Data;
using Car_House.Models;
using Microsoft.AspNetCore.Http;
using Car_House.Models.ViewModels;

namespace Car_House.Views.Brands
{
    public class BrandsController : Controller
    {
        private readonly Car_HouseContext _context;

        public BrandsController(Car_HouseContext context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            if(HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Users");
            }

            var model = new BrandViewModel
            {
                Brands = await _context.Brands.ToListAsync()
            };

            return View(model);
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.BrandID == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand
                {
                    BrandName = model.BrandName
                };
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int bID, BrandViewModel model)
        {
            var brand = await _context.Brands.FindAsync(bID);
            if(brand == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    brand.BrandName = model.BrandName;
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.BrandID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int bID)
        {
            var brand = await _context.Brands.FindAsync(bID);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.BrandID == id);
        }
    }
}
