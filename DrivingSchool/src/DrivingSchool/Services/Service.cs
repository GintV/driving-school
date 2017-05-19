using DrivingSchool.Entities.Context;
using System;
using System.Linq;

/**
* @(#) Service.cs
*/
namespace DrivingSchool.Services
{
    public abstract class Service<T> : IService<T>
    {
        protected DrivingSchoolDbContext m_context;

        public Service(DrivingSchoolDbContext context)
        {
            m_context = context;
        }

        public abstract T Get(int id);

        public abstract IQueryable<T> GetAll();

        public void Add(T data) => m_context.Add((object)data);

        public void SaveChanges() => m_context.SaveChanges();

    }

}
