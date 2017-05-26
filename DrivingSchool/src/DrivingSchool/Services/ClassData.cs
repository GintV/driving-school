using System;
using System.Linq;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using DrivingSchool.Entities.Enumerations;

/**
* @(#) ClassData.cs
*/
namespace DrivingSchool.Services
{
    public class ClassData : DataService<Class>
    {
        public ClassData(DrivingSchoolDbContext context) : base(context) { }

        public override Class Get(int id) => m_context.Classes.Include(c => c.CarUsage).
            Include(c => c.Instructor).Include(c => c.Mark).Include(c => c.Student).
            FirstOrDefault(s => s.Id == id);

        public override IQueryable<Class> GetAll() => m_context.Classes.Include(c => c.CarUsage).
            Include(c => c.Instructor).Include(c => c.Mark).Include(c => c.Student);

        public override void RemoveRange(IEnumerable<Class> data) => m_context.Classes.
            RemoveRange(data);
    }
}
