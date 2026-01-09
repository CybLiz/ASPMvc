using CaisseEnregistreuse.Service;
using Microsoft.AspNetCore.Mvc;

namespace CaisseEnregistreuse.Controllers;
public class UserController : Controller
{
    private readonly UserService _userService;
    private readonly JwtService _jwtService;

    public UserController(UserService userService, JwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _userService.Authenticate(email, password);

        if (user == null)
        {
            // création 
            user = _userService.Register(email.Split('@')[0], email, password);
        }

        var token = _jwtService.GenerateToken(
            user.Id.ToString(),
            user.Email,
            user.Role

        );

        // Stocker le JWT dans un cookie HTTP
        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });

        if (user.Role == "Admin")
        {
            return RedirectToAction("Index", "Product");
        }
        else
        {
            return RedirectToAction("Index", "Product");
        }
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return RedirectToAction("Login");
    }
}
