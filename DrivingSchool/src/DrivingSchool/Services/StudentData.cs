using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;

/**
* @(#) StudentData.cs
*/
namespace DrivingSchool.Services
{
    public class StudentData : DataService<Student>
    {
        public StudentData(DrivingSchoolDbContext context) : base(context) { }

        public Student Get(string guid) =>
            m_context.Students.FirstOrDefault(s => s.IdentityUser.Id == guid);

        public override Student Get(int id) =>
            m_context.Students.FirstOrDefault(s => s.Id == id);

        public override IQueryable<Student> GetAll() =>
            m_context.Students;
    }
}
