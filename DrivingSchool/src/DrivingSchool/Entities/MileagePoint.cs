/**
 * @(#) MileagePoint.cs
 */
namespace DrivingSchool.Entities
{
    public class MileagePoint
    {
        public int Id { get; set; }

        public int Mileage { get; set; }
        public string Name { get; set; }

        public Car OwnerCar { get; set; }
    }
}
