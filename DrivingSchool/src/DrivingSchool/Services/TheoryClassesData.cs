using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;

/**
* @(#) TheoryClassesData.cs
*/
namespace DrivingSchool.Services
{
    public class TheoryClassesData : Service<TheoryClasses>
    {
        public TheoryClassesData(DrivingSchoolDbContext context) : base(context) { }

        public override TheoryClasses Get(int id) =>
            m_context.TheoryClasses.FirstOrDefault(s => s.Id == id);

        public override IQueryable<TheoryClasses> GetAll() =>
            m_context.TheoryClasses;
    }
}
