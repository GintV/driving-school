using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using System.Collections.Generic;

/**
* @(#) MarkData.cs
*/
namespace DrivingSchool.Services
{
    public class MarkData : DataService<Mark>
    {
        public MarkData(DrivingSchoolDbContext context) : base(context) { }

        public override Mark Get(int id) =>
            m_context.Marks.FirstOrDefault(s => s.Id == id);

        public override IQueryable<Mark> GetAll() =>
            m_context.Marks;

        public override void RemoveRange(IEnumerable<Mark> data) => m_context.Marks.
            RemoveRange(data);
    }
}
