using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;
using DrivingSchool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

/**
* @(#) HomeController.cs
*/
namespace DrivingSchool.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager<IdentityUser> m_userManager;
        private SignInManager<IdentityUser> m_signInManager;
        private IUserService<User> m_userData;

        public HomeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IUserService<User> userData)
        {
            m_userManager = userManager;
            m_signInManager = signInManager;
            m_userData = userData;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var guid = m_userManager.GetUserId(User);
                if (!(m_userData.Get(guid).Type == UserType.None))
                {
                    return RedirectToAction("Main", "Home");
                }

                m_signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Main()
        {
            var model = m_userData.Get(m_userManager.GetUserId(User)).Type;

            return View(model);
        }
    }
}
