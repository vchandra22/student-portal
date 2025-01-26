using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models.Entity;

namespace StudentPortal.Web.Controllers;

public class AuthController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public AuthController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Username == username);

        if (user is null)
        {
            ViewBag.Error = "Invalid username or password.";
            return View();
        }
        
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            ViewBag.Error = "Invalid username or password.";
            return View();
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
        };
    
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
        };
    
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return RedirectToAction("Login", "Auth");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(string username, string password)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new Users()
        {
            Username = username,
            Password = hashedPassword,
        };
        
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        
        return RedirectToAction("Login");
    }
}