/**
 * @(#) CarState.cs
 */

using System.ComponentModel.DataAnnotations;

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
        Unused
    }
}
