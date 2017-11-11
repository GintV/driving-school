using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;
using DrivingSchool.Services;
using DrivingSchool.ViewModels.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        private readonly IUserService<User> m_userData;

        public ClassesController(UserManager<IdentityUser> userManager,
            IDataService<Class> classData, IDataService<TheoryClasses> theoryClassesData,
            IUserService<Instructor> instructorData, IUserService<User> userData)
        {
            m_userManager = userManager;
            m_classData = classData;
            m_theoryClassesData = theoryClassesData;
            m_instructorData = instructorData;
            m_userData = userData;
        }

        [HttpGet]
        public IActionResult ClassCreation()
        {
            var userType = m_userData.Get(m_userManager.GetUserId(User)).Type;
            if (userType != UserType.Instructor || userType != UserType.Manager)
                return NotFound();

            var backAction = Request.Headers["Referer"].ToString().Split('/').Reverse().First();

            var model = new ClassCreationViewModel
            {
                BackAction = backAction == "ScheduleView" ? "ScheduleView" :
                    backAction == "ScheduleClassList" ? "ScheduleClassList" : "ScheduleEntry"
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ClassCreation(ClassCreationViewModel model, string submit)
        {
            var userType = m_userData.Get(m_userManager.GetUserId(User)).Type;
            if (userType != UserType.Instructor || userType != UserType.Manager)
                return NotFound();

            if (submit == "Generate")
                return RedirectToAction("ScheduleClassList", "Schedule");

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

                return RedirectToAction("ScheduleClassList", "Schedule");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult GenerateTemplateClasses() =>
            RedirectToAction("ScheduleClassList", "Schedule");
    }
}
