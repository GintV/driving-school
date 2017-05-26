using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

/**
* @(#) UserData.cs
*/
namespace DrivingSchool.Services
{
    public class UserData : UserService<User>
    {
        public UserData(DrivingSchoolDbContext context) : base(context) { }

        public override User Get(string guid) => m_context.GenericUsers.
            FirstOrDefault(s => s.IdentityUser.Id == guid);

        public override User Get(int id) => m_context.GenericUsers.
            Include(self => self.IdentityUser).
            FirstOrDefault(user => user.Id == id);

        public override IQueryable<User> GetAll() => m_context.GenericUsers.
            Include(self => self.IdentityUser);

        public override void RemoveRange(IEnumerable<User> data) => m_context.GenericUsers.
            RemoveRange(data);
    }

    public static class UserServiceExtensions
    {
        public static void Update(this IUserService<User> userData,
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
            }
        }
    }
}
