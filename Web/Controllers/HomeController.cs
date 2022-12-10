using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Web.Models;

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
            //var login = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name,"tuncay")
            //};
            //var userIdentity = new ClaimsIdentity(login,"login");
            //var userPrincipal = new ClaimsPrincipal(userIdentity);
            //await AuthenticationHttpContextExtensions.SignInAsync(HttpContext,userPrincipal);

            //await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}