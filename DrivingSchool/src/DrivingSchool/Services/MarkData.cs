using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;

/**
* @(#) MarkData.cs
*/
namespace DrivingSchool.Services
{
    public class MarkData : Service<Mark>
    {
        public MarkData(DrivingSchoolDbContext context) : base(context) { }

        public override Mark Get(int id) =>
            m_context.Marks.FirstOrDefault(s => s.Id == id);

        public override IQueryable<Mark> GetAll() =>
            m_context.Marks;
    }
}
