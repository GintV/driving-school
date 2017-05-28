using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;
using DrivingSchool.Services;
using DrivingSchool.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using Microsoft.AspNetCore.Authorization;

/**
* @(#) UsersController.cs
*/
namespace DrivingSchool.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<IdentityUser> m_userManager;
        private SignInManager<IdentityUser> m_signInManager;
        private IUserService<User> m_userData;
        private IUserService<Student> m_studentData;
        private IUserService<Instructor> m_instructorData;
        private IDataService<Car> m_carData;

        public UsersController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IUserService<User> userData,
            IUserService<Student> studentData, IUserService<Instructor> instructorData,
            IDataService<Car> carData)
        {
            m_userManager = userManager;
            m_signInManager = signInManager;
            m_userData = userData;
            m_studentData = studentData;
            m_instructorData = instructorData;
            m_carData = carData;
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
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Could not log in.");
            return View(model);
        }

        [Authorize]
        public IActionResult ViewUserList()
        {
            if (IsManager())
            {
                var model = m_userData.GetAll().OrderBy(m => m.Id);
                return View(model);
            }
            return Content("Oops! Nothing to see here.");
        }

        [HttpGet, Authorize]
        public IActionResult EditUserInfo(int? id)
        {
            if (id == null) id = GetCurrentId();
            if (IsManager() || GetCurrentId() == id)
            {
                User u = m_userData.Get((int)id);
                if (u == null) u = m_studentData.Get((int)id);
                if (u == null) u = m_instructorData.Get((int)id);
                Student s = m_studentData.Get((int)id);
                Instructor i = m_instructorData.Get((int)id);

                var model = new UserEditViewModel
                {
                    // User fields
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    BirthDate = u.BirthDate,
                    PersonalNo = u.PersonalNo,
                    Type = u.Type,
                    State = u.State,

                    // Student fields
                    HasTheoryClasses = s != null ? s.HasTheoryClasses : false,
                    PracticeCount = s != null ? s.PracticeCount : 0,
                    
                    // Instructor
                    AssignedCar = i != null ? i.AssignedCar : null,
                    Cars = m_carData.GetAll().OrderBy(m => m.LicensePlate).ThenBy(m => m.Brand).ThenBy(m => m.Model).ToList(),

                    IsManager = IsManager()
                };

                return View(model);
            }
            return Redirect("/");
        }
        [HttpPost, Authorize]
        public IActionResult EditUserInfo(int? id, UserEditViewModel data)
        {
            if (id == null)
                id = GetCurrentId();
            if (data.Id == 0)
                data.Id = (int)id;

            if (IsManager() || GetCurrentId() == id)
            {
                // Validations
                if (!ModelState.IsValid) return View(data);
                ValidateUserData(data, ModelState);
                if (!ModelState.IsValid) return View(data);

                User oldData = m_userData.Get((int)id);
                if (oldData == null) return View(data);

                if (oldData.Type != data.Type)
                {
                    m_userData.Remove(oldData);
                    switch (data.Type)
                    {
                        case UserType.Instructor:
                            m_instructorData.Add(new Instructor {
                                FirstName = data.FirstName,
                                LastName = data.LastName,
                                BirthDate = data.BirthDate,
                                PersonalNo = data.PersonalNo,
                                State = data.State,
                                Type = data.Type,
                                IdentityUser = oldData.IdentityUser,
                                AssignedCar = m_carData.Get(data.AssignedCar.Id)
                            });
                            m_instructorData.SaveChanges();
                            break;
                        case UserType.Student:
                            m_studentData.Add(new Student
                            {
                                FirstName = data.FirstName,
                                LastName = data.LastName,
                                BirthDate = data.BirthDate,
                                PersonalNo = data.PersonalNo,
                                State = data.State,
                                Type = data.Type,
                                IdentityUser = oldData.IdentityUser,
                                HasTheoryClasses = data.HasTheoryClasses,
                                PracticeCount = data.PracticeCount
                            });
                            m_studentData.SaveChanges();
                            break;
                        case UserType.Manager:
                            m_instructorData.Add(new Instructor
                            {
                                FirstName = data.FirstName,
                                LastName = data.LastName,
                                BirthDate = data.BirthDate,
                                PersonalNo = data.PersonalNo,
                                State = data.State,
                                Type = data.Type,
                                IdentityUser = oldData.IdentityUser,
                                AssignedCar = m_carData.Get(data.AssignedCar.Id)
                            });
                            m_instructorData.SaveChanges();
                            break;
                        default:
                            m_userData.Add(new User
                            {
                                FirstName = data.FirstName,
                                LastName = data.LastName,
                                BirthDate = data.BirthDate,
                                PersonalNo = data.PersonalNo,
                                State = data.State,
                                Type = data.Type,
                                IdentityUser = oldData.IdentityUser
                            });
                            m_userData.SaveChanges();
                            break;
                    }
                } else
                {
                    switch (data.Type)
                    {
                        case UserType.Instructor:
                            data.AssignedCar = m_carData.Get(data.AssignedCar.Id);
                            m_instructorData.Update(data);
                            m_instructorData.SaveChanges();
                            break;
                        case UserType.Student:
                            m_studentData.Update(data);
                            m_studentData.SaveChanges();
                            break;
                        case UserType.Manager:
                            data.AssignedCar = m_carData.Get(data.AssignedCar.Id);
                            m_instructorData.Update(data);
                            m_instructorData.SaveChanges();
                            break;
                        default:
                            m_userData.Update(data);
                            m_userData.SaveChanges();
                            break;
                    }
                }
                
                if (IsManager()) return RedirectToAction("ViewUserList");
                return RedirectToAction("Index", "Home");
            }
            return Redirect("/");
        }

        private void ValidateUserData(UserEditViewModel data, ModelStateDictionary state)
        {
            if (data.BirthDate > DateTime.Today)
            {
                state.AddModelError("BirthDate", "Mustn't be a future date");
            }
        }

        private int GetCurrentId() => m_userData.Get(m_userManager.GetUserId(User)).Id;

        private bool IsManager() => m_userData.Get(m_userManager.GetUserId(User))?.
            Type == UserType.Manager;
    }
}