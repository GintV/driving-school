
using System.ComponentModel.DataAnnotations;
/**
* @(#) ClassType.cs
*/
namespace DrivingSchool.Entities.Enumerations
{
    public enum ClassType
    {
        [Display(Name = "Practice drive")]
        PracticeDrive,
        [Display(Name = "Practice exam")]
        PracticeExam,
        Template,
        [Display(Name = "Theory classes")]
        TheoryClasses,
        [Display(Name = "Theory exam")]
        TheoryExam
    }
}
