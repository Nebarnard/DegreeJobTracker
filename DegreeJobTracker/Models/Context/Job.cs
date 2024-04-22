using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DegreeJobTracker.Models.Context {
    public partial class Job
    {
       
        public Job()
        {
            DegreeJobPeople = new HashSet<DegreeJobPerson>();
        }

        [ValidateNever]
        public int? JobId { get; set; }
        [Required(ErrorMessage = "Please enter a Job title (eg: Sales Associate).")]
        [MaxLength(50, ErrorMessage = "Job title cannot be longer than 50 characters.")]
        public string JobTitle { get; set; } = null!;

        [ValidateNever]
        [MaxLength(50, ErrorMessage = "Business name cannot be longer than 50 characters.")]
        public string? BusinessName { get; set; }

        [Required]
        [Range(0.01,double.MaxValue, ErrorMessage = "Salary must be greater than zero.")]
        public decimal Salary { get; set; }

        [ValidateNever]
        public string? Description { get; set; }

        [ValidateNever]
        public int PersonId { get; set; }

        [ValidateNever]
        public virtual Person Person { get; set; } = null!;

        [ValidateNever]
        public virtual ICollection<DegreeJobPerson> DegreeJobPeople { get; set; }
    }
}
