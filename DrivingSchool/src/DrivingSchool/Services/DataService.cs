using DrivingSchool.Entities.Context;
using System.Collections.Generic;
using System.Linq;

/**
* @(#) Service.cs
*/
namespace DrivingSchool.Services
{
    public abstract class DataService<T> : IDataService<T>
    {
        protected DrivingSchoolDbContext m_context;

        public DataService(DrivingSchoolDbContext context)
        {
            m_context = context;
        }

        public abstract T Get(int id);

        public abstract IQueryable<T> GetAll();

        public abstract void RemoveRange(IEnumerable<T> data);

        public void Add(T data) => m_context.Add((object)data);

        public void Remove(T data) => m_context.Remove((object)data);

        public void SaveChanges() => m_context.SaveChanges();

        public void Update(T data) => m_context.Update((object)data);
    }

}
