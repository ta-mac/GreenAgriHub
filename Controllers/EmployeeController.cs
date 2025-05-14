using GreenAgriHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using GreenAgriHub.Models;

namespace GreenAgriHub.Controllers
{
   
    [Authorize(Roles = "Employee")]
    
    public class EmployeeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult AddFarmer()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFarmer(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Farmers.Add(farmer);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Farmer added successfully!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + ex.Message);
                }
            }
            return View(farmer);
        }
       
        public async Task<IActionResult> ViewProducts(string searchCategory = null,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            var productsQuery = _context.Products
                .Include(p => p.Farmer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchCategory))
            {
                productsQuery = productsQuery.Where(p => p.ProductCategory.ToLower().Contains(searchCategory.ToLower()));
            }

            if (startDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductDate <= endDate.Value);
            }

            var products = await productsQuery.OrderByDescending(p => p.ProductDate).ToListAsync();

            ViewBag.Categories = _context.Products
                .Select(p => p.ProductCategory)
                .Distinct()
                .ToList();

            ViewBag.CurrentCategory = searchCategory;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(products);
        }
       
    }
}
