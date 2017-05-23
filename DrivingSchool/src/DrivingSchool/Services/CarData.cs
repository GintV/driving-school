using System;
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

        public override Car Get(int id) =>
            m_context.Cars.FirstOrDefault(s => s.Id == id);

        public override IQueryable<Car> GetAll() =>
            m_context.Cars;



        public void Update(ViewModels.Autos.CarEditViewModel data)
        {
            var toUpdate = m_context.Cars.FirstOrDefault(s => s.Id == data.Id);
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

        public void GetCarsForCarUsage()
        {
            throw new NotImplementedException();
        }

        public void UpdateState(int id)
        {
            var car = m_context.Cars.Include(c => c.Documents).Include(c => c.MileagePoints)
                .FirstOrDefault(c => c.Id == id);
            if (car == null) return;
            if (car.Documents.FirstOrDefault(d => d.Type == DocumentType.ServiceApprovalOrder &&
                                                  d.EndDate > DateTime.Today) == null)
            {
                if (car.Documents.FirstOrDefault(d => d.Type == DocumentType.UsageSuspensionOrder &&
                                                      d.EndDate > DateTime.Today) == null)
                {
                    if (car.Documents.FirstOrDefault(d => (d.Type == DocumentType.InsurancePolicy ||
                                                           d.Type == DocumentType.TechnicalInspection) &&
                                                          d.EndDate < DateTime.Today) == null)
                    {
                        car.State = car.MileagePoints.FirstOrDefault(p => p.Mileage < car.Mileage) == null ? CarState.Operational : CarState.RequiresService;
                    }
                    else
                    {
                        car.State = CarState.NonOperational;
                    }
                }
                else
                {
                    car.State = CarState.Unused;
                }
            }
            else
            {
                car.State = CarState.InService;
            }
        }
    }
}
