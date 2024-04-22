using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DegreeJobTracker.Models.Validation;

namespace DegreeJobTracker.Models.Context
{
    public partial class Degree {
        public Degree() {
            DegreeJobPeople = new HashSet<DegreeJobPerson>();
        }

        [ValidateNever]
        public int? DegreeId { get; set; }

        [Required(ErrorMessage = "Please enter a Degree Type (AS, AAS, BS, etc.).")]
        [MaxLength(20, ErrorMessage = "Degree Type must be in abbreviated format (Associate's = AS).")]
        public string Type { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "Program cannot be longer than 50 characters.")]
        public string? Program { get; set; }


        [Required(ErrorMessage = "Please enter a Degree Major (ex: Programming).")]
        [MaxLength(50, ErrorMessage = "Major cannot be longer than 50 characters.")]
        public string Major { get; set; }

        [MaxLength(50, ErrorMessage = "Concentration cannot be longer than 50 characters.")]
        public string? Concentration { get; set; }

        [YearValidation(ErrorMessage = "Year must be between 1900 and the current year.")]
        public short? YearAwarded { get; set; }

        [ValidateNever]
        public int? PersonId { get; set; }

        [ValidateNever]
        public virtual Person Person { get; set; } = null!;

        [ValidateNever]
        public virtual ICollection<DegreeJobPerson> DegreeJobPeople { get; set; }
    }
}
