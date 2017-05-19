using DrivingSchool.Entities.Enumerations;
using System;

/**
* @(#) Document.cs
*/
namespace DrivingSchool.Entities
{
    public class Document
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DocumentType Type { get; set; }

        public Car OwnerCar { get; set; }
    }
}
