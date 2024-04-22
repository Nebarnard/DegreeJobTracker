using System.ComponentModel.DataAnnotations;
using DegreeJobTracker.Models.Validation;

namespace DegreeJobTracker.Models.ViewModel {
    public class PasswordViewModel {
        [Required(ErrorMessage = "Please enter a Password.")]
        [PasswordValidation]
        public string Password { get; set; } = null!;
    } // end class
} // end namespace
