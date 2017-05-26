using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingSchool.Entities.Context;

namespace DrivingSchool.Services
{
    public abstract class UserService<T> : DataService<T>, IUserService<T>
    {
        public UserService(DrivingSchoolDbContext context) : base(context) { }

        public abstract T Get(string guid);
    }
}
