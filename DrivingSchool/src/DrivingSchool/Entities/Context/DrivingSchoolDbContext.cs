using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Entities.Context
{
    public class DrivingSchoolDbContext : IdentityDbContext<IdentityUser>
    {
        public DrivingSchoolDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarUsage> CarUsages { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<MileagePoint> MileagePoints { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TheoryClasses> TheoryClasses { get; set; }
        public DbSet<User> GenericUsers { get; set; }
    }
}
