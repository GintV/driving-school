using DrivingSchool.Entities.Enumerations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

/**
* @(#) User.cs
*/
namespace DrivingSchool.Entities
{
    public class User : UserBase
    {
        public override int Id { get; set; }

        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override DateTime BirthDate { get; set; }
        public override string PersonalNo { get; set; }

        public override UserType Type { get; set; }
        public override UserState State { get; set; }

        public override IdentityUser IdentityUser { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }

    public abstract class UserBase
    {
        public abstract int Id { get; set; }

        public abstract string FirstName { get; set; }
        public abstract string LastName { get; set; }
        public abstract DateTime BirthDate { get; set; }
        public abstract string PersonalNo { get; set; }

        public abstract UserType Type { get; set; }
        public abstract UserState State { get; set; }

        public abstract IdentityUser IdentityUser { get; set; }

        public abstract override string ToString();

        private static readonly NullUser nullUser = new NullUser();

        public static NullUser NULL => nullUser;

        public class NullUser : UserBase
        {
            public override int Id { get { return -1; } set { } }

            public override string FirstName { get { return string.Empty; } set { } }
            public override string LastName { get { return string.Empty; } set { } }
            public override DateTime BirthDate { get { return DateTime.MinValue; } set { } }
            public override string PersonalNo { get { return string.Empty; } set { } }

            public override UserType Type { get { return UserType.None; } set { } }
            public override UserState State { get { return UserState.Unverified; } set { } }

            public override IdentityUser IdentityUser
                { get { return default (IdentityUser); } set { } }

            public override string ToString() => string.Empty;
        }
    }
}
