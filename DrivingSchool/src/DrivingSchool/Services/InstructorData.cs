using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

/**
* @(#) InstructorData.cs
*/
namespace DrivingSchool.Services
{
    public class InstructorData : UserService<Instructor>
    {
        public InstructorData(DrivingSchoolDbContext context) : base(context) { }

        public override Instructor Get(string guid) => m_context.Instructors.
            Include(i => i.AssignedCar).Include(i => i.CarUsages).Include(i => i.TaughtClasses).
            ThenInclude(c => c.Student).FirstOrDefault(s => s.IdentityUser.Id == guid);

        public override Instructor Get(int id) => m_context.Instructors.
            Include(i => i.AssignedCar).Include(i => i.CarUsages).Include(i => i.TaughtClasses).
            FirstOrDefault(s => s.Id == id);

        public override IQueryable<Instructor> GetAll() =>
            m_context.Instructors.Include(i => i.AssignedCar).Include(i => i.CarUsages).
            Include(i => i.TaughtClasses);

        public override void RemoveRange(IEnumerable<Instructor> data) => m_context.Instructors.
            RemoveRange(data);
    }

    public static class InstructorServiceExtensions
    {
        public static void Update(this IUserService<Instructor> userData,
            ViewModels.Users.UserEditViewModel data)
        {
            var toUpdate = userData.GetAll().FirstOrDefault(s => s.Id == data.Id);
            if (toUpdate != null)
            {
                toUpdate.FirstName = data.FirstName;
                toUpdate.LastName = data.LastName;
                toUpdate.BirthDate = data.BirthDate;
                toUpdate.PersonalNo = data.PersonalNo;
                toUpdate.Type = data.Type;
                toUpdate.State = data.State;
                toUpdate.AssignedCar = data.AssignedCar;
            }
        }
    }
}
