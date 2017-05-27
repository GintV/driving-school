using System;
using System.Collections.Generic;
using System.Linq;

/**
* @(#) IService.cs
*/
namespace DrivingSchool.Services
{
    public interface IDataService<T>
    {
        void Add(T data);
        T Get(int id);
        IQueryable<T> GetAll();
        void Remove(T data);
        void RemoveRange(IEnumerable<T> data);
        void SaveChanges();
        void Update(T data);
    }
}
