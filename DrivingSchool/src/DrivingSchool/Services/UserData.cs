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

        public User Get(string guid) =>
            m_context.GenericUsers.SingleOrDefault(user => user.IdentityUser.Id == guid);

        public override IQueryable<User> GetAll() =>
            m_context.GenericUsers;

        public void Update(ViewModels.Users.UserEditViewModel data)
        {
            
            var toUpdate = m_context.GenericUsers.FirstOrDefault(s => s.Id == data.Id);
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
