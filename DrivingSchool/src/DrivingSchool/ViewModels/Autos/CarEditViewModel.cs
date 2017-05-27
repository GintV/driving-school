using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;

/**
 * @(#) CarEditViewModel.cs
 */
namespace DrivingSchool.ViewModels.Autos
{
    public class CarEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(255, ErrorMessage = "Maximum length is 255")]
        [Display(Name = "Make")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(255, ErrorMessage = "Maximum length is 255")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinLength(6, ErrorMessage = "Minimum length is 6")]
        [MaxLength(7, ErrorMessage = "Maximum length is 7")]
        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Date, ErrorMessage = "Has to be a valid date")]
        [Display(Name = "Date of Manufacture")]
        public DateTime ManufactureDate { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Mileage has to be a positive number")]
        public int Mileage { get; set; }

        [EnumDataType(typeof(Gearbox), ErrorMessage = "Invalid gearbox type.")]
        [Display(Name = "Type of Gearbox")]
        public Gearbox Gearbox { get; set; }

        public CarState State { get; set; }
        public string StateName { get; set; }

        public List<Document> Documents { get; set; }
        public List<MileagePointBase> MileagePoints { get; set; }
    }

}
