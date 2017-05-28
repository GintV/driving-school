using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using System.Collections.Generic;
using System.Linq;

/**
* @(#) MileagePointData.cs
*/
namespace DrivingSchool.Services
{
    public class MileagePointData : DataService<MileagePointBase>
    {
        public MileagePointData(DrivingSchoolDbContext context) : base(context) { }

        public override MileagePointBase Get(int id) => (MileagePointBase)m_context.MileagePoints.
            FirstOrDefault(s => s.Id == id) ?? MileagePointBase.NULL;

        public override IQueryable<MileagePointBase> GetAll() => m_context.MileagePoints;

        public override void RemoveRange(IEnumerable<MileagePointBase> data) => m_context.
            MileagePoints.RemoveRange(data.Select(p => p as MileagePoint));
    }

    public static class MileagePointDataServiceExtensions
    {
        public static IEnumerable<MileagePointBase>
            GetCarsMileagePoints(this IDataService<MileagePointBase> data, Car car) =>
            data.GetAll().Where(p => p.OwnerCar == car).OrderBy(p => p.Mileage);

        public static void UpdateCarsMileagePoints(this IDataService<MileagePointBase> data,
            Car car, List<MileagePointBase> points)
        {
            data.RemoveRange(data.GetAll().Where(p => p.OwnerCar == car));
            data.SaveChanges();
            car.MileagePoints = points?.Select(p => p as MileagePoint).ToList();
        }
    }
}
