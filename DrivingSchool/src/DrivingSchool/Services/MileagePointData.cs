using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using System.Collections.Generic;
using System.Linq;

/**
* @(#) MileagePointData.cs
*/
namespace DrivingSchool.Services
{
    public class MileagePointData : DataService<MileagePoint>
    {
        public MileagePointData(DrivingSchoolDbContext context) : base(context) { }

        public override MileagePoint Get(int id) => m_context.MileagePoints.
            FirstOrDefault(s => s.Id == id);

        public override IQueryable<MileagePoint> GetAll() => m_context.MileagePoints;

        public override void RemoveRange(IEnumerable<MileagePoint> data) => m_context.
            MileagePoints.RemoveRange(data);
    }

    public static class MileagePointDataServiceExtensions
    {
        public static IEnumerable<MileagePoint>
            GetCarsMileagePoints(this IDataService<MileagePoint> data, Car car) => data.GetAll().
            Where(p => p.OwnerCar == car).OrderBy(p => p.Mileage);

        public static void UpdateCarsMileagePoints(this IDataService<MileagePoint> data, Car car,
            List<MileagePoint> points)
        {
            //m_context.Database.
            //    ExecuteSqlCommand("DELETE FROM MileagePoints WHERE OwnerCarId = {0}", car.Id);
            data.RemoveRange(data.GetAll().Where(p => p.OwnerCar == car));
            data.SaveChanges();
            car.MileagePoints = points;
        }
    }
}
