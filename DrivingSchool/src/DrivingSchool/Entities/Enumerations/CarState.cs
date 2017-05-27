using System.ComponentModel.DataAnnotations;

/**
 * @(#) CarState.cs
 */
namespace DrivingSchool.Entities.Enumerations
{
    public enum CarState
    {
        [Display(Name = "In Service")]
        InService,
        [Display(Name = "Non-Operational")]
        NonOperational,
        Operational,
        [Display(Name = "Requires Service")]
        RequiresService,
        [Display(Name = "Not being Used")]
        Unused
    }
}
