
using GreenAgriHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System;
using GreenAgriHub.Models;

namespace GreenAgriHub.Controllers
{
 
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmerController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
           
            var farmerId = User.FindFirst("FarmerId")?.Value;
            if (farmerId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            
            int id = int.Parse(farmerId);
            var farmer = _context.Farmers 
                .Include(f => f.Products) 
                .FirstOrDefault(f => f.FarmerId == id);

            return View(farmer);

        }
      
        public IActionResult AddProduct()
        {
            
            string currentFarmerId = User.FindFirst("FarmerId")?.Value;
            
            if (string.IsNullOrEmpty(currentFarmerId))
            {
                return RedirectToAction("Login", "Auth");
            }
            
            var product = new Product
            {
                FarmerId = int.Parse(currentFarmerId),
                ProductDate = DateTime.Now
            };

            return View(product);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(Product product)
        {
            Console.WriteLine($"Recieved Data: {product.ProductName}, {product.ProductCategory}, {product.ProductPrice}, {product.ProductDate}, {product.FarmerId}");

            string currentFarmerId = User.FindFirst("FarmerId")?.Value;
            if (!string.IsNullOrEmpty(currentFarmerId))
            {
                product.FarmerId = int.Parse(currentFarmerId);
            }
            
            ModelState.Remove("Farmer");
            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ProductDate == DateTime.MinValue)
                    {
                        product.ProductDate = DateTime.Now;
                    }

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Product added successfully!";
                    return RedirectToAction("ViewMyProducts", new { farmerId = product.FarmerId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + ex.Message);
                }
            }
            return View(product);
        }
   
        public async Task<IActionResult> ViewMyProducts(int farmerId)
        {
            var farmer = await _context.Farmers
                .Include(f => f.Products)
                .FirstOrDefaultAsync(f => f.FarmerId == farmerId);

            if (farmer == null)
            {
                return NotFound();
            }

            string currentFarmerId = User.FindFirst("FarmerId")?.Value;
            if (!string.IsNullOrEmpty(currentFarmerId) && currentFarmerId != farmerId.ToString())
            {
                return Forbid();
            }

            ViewBag.FarmerName = $"{farmer.FarmerName} {farmer.FarmerSurname}";
            return View(farmer.Products.OrderByDescending(p => p.ProductDate).ToList());
        }
       
        public async Task<IActionResult> Marketplace()
        {
            var products = await _context.Products
                .Include(p => p.Farmer)
                .ToListAsync();

            return View(products);
        }
       
    }
}
