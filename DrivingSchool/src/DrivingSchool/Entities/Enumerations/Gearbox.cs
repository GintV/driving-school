using System.ComponentModel.DataAnnotations;

/**
 * @(#) Gearbox.cs
 */
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
