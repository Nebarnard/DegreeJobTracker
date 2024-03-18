using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace DegreeJobTracker.Models
{
    public partial class Person
    {

        public int PersonId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }


        public ICollection<Degree> Degrees { get; } = new List<Degree>();

        public ICollection<Job> Jobs { get; } = new List<Job>();

        public virtual ICollection<DegreeJobPerson> DegreeJobPeople { get; set; }
    }
}
