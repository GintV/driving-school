using DrivingSchool.Entities.Enumerations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

/**
* @(#) User.cs
*/
namespace DrivingSchool.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PersonalNo { get; set; }

        public UserType Type { get; set; }
        public UserState State { get; set; }

        public IdentityUser IdentityUser { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
