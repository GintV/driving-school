
using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;

/**
* @(#) CarUsageData.cs
*/
namespace DrivingSchool.Services
{
    public class CarUsageData : DataService<CarUsage>
    {
        public CarUsageData(DrivingSchoolDbContext context) : base(context) { }

        public override CarUsage Get(int id) =>
             m_context.CarUsages.FirstOrDefault(s => s.Id == id);

        public override IQueryable<CarUsage> GetAll() =>
            m_context.CarUsages;



        public void GetFilteredCarUsages()
        {
            throw new NotImplementedException();
        }
    }
}
