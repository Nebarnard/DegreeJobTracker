using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DegreeJobTracker.Models
{
    public partial class Person
    {

        public int? PersonId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }


        public ICollection<Degree>? Degrees { get; } = new List<Degree>();

        public ICollection<Job>? Jobs { get; } = new List<Job>();

        public virtual ICollection<DegreeJobPerson>? DegreeJobPeople { get; set; }
    }
}
