using System;
using System.Collections.Generic;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using DrivingSchool.Entities.Enumerations;
using Microsoft.EntityFrameworkCore;

/**
* @(#) CarData.cs
*/
namespace DrivingSchool.Services
{
    public class CarData : DataService<Car>
    {
        public CarData(DrivingSchoolDbContext context) : base(context) { }

        public override Car Get(int id) => m_context.Cars.FirstOrDefault(s => s.Id == id);

        public override IQueryable<Car> GetAll() => m_context.Cars.Include(c => c.Documents).
            Include(c => c.MileagePoints);

        public override void RemoveRange(IEnumerable<Car> data) => m_context.Cars.
            RemoveRange(data);
    }

    public static class CarDataServiceExtensions
    {
        public static void Update(this IDataService<Car> carData,
            ViewModels.Autos.CarEditViewModel data)
        {
            var toUpdate = carData.GetAll().FirstOrDefault(s => s.Id == data.Id);
            if (toUpdate != null)
            {
                toUpdate.LicensePlate = data.LicensePlate;
                toUpdate.Brand = data.Brand;
                toUpdate.Model = data.Model;
                toUpdate.ManufactureDate = data.ManufactureDate;
                toUpdate.Mileage = data.Mileage;
                toUpdate.Gearbox = data.Gearbox;
            }
        }

        public static void UpdateState(this IDataService<Car> carData, int id)
        {
            var car = carData.GetAll().FirstOrDefault(c => c.Id == id);
            if (car == null) return;
            var currentDate = DateTime.Today;
            if (car.Documents.
                FirstOrDefault(d => d.Type == DocumentType.ServiceApprovalOrder &&
                d.EndDate > currentDate && d.StartDate <= currentDate) != null)
            {
                car.State = CarState.InService;
            }
            else if (car.Documents.
                     FirstOrDefault(d => d.Type == DocumentType.UsageSuspensionOrder &&
                     d.EndDate > currentDate && d.StartDate <= currentDate) != null)
            {
                car.State = CarState.Unused;

            }
            else if (car.Documents.
                     FirstOrDefault(d => d.Type == DocumentType.InsurancePolicy &&
                     d.StartDate <= currentDate &&
                     d.EndDate > currentDate) == null ||
                     car.Documents.
                     FirstOrDefault(d => d.Type == DocumentType.TechnicalInspection &&
                     d.StartDate <= currentDate &&
                     d.EndDate > currentDate) == null)
            {
                car.State = CarState.NonOperational;
            }
            else
            {
                car.State = car.MileagePoints.FirstOrDefault(p => p.Mileage < car.Mileage) == null
                    ? CarState.Operational
                    : CarState.RequiresService;
            }
        }
    }
}
