using BussinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Turtle.Controllers
{
    public class LoginController : Controller
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        [AllowAnonymous]
        public IActionResult Index()
        {
            var userValues = userManager.GetUserByIdentityName(User.Identity.Name);
            if (userValues == null)
            {
                return View();

            }
            else if (userValues.AccountType == "Admin")
            {
                return RedirectToAction("Privacy", "Home", new { area = "Admin" });

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(User p)
        {
            Context c = new Context();
            var datavalue = c.Users.FirstOrDefault(x => x.UserName == p.UserName && x.Password == p.Password);
            if (datavalue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, p.UserName)
                };
                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                ViewBag.LoginSuccessUser = "Giriş Başarılı!";
                ViewBag.AccountType = "User";
                ViewBag.UsernameUser = p.UserName;
            }else{
                    ViewBag.LoginErrorUser = "Kullanıcı adı ya da şifre hatalı!";
                    ViewBag.AccountType = "User";
                    ViewBag.UsernameUser = p.UserName;

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        public IActionResult NoAuthorize()
        {
            return View();
        }
    }
}
