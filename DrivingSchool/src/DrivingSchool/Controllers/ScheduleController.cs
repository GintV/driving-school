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
        private IDataService<Instructor> m_instructorData;
        private IDataService<Student> m_studentData;
        private IDataService<Class> m_classData;
        private IDataService<TheoryClasses> m_theoryClasses;

        public ScheduleController(UserManager<IdentityUser> userManager,
            IDataService<Instructor> instructorData, IDataService<Student> studentData,
            IDataService<Class> classData, IDataService<TheoryClasses> theoryClasses)
        {
            m_userManager = userManager;
            m_instructorData = instructorData;
            m_studentData = studentData;
            m_classData = classData;
            m_theoryClasses = theoryClasses;
        }

        [HttpGet]
        public IActionResult ScheduleEntry()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ScheduleView()
        {
            var guid = m_userManager.GetUserId(User);

            var user = ((StudentData)m_studentData).Get(guid);
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
                classes = ((InstructorData)m_instructorData).Get(guid).TaughtClasses;
            }

            var model = new List<ScheduleViewModel>();

            foreach (var @class in classes)
            {
                var weeks = @class.Type == ClassType.TheoryClasses ?
                    ((TheoryClasses)@class).Weeks : 1;

                for (var i = 0; i < weeks; ++i)
                {

                    var date = @class.Date.AddDays(7 * i);

                    model.Add(new ScheduleViewModel
                    {
                        Title = $"{@class.Type} \n Details",
                        EventColor = @class.Type == ClassType.TheoryClasses ? "#F74B30" : "#3E94FB",
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
            var selectedPracticeClasses = ((StudentData)m_studentData).Get(guid).
                AttendedClasses.Where(c => c.Type == ClassType.PracticeDrive);

            var theoryClasses = m_theoryClasses.GetAll().Where(c => c.Seats > 0);
            var selectedTheoryClasses = ((StudentData)m_studentData).Get(guid).TheoryClasses;

            var practiceExams = m_classData.GetAll().Where(c => c.Type == ClassType.PracticeExam);
            var selectedPracticeExam = ((StudentData)m_studentData).Get(guid).
                AttendedClasses.Where(c => c.Type == ClassType.PracticeExam).FirstOrDefault();

            var theoryExams = m_classData.GetAll().Where(c => c.Type == ClassType.TheoryExam);
            var selectedTheoryExam = ((StudentData)m_studentData).Get(guid).
                AttendedClasses.Where(c => c.Type == ClassType.TheoryExam).FirstOrDefault();

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
                TheoryExamId = selectedTheoryExam?.ToString() ?? "None"
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleChoise(ScheduleChoiseViewModel model,
            string submit)
        {
            var guid = m_userManager.GetUserId(User);
            var user = ((StudentData)m_studentData).Get(guid);

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
            return RedirectToAction("ScheduleChoise", "Schedule");
        }


    }
}
