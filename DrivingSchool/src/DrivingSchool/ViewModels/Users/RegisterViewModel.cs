using System;
using System.ComponentModel.DataAnnotations;

/**
* @(#) SignUpViewModel.cs
*/
namespace DrivingSchool.ViewModels.Users
{
    public class RegisterViewModel
    {
        [Required, MaxLength(255)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required, MaxLength(255)]
        public string FirstName { get; set; }

        [Required, MaxLength(255)]
        public string LastName { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required, MinLength(11), MaxLength(11)]
        public string PersonalNo { get; set; }
    }
    
}
