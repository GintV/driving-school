using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;

/**
* @(#) CarData.cs
*/
namespace DrivingSchool.Services
{
    public class CarData : Service<Car>
    {
        public CarData(DrivingSchoolDbContext context) : base(context) { }

        public override Car Get(int id) =>
            m_context.Cars.FirstOrDefault(s => s.Id == id);

        public override IQueryable<Car> GetAll() =>
            m_context.Cars;



        public void Update()
        {
            throw new NotImplementedException();
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
