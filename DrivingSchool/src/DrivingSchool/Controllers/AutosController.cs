using System;
using System.Linq;
using System.Text.RegularExpressions;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;
using Microsoft.AspNetCore.Mvc;
using DrivingSchool.Services;
using DrivingSchool.ViewModels;
using DrivingSchool.ViewModels.Autos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

/**
* @(#) AutosController.cs
*/
namespace DrivingSchool.Controllers
{
    public class AutosController : Controller
    {
        private IDataService<Car> m_carData;
        private IDataService<MileagePointBase> m_mileagePointData;
        private IDataService<Document> m_documentData;
        private IUserService<User> m_userData;
        private UserManager<IdentityUser> m_userManager;

        public AutosController(IDataService<Car> carData,
            IDataService<MileagePointBase> mileagePointData, IDataService<Document> documentData,
            IUserService<User> userData, UserManager<IdentityUser> userManager)
        {
            m_carData = carData;
            m_mileagePointData = mileagePointData;
            m_documentData = documentData;
            m_userData = userData;
            m_userManager = userManager;
        }

        [HttpGet, Authorize]
        public IActionResult ViewCarInfo(int id)
        {
            if (m_userData.Get(m_userManager.GetUserId(User)).Type != UserType.Manager)
            {
                return Content("Oops! Nothing to see here.");
            }
            var c = m_carData.Get(id);
            if (c == null) return View("_Error", new ErrorViewModel
            {
                ErrorMessage = "Requested car was not found.",
                ButtonName = "Go back to Vehicle list",
                ButtonLink = Url.Action("ViewCarList", "Autos")
            });
            var points = m_mileagePointData.GetCarsMileagePoints(c).ToList();
            var docs = m_documentData.GetCarsDocuments(c).ToList();
            var model = new CarViewModel
            {
                Brand = c.Brand,
                Documents = docs,
                Gearbox = c.Gearbox,
                GearboxName = c.Gearbox.GetDescription(),
                Id = c.Id,
                LicensePlate = c.LicensePlate,
                ManufactureDate = c.ManufactureDate,
                Mileage = c.Mileage,
                MileagePoints = points,
                Model = c.Model,
                State = c.State,
                StateName = c.State.GetDescription()
            };
            return View(model);
        }

        [HttpGet, Authorize]
        public IActionResult ViewCarList()
        {
            if (m_userData.Get(m_userManager.GetUserId(User)).Type != UserType.Manager)
            {
                return Content("Oops! Nothing to see here.");
            }
            var model = m_carData.GetAll().OrderBy(m => m.LicensePlate).ThenBy(m => m.Brand).
                ThenBy(m => m.Model);
            return View(model);
        }

        [HttpGet, Authorize]
        public IActionResult Create()
        {
            if (m_userData.Get(m_userManager.GetUserId(User)).Type != UserType.Manager)
            {
                return Content("Oops! Nothing to see here.");
            }
            return View(new CarCreationViewModel
            {
                Brand = "",
                Gearbox = Gearbox.Manual,
                LicensePlate = "",
                ManufactureDate = DateTime.Today,
                Mileage = 0,
                Model = ""
            });
        }

        [HttpPost, Authorize]
        public IActionResult Create(CarCreationViewModel data)
        {
            if (m_userData.Get(m_userManager.GetUserId(User)).Type != UserType.Manager)
            {
                return Content("Oops! Nothing to see here.");
            }
            if (!ModelState.IsValid) return View(data);
            ValidateCarData(data, ModelState);
            if (!ModelState.IsValid) return View(data);
            Car created = new Car
            {
                Brand = data.Brand,
                Gearbox = data.Gearbox,
                LicensePlate = data.LicensePlate,
                ManufactureDate = data.ManufactureDate,
                Mileage = data.Mileage,
                Model = data.Model
            };
            m_carData.Add(created);
            m_documentData.UpdateCarsDocuments(created, data.Documents);
            m_mileagePointData.UpdateCarsMileagePoints(created, data.MileagePoints.Select(p => p as MileagePointBase).ToList());
            m_carData.SaveChanges();
            m_carData.UpdateState(created.Id);
            m_carData.SaveChanges();
            return RedirectToAction("ViewCarList");
        }

