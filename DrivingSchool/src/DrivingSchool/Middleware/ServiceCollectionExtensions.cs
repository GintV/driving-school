using DrivingSchool.Entities;
using DrivingSchool.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataServices(this IServiceCollection services) =>
            services.AddScoped<IDataService<Car>, CarData>().
            AddScoped<IDataService<CarUsage>, CarUsageData>().
            AddScoped<IDataService<Class>, ClassData>().
            AddScoped<IDataService<Document>, DocumentData>().
            AddScoped<IUserService<Instructor>, InstructorData>().
            AddScoped<IDataService<Mark>, MarkData>().
            AddScoped<IDataService<MileagePoint>, MileagePointData>().
            AddScoped<IUserService<Student>, StudentData>().
            AddScoped<IDataService<TheoryClasses>, TheoryClassesData>().
            AddScoped<IUserService<User>, UserData>();
    }
}
