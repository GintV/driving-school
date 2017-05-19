using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;

/**
* @(#) ClassData.cs
*/
namespace DrivingSchool.Services
{
    public class ClassData : Service<Class>
    {
        public ClassData(DrivingSchoolDbContext context) : base(context) { }

        public override Class Get(int id) =>
            m_context.Classes.FirstOrDefault(s => s.Id == id);

        public override IQueryable<Class> GetAll() =>
            m_context.Classes;



        public void SaveClasses()
        {
            throw new NotImplementedException();
        }
    }
}
