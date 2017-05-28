using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;
using DrivingSchool.Services;
using DrivingSchool.ViewModels.Schedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
* @(#) ScheduleController.cs
*/
namespace DrivingSchool.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private UserManager<IdentityUser> m_userManager;
        private IUserService<Instructor> m_instructorData;
        private IUserService<Student> m_studentData;
        private IUserService<User> m_userData;
        private IDataService<Class> m_classData;
        private IDataService<TheoryClasses> m_theoryClasses;

        public ScheduleController(UserManager<IdentityUser> userManager,
            IUserService<Instructor> instructorData, IUserService<Student> studentData,
            IUserService<User> userData, IDataService<Class> classData,
            IDataService<TheoryClasses> theoryClasses)
        {
            m_userManager = userManager;
            m_instructorData = instructorData;
            m_studentData = studentData;
            m_userData = userData;
            m_classData = classData;
            m_theoryClasses = theoryClasses;
        }

        [HttpGet]
        public IActionResult ScheduleClassList()
        {
            var guid = m_userManager.GetUserId(User);

            var user = m_studentData.Get(guid);
            List<Class> classes;
            if (user != null)
            {
                classes = user.AttendedClasses;
                if (user.TheoryClasses != null)
                {
                    classes.Add(user.TheoryClasses);
                }
            }
            else
            {
                classes = m_instructorData.Get(guid).TaughtClasses;
            }

            var model = new ScheduleClassListViewModel
            {
                UserType = m_userData.Get(m_userManager.GetUserId(User)).Type,
                List = new List<ScheduleClassListViewModel.ScheduleClassList>()
            };

            foreach (var @class in classes)
            {
                var atendee = model.UserType == UserType.Student ? @class.Instructor.ToString() :
                    @class.Type != ClassType.TheoryClasses ?
                    (@class.Student != null ? @class.Student.ToString() : "N/A") :
                    ((TheoryClasses)@class).Students != null ? "Some" : "N/A";

                model.List.Add(new ScheduleClassListViewModel.ScheduleClassList
                {
                    Id = @class.Id,
                    ClassType = @class.Type,
                    Date = @class.Date,
                    StartTime = @class.StartTime,
                    EndTime = @class.EndTime,
                    Atendee = atendee
                });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ScheduleEntry()
        {
            var model = m_userData.Get(m_userManager.GetUserId(User)).Type;
            return View(model);
        }

        [HttpGet]
        public IActionResult ScheduleView()
        {
            var guid = m_userManager.GetUserId(User);

            var user = m_studentData.Get(guid);
            List<Class> classes;
            if (user != null)
            {
                classes = user.AttendedClasses;
                if (user.TheoryClasses != null)
                {
                    classes.Add(user.TheoryClasses);
                }
            }
            else
            {
                classes = m_instructorData.Get(guid).TaughtClasses;
            }

            var model = new ScheduleViewModel
            {
                UserType = m_userData.Get(guid).Type,
                List = new List<ScheduleClass>()
            };

            foreach (var @class in classes)
            {
                var weeks = @class.Type == ClassType.TheoryClasses ?
                    ((TheoryClasses)@class).Weeks : 1;

                for (var i = 0; i < weeks; ++i)
                {

                    var date = @class.Date.AddDays(7 * i);

                    model.List.Add(new ScheduleClass
                    {
                        Title = $"{@class.Type} \n" +
                            $@"<a href=/Classes/ClassView/{@class.Id}>Details</a>",
                        EventColor = @class.Type == ClassType.TheoryClasses ? "#F74B30" :
                            "#3E94FB",
                        Start = new Date
                        {
                            Year = date.Year.ToString(),
                            Month = date.Month.ToString(),
                            Day = date.Day.ToString(),
                            Time = @class.StartTime.TimeOfDay.ToString()
                        }.ToString(),
                        End = new Date
                        {
                            Year = date.Year.ToString(),
                            Month = date.Month.ToString(),
                            Day = date.Day.ToString(),
                            Time = @class.EndTime.TimeOfDay.ToString()
                        }.ToString()
                    });
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ScheduleChoise()
        {
            var guid = m_userManager.GetUserId(User);

            var availablePracticeClasses = m_classData.GetAll().
                Where(c => c.Type == ClassType.PracticeDrive && c.State == ClassState.New);
            var selectedPracticeClasses = m_studentData.Get(guid).
                AttendedClasses.Where(c => c.Type == ClassType.PracticeDrive);

            var theoryClasses = m_theoryClasses.GetAll().Where(c => c.Seats > 0);
            var selectedTheoryClasses = m_studentData.Get(guid).TheoryClasses;

            var practiceExams = m_classData.GetAll().Where(c => c.Type == ClassType.PracticeExam);
            var selectedPracticeExam = m_studentData.Get(guid).
                AttendedClasses.Where(c => c.Type == ClassType.PracticeExam).FirstOrDefault();

            var theoryExams = m_classData.GetAll().Where(c => c.Type == ClassType.TheoryExam);
            var selectedTheoryExam = m_studentData.Get(guid).
                AttendedClasses.Where(c => c.Type == ClassType.TheoryExam).FirstOrDefault();

            var backAction = Request.Headers["Referer"].ToString().Split('/').Reverse().First();

            var model = new ScheduleChoiseViewModel
            {
                SelectedClasses = new SelectList(selectedPracticeClasses),
                AvailableClasses = new SelectList(availablePracticeClasses),
                TheoryClasses = new SelectList(new[] { "None" }.
                    Concat(theoryClasses.Select(c => c.ToString()))),
                TheoryClassesId = selectedTheoryClasses?.ToString() ?? "None",
                PracticeExam = new SelectList(new[] { "None" }.
                    Concat(practiceExams.Select(c => c.ToString()))),
                PracticeExamId = selectedPracticeExam?.ToString() ?? "None",
                TheoryExam = new SelectList(new[] { "None" }.
                    Concat(theoryExams.Select(c => c.ToString()))),
                TheoryExamId = selectedTheoryExam?.ToString() ?? "None",
                BackAction = backAction == "ScheduleView" ? "ScheduleView" :
                    backAction == "ScheduleClassList" ? "ScheduleClassList" : "ScheduleEntry"
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleChoise(ScheduleChoiseViewModel model,
            string submit)
        {
            var guid = m_userManager.GetUserId(User);
            var user = m_studentData.Get(guid);

            if (submit == "Select")
                await Task.Run(() =>
                {
                    int classId;
                    if (int.TryParse(model.AvailableClassesId?.Split('.')[0], out classId))
                    {
                        var @class = m_classData.Get(classId);
                        @class.Student = user;
                        @class.State = ClassState.Locked;
                    }

                    m_classData.SaveChanges();
                    return RedirectToAction("ScheduleChoise", "Schedule");
                });
            else if (submit == "Deselect")
                await Task.Run(() =>
                {
                    int classId;
                    if (int.TryParse(model.SelectedClassesId?.Split('.')[0], out classId))
                    {
                        var @class = m_classData.Get(classId);
                        @class.Student = null;
                        @class.State = ClassState.New;
                    }

                    m_classData.SaveChanges();
                    return RedirectToAction("ScheduleChoise", "Schedule");
                });
            else
            {
                await Task.Run(() =>
                {
                    int theoryClassesId;
                    if (user.TheoryClasses != null)
                    {
                        user.TheoryClasses.Seats++;
                    }

                    if (int.TryParse(model.TheoryClassesId?.Split('.')[0], out theoryClassesId))
                    {
                        var @class = m_theoryClasses.Get(theoryClassesId);
                        @class.Seats--;
                        @class.State = @class.Seats == 0 ? ClassState.Locked : ClassState.New;
                        user.TheoryClasses = @class;
                    }
                    else
                    {
                        user.TheoryClasses = null;
                    }
                });

                await Task.Run(() =>
                {
                    int practiceExamId;
                    var currentClass = user.AttendedClasses.
                        Where(c => c.Type == ClassType.PracticeExam).FirstOrDefault();

                    if (currentClass != null)
                    {
                        currentClass.State = ClassState.New;
                        currentClass.Student = null;
                    }

                    if (int.TryParse(model.PracticeExamId?.Split('.')[0], out practiceExamId))
                    {
                        var @class = m_classData.Get(practiceExamId);
                        @class.Student = user;
                        @class.State = ClassState.Locked;
                    }
                });

                await Task.Run(() =>
                {
                    int theoryExamId;
                    var currentClass = user.AttendedClasses.
                        Where(c => c.Type == ClassType.TheoryExam).FirstOrDefault();

                    if (currentClass != null)
                    {
                        currentClass.State = ClassState.New;
                        currentClass.Student = null;
                    }

                    if (int.TryParse(model.TheoryExamId?.Split('.')[0], out theoryExamId))
                    {
                        var @class = m_classData.Get(theoryExamId);
                        @class.Student = user;
                        @class.State = ClassState.Locked;
                    }
                });
            }


            m_classData.SaveChanges();
            return RedirectToAction("ScheduleClassList", "Schedule");
        }
    }
}
