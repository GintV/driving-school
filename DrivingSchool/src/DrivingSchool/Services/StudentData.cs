using System;
using System.Linq;
using DrivingSchool.Entities;
using System.Collections.Generic;
using DrivingSchool.Entities.Context;

/**
* @(#) StudentData.cs
*/
namespace DrivingSchool.Services
{
    public class StudentData : Service<Student>
    {
        public StudentData(DrivingSchoolDbContext context) : base(context) { }

        public override Student Get(int id) =>
            m_context.Students.FirstOrDefault(s => s.Id == id.ToString());

        public override IQueryable<Student> GetAll() =>
            m_context.Students;
    }
}
