using DrivingSchool.Entities.Enumerations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

/**
* @(#) ClassCreationViewModel.cs
*/
namespace DrivingSchool.ViewModels.Classes
{
    public class ClassCreationViewModel
    {
        public ClassCreationViewModel()
        {
            ClassTypeList = new SelectList(new List<string>
            {
                ClassType.PracticeDrive.ToString(),
                ClassType.PracticeExam.ToString(),
                ClassType.TheoryClasses.ToString(),
                ClassType.TheoryExam.ToString()
            });
        }

        public SelectList ClassTypeList { get; set; }

        [Required, Display(Name = "Class Type")]
        public ClassType ClassType { get; set; }

        [Required, DataType(DataType.Date), DateGreaterEqThanToday]
        public DateTime Date { get; set; }

        [Required, Display(Name = "Start Time"), DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required, Display(Name = "End Time"), DataType(DataType.Time)]
        [TimeGreaterThan(nameof(StartTime))]
        public DateTime EndTime { get; set; }

        [Range(1, 100), Display(Name = "Number of Seats", Prompt = "For theory classes only")]
        public int? NumberOfSeats { get; set; }

        [Range(1, 16), Display(Name = "Weeks to Repeat", Prompt = "For theory classes only")]
        public int? Weeks { get; set; }

        public string BackAction { get; set; }
    }

    public class TimeGreaterThanAttribute : ValidationAttribute
    {
        private string m_otherProperty;

        public TimeGreaterThanAttribute(string otherProperty)
        {
            m_otherProperty = otherProperty;
        }

        private string DateToCompareToFieldName { get; set; }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            TimeSpan earlierDate = ((DateTime)validationContext.ObjectType.
                GetProperty(m_otherProperty).GetValue(validationContext.ObjectInstance, null)).
                TimeOfDay;
            TimeSpan laterDate = ((DateTime)value).TimeOfDay;

            if (laterDate > earlierDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Time must be greater than start time.");
            }
        }
    }

    public class DateGreaterEqThanTodayAttribute : ValidationAttribute
    {
        public DateGreaterEqThanTodayAttribute() { }

        private string DateToCompareToFieldName { get; set; }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            DateTime earlierDate = DateTime.Today;
            DateTime laterDate = ((DateTime)value).Date;

            if (laterDate >= earlierDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date must be current or later.");
            }
        }
    }
}
