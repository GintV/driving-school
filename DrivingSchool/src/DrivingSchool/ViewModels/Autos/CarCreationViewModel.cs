/**
 * @(#) CarCreationViewModel.cs
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;

namespace DrivingSchool.ViewModels.Autos
{
    public class CarCreationViewModel
    {
        [Required, MaxLength(255), Display(Name = "Make")]
        public string Brand { get; set; }
        [Required, MaxLength(255)]
        public string Model { get; set; }

        [Required, MinLength(6), MaxLength(7), Display(Name = "License Plate")]
        public string LicensePlate { get; set; }
        [DataType(DataType.Date), Display(Name = "Date of Manufacture")]
        public DateTime ManufactureDate { get; set; }
        [Required]
        public int Mileage { get; set; }
        [Display(Name = "Type of Gearbox")]
        [EnumDataType(typeof(Gearbox), ErrorMessage = "Invalid gearbox type.")]
        public Gearbox Gearbox { get; set; }

        public List<Document> Documents { get; set; }
        public List<MileagePoint> MileagePoints { get; set; }
    }
    
}
