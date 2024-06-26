﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DegreeJobTracker.Models.Validation
{

    public class PasswordValidation : ValidationAttribute
    {
        private readonly int _minimumLength = 8;
        private readonly bool _requireUppercase = true;
        private readonly bool _requireLowercase = true;
        private readonly bool _requireDigit = true;
        private readonly bool _requireSpecialChar = true;

        // Default Constructor
        public PasswordValidation() { } // end method

        // Length only Constructor
        public PasswordValidation(int minimumLength)
        {
            _minimumLength = minimumLength;
        } // end method

        // Bool only Constructor
        public PasswordValidation(bool requireUppercase, bool requireLowercase, bool requireDigit, bool requireSpecialChar)
        {
            _requireUppercase = requireUppercase;
            _requireLowercase = requireLowercase;
            _requireDigit = requireDigit;
            _requireSpecialChar = requireSpecialChar;
        } // end method

        // Length and Single Bool Constructor
        public PasswordValidation(int minimumLength, bool requireAll)
        {
            _minimumLength = minimumLength;
            _requireUppercase = requireAll;
            _requireLowercase = requireAll;
            _requireDigit = requireAll;
            _requireSpecialChar = requireAll;
        } // end method

        // Full Constructor
        public PasswordValidation(int minimumLength, bool requireUppercase, bool requireLowercase, bool requireDigit, bool requireSpecialChar)
        {
            _minimumLength = minimumLength;
            _requireUppercase = requireUppercase;
            _requireLowercase = requireLowercase;
            _requireDigit = requireDigit;
            _requireSpecialChar = requireSpecialChar;
        } // end method

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            // Initialize String
            string password;

            // Check if null
            if (value == null) {
                password = string.Empty;
            } else {
                password = value.ToString();
            } // end if

            // Create Error Message
            string errorMessage = GetErrorMessage();

            // Check minimum length
            if (password.Length < _minimumLength) {
                return new ValidationResult(errorMessage);
            } // end if

            // Check uppercase requirement
            if (_requireUppercase && !ContainsUppercase(password)) {
                return new ValidationResult(errorMessage);
            } // edn if

            // Check lowercase requirement
            if (_requireLowercase && !ContainsLowercase(password)) {
                return new ValidationResult(errorMessage);
            } // end if

            // Check digit requirement
            if (_requireDigit && !ContainsDigit(password)) {
                return new ValidationResult(errorMessage);
            } // end if

            // Check special character requirement
            if (_requireSpecialChar && !ContainsSpecialChar(password)) {
                return new ValidationResult(errorMessage);
            } // end if

            return ValidationResult.Success;
        } // end method

        // Creates an Error Message based on parameters
        private string GetErrorMessage() {
            // Initialize Error Message
            string errorMessage = "Password must";

            // Min Length
            if (_minimumLength > 0) {
                errorMessage += $"be {_minimumLength} characters long";
            } // end if

            // Uppercase
            if (_requireUppercase) {
                // Check if requirement before
                if (errorMessage.Length != 7) {
                    errorMessage += ", ";
                } // end if

                // Add Message
                errorMessage += $"contain at least one uppercase letter";
            } // end if

            // Lowercase
            if (_requireLowercase) {
                // Check if requirement before
                if (errorMessage.Length != 7) {
                    errorMessage += ", ";
                } // end if

                // Add Message
                errorMessage += $"contain at least one lowercase letter";
            } // end if

            // Digit
            if (_requireDigit) {
                // Check if requirement before
                if (errorMessage.Length != 7) {
                    errorMessage += ", ";
                } // end if

                // Add Message
                errorMessage += $"contain at least one digit";
            } // end if

            // Special Char
            if (_requireSpecialChar) {
                // Check if requirement before
                if (errorMessage.Length != 7) {
                    errorMessage += ", and ";
                } // end if

                // Add Message
                errorMessage += $"contain at least one special character";
            } // end if

            // Return with period
            return errorMessage + '.';
        } // end if

        // Check if a password contains an uppercase letter
        private bool ContainsUppercase(string value) {
            return value.Any(char.IsUpper);
        } // end method

        // Check if a password contains a lowercase letter
        private bool ContainsLowercase(string value) {
            return value.Any(char.IsLower);
        } // end method

        // Check if password contains a digit
        private bool ContainsDigit(string value) {
            return value.Any(char.IsDigit);
        } // end method

        // Check if password contains a special character
        private bool ContainsSpecialChar(string value) {
            return value.Any(ch => !char.IsLetterOrDigit(ch));
        } // end method
    } // end class
} // end namespace