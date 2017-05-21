using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

/**
* @(#) MileagePointData.cs
*/
namespace DrivingSchool.Services
{
    public class MileagePointData : DataService<MileagePoint>
    {
        public MileagePointData(DrivingSchoolDbContext context) : base(context) { }

        public override MileagePoint Get(int id) =>
            m_context.MileagePoints.FirstOrDefault(s => s.Id == id);

        public override IQueryable<MileagePoint> GetAll() =>
            m_context.MileagePoints;



        public IQueryable<MileagePoint> GetCarsMileagePoints(Car car)
        {
            return m_context.MileagePoints.Where(p => p.OwnerCar == car);
        }

        public void UpdateCarsMileagePoints(Car car, List<MileagePoint> points)
        {
            m_context.Database.ExecuteSqlCommand("DELETE FROM MileagePoints WHERE OwnerCarId = {0}", car.Id);
            car.MileagePoints = points;
        }
    }
}
