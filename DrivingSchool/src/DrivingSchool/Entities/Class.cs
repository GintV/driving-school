using DrivingSchool.Entities.Enumerations;
using System;

/**
* @(#) Class.cs
*/
namespace DrivingSchool.Entities
{
    public class Class
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ClassType Type { get; set; }
        public ClassState State { get; set; }

        public Instructor Instructor { get; set; }
        public Student Student { get; set; }
        public Mark Mark { get; set; }
        public CarUsage CarUsage { get; set; }

        public override string ToString() => $"{Id}. {Date.Date.ToString("yyyy-MM-dd")} " + 
            $"{StartTime.TimeOfDay}-{EndTime.TimeOfDay} {Instructor.FirstName} " +
            Instructor.LastName;
    }
}
