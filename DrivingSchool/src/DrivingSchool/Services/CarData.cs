using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;

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

        public void ChangeState()
        {
            throw new NotImplementedException();
        }
    }
}
