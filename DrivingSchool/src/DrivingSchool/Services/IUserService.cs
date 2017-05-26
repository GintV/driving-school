

namespace DrivingSchool.Services
{
    public interface IUserService<T> : IDataService<T>
    {
        T Get(string guid);
    }
}
