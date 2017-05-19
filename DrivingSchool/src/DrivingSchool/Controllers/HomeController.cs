using DrivingSchool.Services;
using Microsoft.AspNetCore.Mvc;

/**
* @(#) HomeController.cs
*/
namespace DrivingSchool.Controllers
{
    public class HomeController : Controller
    {
        private StudentData m_studentData;

        public HomeController(StudentData studentData)
        {
            m_studentData = studentData;
        }
        public IActionResult Index()
        {
            var model = m_studentData.GetAll();

            return View(model);
        }

        public void ShowMainMenu()
        {

        }
    }
}
