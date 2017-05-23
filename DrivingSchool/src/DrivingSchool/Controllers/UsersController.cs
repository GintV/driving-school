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
            return Redirect("/");
        }

        [HttpGet, Authorize]
        public IActionResult EditUserInfo(int? id)
        {
            if (id == null) id = GetCurrentId();
            if (IsManager() || GetCurrentId() == id)
            {
                var u = m_userData.Get((int)id);
                var model = new UserEditViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    BirthDate = u.BirthDate,
                    PersonalNo = u.PersonalNo,
                    Type = u.Type,
                    State = u.State,
                    StateName = u.State.GetDescription()
                };
                return View(model);
            }
            return Redirect("/");
        }
        [HttpPost, Authorize]
        public IActionResult EditUserInfo(int? id, UserEditViewModel data)
        {
            if (id == null) id = GetCurrentId();
            if (data.Id == 0) data.Id = (int)id;
            if (IsManager() || GetCurrentId() == id)
            {
                if (!ModelState.IsValid) return View(data);
                ValidateUserData(data, ModelState);
                if (!ModelState.IsValid) return View(data);
                User modified = m_userData.Get((int)id);
                if (modified == null) return View(data);
                ((UserData)m_userData).Update(data);
                m_userData.SaveChanges();
                return RedirectToAction("ViewUserList");
            }
            return Redirect("/");
        }

        public void ValidateUserData(UserEditViewModel data, ModelStateDictionary state)
        {
            if (data.BirthDate > DateTime.Today)
            {
                state.AddModelError("BirthDate", "Mustn't be a future date");
            }
        }

        public int GetCurrentId()
        {
            var currentUserId = m_userManager.GetUserId(User);
            return ((UserData)m_userData).Get(currentUserId).Id;
        }

        public bool IsManager()
        {
            var currentUserId = m_userManager.GetUserId(User);
            if(currentUserId != null)
                return ((UserData)m_userData).Get(currentUserId).Type == UserType.Manager;
                
            return false;
        }
    }
}