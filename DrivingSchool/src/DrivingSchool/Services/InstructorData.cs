using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;

/**
* @(#) InstructorData.cs
*/
namespace DrivingSchool.Services
{
    public class InstructorData : Service<Instructor>
    {
        public InstructorData(DrivingSchoolDbContext context) : base(context) { }

        public override Instructor Get(int id) =>
            m_context.Instructors.FirstOrDefault(s => s.Id == id.ToString());

        public override IQueryable<Instructor> GetAll() =>
            m_context.Instructors;
    }
}
