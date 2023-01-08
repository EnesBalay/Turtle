using BussinessLayer.Concrete;
using BussinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Turtle.Controllers
{
    public class RegisterController : Controller
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        [AllowAnonymous]
        public IActionResult Index()
        {
            var userValues = userManager.GetUserByIdentityName(User.Identity.Name);
            if (userValues != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.RegisterSuccess = ' ';
                return View();
            }
                
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(User newUser)
        {
            UserValidator uv = new UserValidator();
            ValidationResult results = uv.Validate(newUser);
            if (results.IsValid)
            {
                User user = new User();
                user.UserName= newUser.UserName;
                user.Name = newUser.Name;
                user.Email = newUser.Email;
                user.Password = newUser.Password;
                user.Surname = newUser.Surname;
                user.AccountType = "User";
                userManager.Add(user);
                ViewBag.RegisterSuccess = "Kayıt Başarılı";
                return View();
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

    }
}
