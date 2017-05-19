using DrivingSchool.Entities.Enumerations;
using System;

/**
* @(#) CarUsage.cs
*/
namespace DrivingSchool.Entities
{
    public class CarUsage
    {
        public int Id { get; set; }

        public int MileageBefore { get; set; }
        public int MileageAfter { get; set; }
        public DateTime Date { get; set; }

        public CarUsageType Type { get; set; }
        
        public Class ClassUsedIn { get; set; }
        public Instructor Instructor { get; set; }
        public Car Car { get; set; }
    }
}
