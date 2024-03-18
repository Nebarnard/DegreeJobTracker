using System;
using System.Collections.Generic;

namespace DegreeJobTracker.Models
{
    public partial class Job
    {
        public Job()
        {
            DegreeJobPeople = new HashSet<DegreeJobPerson>();
        }

        public int JobId { get; set; }
        public string JobTitle { get; set; } = null!;
        public string? BusinessName { get; set; }
        public decimal? Salary { get; set; }
        public string? Description { get; set; }
        public int PersonId { get; set; }

        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<DegreeJobPerson> DegreeJobPeople { get; set; }
    }
}
