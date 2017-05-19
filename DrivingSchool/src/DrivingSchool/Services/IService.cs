/**
* @(#) IService.cs
*/

using System.Linq;

namespace DrivingSchool.Services
{
    public interface IService<T>
    {
        void Add(T data);
        T Get(int id);
        IQueryable<T> GetAll();
    }
}
