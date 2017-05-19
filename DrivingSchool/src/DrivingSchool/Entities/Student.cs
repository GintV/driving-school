using System.Collections.Generic;

/**
* @(#) Student.cs
*/
namespace DrivingSchool.Entities
{
    public class Student : User
    {
        public bool HasTheoryClasses { get; set; }
        public int PracticeCount { get; set; }
        
        public List<Class> AttendedClasses { get; set; }
        public TheoryClasses TheoryClasses { get; set; }
        public List<Mark> ExamMarks { get; set; }
    }
}
