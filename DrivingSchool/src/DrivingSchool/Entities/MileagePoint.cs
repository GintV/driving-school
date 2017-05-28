/**
 * @(#) MileagePoint.cs
 */

using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Entities
{
    public class MileagePoint : MileagePointBase
    {
        public override int Id { get; set; }

        [Required]
        public override int Mileage { get; set; }
        [Required(ErrorMessage = "The Message field is required.")]
        public override string Name { get; set; }

        public override Car OwnerCar { get; set; }
    }

    public abstract class MileagePointBase
    {
        public abstract int Id { get; set; }

        public abstract int Mileage { get; set; }
        public abstract string Name { get; set; }

        public abstract Car OwnerCar { get; set; }

        private static readonly NullMileagePoint nullMileagePoint = new NullMileagePoint();

        public static NullMileagePoint NULL => nullMileagePoint;

        public class NullMileagePoint : MileagePointBase
        {
            public override int Id { get { return -1; } set { } }

            public override int Mileage { get { return -1; } set { } }
            public override string Name { get { return string.Empty; } set { } }

            public override Car OwnerCar { get { return default(Car); } set { } }
        }
    }
}
