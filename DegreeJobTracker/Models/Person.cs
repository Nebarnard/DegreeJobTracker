using System;
using System.Collections.Generic;

namespace DegreeJobTracker.Models
{
    public partial class Person
    {
        public Person()
        {
            DegreeJobPeople = new HashSet<DegreeJobPerson>();
            Degrees = new HashSet<Degree>();
            Jobs = new HashSet<Job>();
        }

        public int PersonId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<DegreeJobPerson> DegreeJobPeople { get; set; }
        public virtual ICollection<Degree> Degrees { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
