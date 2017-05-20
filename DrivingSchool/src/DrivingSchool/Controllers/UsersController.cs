using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;
using DrivingSchool.Services;
using DrivingSchool.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/**
* @(#) UsersController.cs
*/
namespace DrivingSchool.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<IdentityUser> m_userManager;
        private SignInManager<IdentityUser> m_signInManager;
        private IDataService<User> m_userData;

        public UsersController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IDataService<User> userData)
        {
            m_userManager = userManager;
            m_signInManager = signInManager;
            m_userData = userData;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser(model.UserName);
                var result = await m_userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    m_userData.Add(new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = model.BirthDate,
                        PersonalNo = model.PersonalNo,
                        State = UserState.Unverified,
                        Type = UserType.None,
                        IdentityUser = user
                    });

                    m_userData.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await m_signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await m_signInManager.PasswordSignInAsync(model.Username,
                    model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Main", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Could not log in.");
            return View(model);
        }
    }
}