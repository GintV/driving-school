using DrivingSchool.Entities;
using DrivingSchool.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrivingSchool.ViewComponents
{
    public class NavigationBarViewComponent : ViewComponent
    {
        private UserManager<IdentityUser> m_userManager;
        private IUserService<User> m_userData;

        public NavigationBarViewComponent(UserManager<IdentityUser> userManager,
            IUserService<User> userData)
        {
            m_userManager = userManager;
            m_userData = userData;
        }

        public IViewComponentResult Invoke()
        {
            var model = m_userData.Get(m_userManager.GetUserId((ClaimsPrincipal)User));
            return View(model);
        }
    }
}
