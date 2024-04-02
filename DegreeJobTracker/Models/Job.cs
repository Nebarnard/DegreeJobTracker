using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DegreeJobTracker.Models
{
    public partial class Job
    {
       
        public Job()
        {
            DegreeJobPeople = new HashSet<DegreeJobPerson>();
        }

        [ValidateNever]
        public int? JobId { get; set; }
        [Required(ErrorMessage = "Please enter a Job title (eg: Sales Associate).")]
        public string JobTitle { get; set; } = null!;

        [ValidateNever]
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
