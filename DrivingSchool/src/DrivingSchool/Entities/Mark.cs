using DrivingSchool.Entities.Enumerations;

/**
* @(#) Mark.cs
*/
namespace DrivingSchool.Entities
{
    public class Mark
    {
        public int Id { get; set; }

        public int ClassMark { get; set; }
        public string Comment { get; set; }

        public MarkType Type { get; set; }

        public Student Student { get; set; }
        public Class GivenForClass { get; set; }
    }
}
