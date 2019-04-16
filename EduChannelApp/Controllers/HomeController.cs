using EduChannel.Domain;
using EduChannelApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EduChannelApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public HomeController(UserManager<AppUser> _userManager)
        {
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginModel.UserName);

                if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var identity = new ClaimsIdentity("cookies");

                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError("", "Invalid Username or Password");

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByNameAsync(register.UserName);

                if (user == null)
                {
                    var result = await userManager.CreateAsync(new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = register.UserName
                    }, register.Password);
                }

                return View("Success");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
