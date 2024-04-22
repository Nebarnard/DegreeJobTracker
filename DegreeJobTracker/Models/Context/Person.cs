using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DegreeJobTracker.Models.Context {
    public partial class Person
    {

        public int? PersonId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [MaxLength(35, ErrorMessage = "First name cannot be longer than 35 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a last name.")]
        [MaxLength(35, ErrorMessage = "Last name cannot be longer than 35 characters.")]
        public string LastName { get; set; } = null!;

        [MaxLength(70, ErrorMessage = "Email cannot be longer than 70 characters.")]
        public string? Email { get; set; }

        [MaxLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
        public string? Phone { get; set; }


        public ICollection<Degree>? Degrees { get; } = new List<Degree>();

        public ICollection<Job>? Jobs { get; } = new List<Job>();

        public virtual ICollection<DegreeJobPerson>? DegreeJobPeople { get; set; }
    }
}
