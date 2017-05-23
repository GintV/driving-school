using System.Collections.Generic;

/**
* @(#) TheoryClasses.cs
*/
namespace DrivingSchool.Entities
{
    public class TheoryClasses : Class
    {
        public int Seats { get; set; }
        public bool IsMain { get; set; }
        public int Weeks { get; set; }

        public List<Student> Students { get; set; }
        public TheoryClasses MainClasses { get; set; }
        public List<TheoryClasses> AdditionalClasses { get; set; }

        public override string ToString() => base.ToString() + $" ({Weeks} weeks)";
    }
}
