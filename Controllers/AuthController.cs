
using Microsoft.AspNetCore.Mvc;
using GreenAgriHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GreenAgriHub.Controllers
{
    //Controller for handling authentication
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public IActionResult Login()
        {
            //Check if the user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Employee"))
                {
                    return RedirectToAction("Index", "Employee");
                }
                else if (User.IsInRole("Farmer"))
                {
                    return RedirectToAction("Index", "Farmer");
                }
            }
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string userRole)
        {
            //Check if the user is already authenticated
            if (userRole == "Employee")
            {
                //Find the employee by email
                var employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeEmail == email);

                //Check if the employee exists and the password is correct
                if (employee != null && employee.EmployeePassword == password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, employee.EmployeeName),
                        new Claim(ClaimTypes.Email, employee.EmployeeEmail),
                        new Claim(ClaimTypes.Role, "Employee"),
                        new Claim("EmployeeId", employee.EmployeeId.ToString())
                    };

                    //Create claim identity and sign in the user
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Sign in the user with the claims identity
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    // Redirect to the employee index page
                    return RedirectToAction("Index", "Employee");
                }
            }           
            else if (userRole == "Farmer")
            {
                var farmer = await _context.Farmers
                    .FirstOrDefaultAsync(f => f.FarmerEmail == email);

                if (farmer != null && farmer.FarmerPassword == password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, farmer.FarmerName),
                        new Claim(ClaimTypes.Email, farmer.FarmerEmail),
                        new Claim(ClaimTypes.Role, "Farmer"),
                        new Claim("FarmerId", farmer.FarmerId.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Farmer", new { FarmerId = farmer.FarmerId });
                }
            }
            //Retur to login view if login fails
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();

        }
        
        //Logout method
        public async Task<IActionResult> Logout()
        {
            //Sign 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        
    }
}
