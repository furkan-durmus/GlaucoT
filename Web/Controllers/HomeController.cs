using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Web.Models;
using Web.Models.Home;

namespace Web.Controllers
{
    public class HomeController : Controller // test
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            //var login = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name,model.PatientUserName),
            //    new Claim(ClaimTypes.Role, "Admin")
            //};
            //var userIdentity = new ClaimsIdentity(login, CookieAuthenticationDefaults.AuthenticationScheme);
            //var principal = new ClaimsPrincipal(userIdentity);
            //var authProperties = new AuthenticationProperties();
            //HttpConte
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}