/**
 * @(#) MileagePoint.cs
 */

using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Entities
{
    public class MileagePoint
    {
        public int Id { get; set; }
        [Required]
        public int Mileage { get; set; }
        [Required]
        public string Name { get; set; }

        public Car OwnerCar { get; set; }
    }
}
