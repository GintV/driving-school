using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

/**
* @(#) StudentData.cs
*/
namespace DrivingSchool.Services
{
    public class StudentData : UserService<Student>
    {
        public StudentData(DrivingSchoolDbContext context) : base(context) { }

        public override Student Get(string guid) => m_context.Students.
            Include(s => s.AttendedClasses).ThenInclude(c => c.Instructor).
            Include(s => s.TheoryClasses).Include(s => s.ExamMarks).
            FirstOrDefault(s => s.IdentityUser.Id == guid);

        public override Student Get(int id) => m_context.Students.Include(s => s.AttendedClasses).
            ThenInclude(c => c.Instructor).Include(s => s.TheoryClasses).Include(s => s.ExamMarks).
            FirstOrDefault(s => s.Id == id);

        public override IQueryable<Student> GetAll() =>
            m_context.Students.Include(s => s.AttendedClasses).ThenInclude(c => c.Instructor).
            Include(s => s.TheoryClasses).Include(s => s.ExamMarks);

        public override void RemoveRange(IEnumerable<Student> data) => m_context.Students.
            RemoveRange(data);
    }
}
