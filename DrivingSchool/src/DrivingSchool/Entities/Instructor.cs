using System.Collections.Generic;

/**
* @(#) Instructor.cs
*/
namespace DrivingSchool.Entities
{
    public class Instructor : User
    {
        public List<Class> TaughtClasses { get; set; }
        public CarUsage CarUsages { get; set; }
        public Car AssignedCar { get; set; }
    }
}
