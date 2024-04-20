using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DegreeJobTracker.Models {
    public partial class UserCredential {

        [Required(ErrorMessage = "Please enter a Username.")]
        [MaxLength(50, ErrorMessage = "Username must be 50 characters or less.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a Password.")]
        [PasswordValidation(ErrorMessage = "Password does not meet the requirements.")]
        public string Password { get; set; } = null!;
    } // end class
} // end namespace
