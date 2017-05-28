using DrivingSchool.Entities.Enumerations;
using System;
using System.Collections.Generic;

/**
* @(#) Car.cs
*/
namespace DrivingSchool.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public DateTime ManufactureDate { get; set; }
        public int Mileage { get; set; }

        public Gearbox Gearbox { get; set; }
        public CarState State { get; set; }

        public List<Instructor> InstructorsWithAccess { get; set; }
        public List<Document> Documents { get; set; }
        public List<CarUsage> CarUsages { get; set; }
        public List<MileagePoint> MileagePoints { get; set; }
        
        public override string ToString() => $"[ {LicensePlate} ] {Brand} {Model}";
    }
}
