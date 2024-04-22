using System;
using System.ComponentModel.DataAnnotations;

namespace DegreeJobTracker.Models.Validation
{
    public class YearValidation : ValidationAttribute
    {
        private readonly int _minYear;
        private readonly int _maxYear;

        // Empty Constructor
        public YearValidation() { } // end method

        // Min Year Constructor
        public YearValidation(int minYear)
        {
            _minYear = minYear;
            _maxYear = DateTime.Now.Year;
        } // end method

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            } // end if

            if (value is int year)
            {
                return year >= _minYear && year <= _maxYear;
            } // end if

            return false;
        } // end method
    } // end class
} // end namespace
