using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

/**
* @(#) TheoryClassesData.cs
*/
namespace DrivingSchool.Services
{
    public class TheoryClassesData : DataService<TheoryClasses>
    {
        public TheoryClassesData(DrivingSchoolDbContext context) : base(context) { }

        public override TheoryClasses Get(int id) => m_context.TheoryClasses.
            Include(c => c.AdditionalClasses).Include(c => c.Instructor).Include(c => c.Mark).
            Include(c => c.Students).FirstOrDefault(s => s.Id == id);

        public override IQueryable<TheoryClasses> GetAll() => m_context.TheoryClasses.
            Include(c => c.AdditionalClasses).Include(c => c.Instructor).
            Include(c => c.Mark).Include(c => c.Students);

        public override void RemoveRange(IEnumerable<TheoryClasses> data) => m_context.
            TheoryClasses.RemoveRange(data);
    }
}
