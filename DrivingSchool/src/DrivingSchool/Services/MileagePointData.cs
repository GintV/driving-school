using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using System;
using System.Linq;

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



        public void GetCarsMileagePoints()
        {
            throw new NotImplementedException();
        }

        public void UpdateCarsMileagePoints()
        {
            throw new NotImplementedException();
        }
    }
}
