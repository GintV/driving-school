using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using System;
using System.Linq;

/**
* @(#) UserData.cs
*/
namespace DrivingSchool.Services
{
    public class UserData : DataService<User>
    {
        public UserData(DrivingSchoolDbContext context) : base(context) { }

        public override User Get(int id) =>
            m_context.GenericUsers.FirstOrDefault(user => user.Id == id);

        public override IQueryable<User> GetAll() =>
            m_context.GenericUsers;
    }
}
