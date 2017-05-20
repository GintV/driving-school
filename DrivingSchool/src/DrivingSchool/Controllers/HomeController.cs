using DrivingSchool.Entities;
using DrivingSchool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/**
* @(#) HomeController.cs
*/
namespace DrivingSchool.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IDataService<User> m_userData;

        public HomeController(IDataService<User> userData)
        {
            m_userData = userData;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Main", "Home");
            }
            
            return View();
        }

        public IActionResult Main()
        {
            var model = m_userData.GetAll();

            return View(model);
        }
    }
}
