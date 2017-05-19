using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using System;
using System.Linq;

/**
* @(#) UserData.cs
*/
namespace DrivingSchool.Services
{
    public class UserData : Service<User>
    {
        public UserData(DrivingSchoolDbContext context) : base(context) { }

        public override User Get(int id) =>
            m_context.Users.FirstOrDefault(s => s.Id == id.ToString());

        public override IQueryable<User> GetAll() =>
            m_context.Users;
    }
}
