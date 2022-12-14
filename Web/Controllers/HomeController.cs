using Business.Abstract;
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
        private readonly IDoctorService _doctorService;

        public HomeController(ILogger<HomeController> logger, IDoctorService doctorService = null)
        {
            _logger = logger;
            _doctorService = doctorService;
        }

        public IActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginResult = _doctorService.Login(model.UserName,model.UserPassword);
            if (loginResult != null)
            {
                var login = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginResult.DoctorName),
                    new Claim(ClaimTypes.Surname, loginResult.DoctorLastName),
                    new Claim(ClaimTypes.Role, "Doctor"),
                    new Claim(ClaimTypes.Email, loginResult.DoctorEmail),
                    new Claim("Id",loginResult.DoctorId.ToString())
                };
                var userIdentity = new ClaimsIdentity(login, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(userIdentity);
                var authProperties = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                return RedirectToAction("Index","Doctor");
            }
            ModelState.AddModelError("UserName", "Please check email or password");
            return View(model);
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