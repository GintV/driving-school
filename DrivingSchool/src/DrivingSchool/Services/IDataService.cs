/**
* @(#) IService.cs
*/

using System.Linq;

namespace DrivingSchool.Services
{
    public interface IDataService<T>
    {
        void Add(T data);
        T Get(int id);
        IQueryable<T> GetAll();
        void Remove(T data);
        void SaveChanges();
        void Update(T data);
    }
}
