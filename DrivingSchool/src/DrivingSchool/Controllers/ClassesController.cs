using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;
using DrivingSchool.Services;
using DrivingSchool.ViewModels.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

/**
* @(#) ClassesController.cs
*/
namespace DrivingSchool.Controllers
{
    [Authorize]
    public class ClassesController : Controller
    {
        private UserManager<IdentityUser> m_userManager;
        private IDataService<Class> m_classData;
        private IDataService<TheoryClasses> m_theoryClassesData;
        private IUserService<Instructor> m_instructorData;

        public ClassesController(UserManager<IdentityUser> userManager,
            IDataService<Class> classData, IDataService<TheoryClasses> theoryClassesData,
            IUserService<Instructor> instructorData)
        {
            m_userManager = userManager;
            m_classData = classData;
            m_theoryClassesData = theoryClassesData;
            m_instructorData = instructorData;
        }

        [HttpGet]
        public IActionResult ClassCreation()
        {
            var model = new ClassCreationViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult ClassCreation(ClassCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ClassType == ClassType.TheoryClasses)
                {
                    if (model.NumberOfSeats == null || model.Weeks == null)
                    {
                        ModelState.AddModelError("", "Number of Seats and weaks to Repeat are" +
                            " required fields for theory classes");
                        return View(model);
                    }

                    m_theoryClassesData.Add(new TheoryClasses
                    {
                        IsMain = true,
                        Date = model.Date,
                        StartTime = model.StartTime,
                        EndTime = model.EndTime,
                        Type = model.ClassType,
                        State = ClassState.New,
                        Seats = (int)model.NumberOfSeats,
                        Weeks = (int)model.Weeks,
                        Instructor = m_instructorData.Get(m_userManager.GetUserId(User))
                    });
                }
                else
                {
                    m_classData.Add(new Class
                    {
                        Date = model.Date,
                        StartTime = model.StartTime,
                        EndTime = model.EndTime,
                        Type = model.ClassType,
                        State = ClassState.New,
                        Instructor = m_instructorData.Get(m_userManager.GetUserId(User))
                    });
                }

                m_classData.SaveChanges();

                return RedirectToAction("");
            }

            return View(model);
        }
    }
}
