/**
 * @(#) Gearbox.cs
 */

using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Entities.Enumerations
{
    public enum Gearbox
    {
        Automatic,
        Manual,
        [Display(Name = "Semi-Automatic")]
        SemiAutomatic
    }
}
