using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using Microsoft.EntityFrameworkCore;

/**
* @(#) InstructorData.cs
*/
namespace DrivingSchool.Services
{
    public class InstructorData : DataService<Instructor>
    {
        public InstructorData(DrivingSchoolDbContext context) : base(context) { }

        public Instructor Get(string guid) => m_context.Instructors.Include(i => i.AssignedCar).
            Include(i => i.CarUsages).Include(i => i.TaughtClasses).
            FirstOrDefault(s => s.IdentityUser.Id == guid);

        public override Instructor Get(int id) => m_context.Instructors.
            Include(i => i.AssignedCar).Include(i => i.CarUsages).Include(i => i.TaughtClasses).
            FirstOrDefault(s => s.Id == id);

        public override IQueryable<Instructor> GetAll() =>
            m_context.Instructors.Include(i => i.AssignedCar).Include(i => i.CarUsages).
            Include(i => i.TaughtClasses);
    }
}
