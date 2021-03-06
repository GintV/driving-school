using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DrivingSchool.Entities;
using DrivingSchool.Entities.Enumerations;

/**
* @(#) UserDataEditViewModel.cs
*/
namespace DrivingSchool.ViewModels.Users
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string FirstName { get; set; }
        [Required, MaxLength(255)]
        public string LastName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required, MinLength(11), MaxLength(11)]
        public string PersonalNo { get; set; }

        [EnumDataType(typeof(UserType), ErrorMessage = "Invalid user type.")]
        public UserType Type { get; set; }
        [EnumDataType(typeof(UserState), ErrorMessage = "Invalid user state.")]
        public UserState State { get; set; }

        // Student fields
        public bool HasTheoryClasses { get; set; }
        public int PracticeCount { get; set; }

        // Intructor fields
        public Car AssignedCar { get; set; }
        public List<Car> Cars { get; set; }

        public bool IsManager { get; set; }
        
    }
    
}
