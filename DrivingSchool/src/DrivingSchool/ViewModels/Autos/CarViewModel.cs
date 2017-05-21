/**
 * @(#) CarViewModel.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;

namespace DrivingSchool.ViewModels.Autos
{
    public class CarViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Make")]
        public string Brand { get; set; }
        public string Model { get; set; }

        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; }
        [Display(Name = "Date of Manufacture")]
        [DataType(DataType.Date)]
        public DateTime ManufactureDate { get; set; }
        public int Mileage { get; set; }
        [Display(Name = "Type of gearbox")]
        public Gearbox Gearbox { get; set; }
        public string GearboxName { get; set; }
        public CarState State { get; set; }
        public string StateName { get; set; }

        public List<Document> Documents { get; set; }
        public List<MileagePoint> MileagePoints { get; set; }
    }
    
}