        [HttpGet, Authorize]
        public IActionResult EditCarInfo(int id)
        {
            if (m_userData.Get(m_userManager.GetUserId(User)).Type != UserType.Manager)
            {
                return Content("Oops! Nothing to see here.");
            }
            var c = m_carData.Get(id);
            if (c == null)
            {
                return View("_Error", new ErrorViewModel
                {
                    ErrorMessage = "Requested car was not found.",
                    ButtonName = "Go back to Vehicle list",
                    ButtonLink = Url.Action("ViewCarList", "Autos")
                });
            }
            var points = m_mileagePointData.GetCarsMileagePoints(c).Select(p => p as MileagePoint).ToList();
            var docs = m_documentData.GetCarsDocuments(c).ToList();
            var model = new CarEditViewModel
            {
                Brand = c.Brand,
                Documents = docs,
                Gearbox = c.Gearbox,
                Id = c.Id,
                LicensePlate = c.LicensePlate,
                ManufactureDate = c.ManufactureDate,
                Mileage = c.Mileage,
                MileagePoints = points,
                Model = c.Model,
                State = c.State,
                StateName = c.State.GetDescription()
            };
            return View(model);
        }
        [HttpPost, Authorize]
        public IActionResult EditCarInfo(int id, CarEditViewModel data)
        {
            if (m_userData.Get(m_userManager.GetUserId(User)).Type != UserType.Manager)
            {
                return Content("Oops! Nothing to see here.");
            }
            if (!ModelState.IsValid) return View(data);
            ValidateCarData(data, ModelState);
            if (!ModelState.IsValid) return View(data);
            Car modified = m_carData.Get(id);
            if (modified == null) return View(data);
            m_carData.Update(data);
            m_documentData.UpdateCarsDocuments(modified, data.Documents);
            m_mileagePointData.UpdateCarsMileagePoints(modified, data.MileagePoints?.Select(p => p as MileagePointBase).ToList());
            m_carData.SaveChanges();
            m_carData.UpdateState(id);
            m_carData.SaveChanges();
            return RedirectToAction("ViewCarInfo", new { id = data.Id });
        }

        private void ValidateCarData(CarEditViewModel data, ModelStateDictionary state)
        {
            if (data.ManufactureDate > DateTime.Today)
            {
                state.AddModelError("ManufactureDate", "Mustn't be a future date");
            }
            if (!new Regex("[A-Z]{3}[' ']?[0-9]{3}").IsMatch(data.LicensePlate))
            {
                state.AddModelError("LicensePlate", "Must be in format XXX 000");
            }
            if (data.Documents != null)
            {
                for (var i = 0; i < data.Documents.Count; i++)
                {
                    if (data.Documents[i].StartDate > data.Documents[i].EndDate)
                    {
                        state.AddModelError("Documents[" + i + "].StartDate",
                            "Start date mustn't be greater than end date");
                    }
                }
            }
            if (data.MileagePoints != null)
            {
                for (var i = 0; i < data.MileagePoints.Count; i++)
                {
                    if (data.MileagePoints[i].Mileage < 0)
                    {
                        state.AddModelError("MileagePoints[" + i + "].Mileage",
                            "Mileage must be a positive integer");
                    }
                }
            }
        }

        // TODO interface? merge views?
        private void ValidateCarData(CarCreationViewModel data, ModelStateDictionary state)
        {
            if (data.ManufactureDate > DateTime.Today)
            {
                state.AddModelError("ManufactureDate", "Mustn't be a future date");
            }
            if (!new Regex("[A-Z]{3}[' ']?[0-9]{3}").IsMatch(data.LicensePlate))
            {
                state.AddModelError("LicensePlate", "Must be in format XXX 000");
            }
            if (data.Documents != null)
            {
                for (var i = 0; i < data.Documents.Count; i++)
                {
                    if (data.Documents[i].StartDate > data.Documents[i].EndDate)
                    {
                        state.AddModelError("Documents[" + i + "].StartDate",
                            "Start date mustn't be greater than end date");
                    }
                }
            }
            if (data.MileagePoints != null)
            {
                for (var i = 0; i < data.MileagePoints.Count; i++)
                {
                    if (data.MileagePoints[i].Mileage < 0)
                    {
                        state.AddModelError("MileagePoints[" + i + "].Mileage",
                            "Mileage must be a positive integer");
                    }
                }
            }
        }
    }
}
