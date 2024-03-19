using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        [ValidateNever]
        public int? JobId { get; set; }
        public string JobTitle { get; set; } = null!;
        public string? BusinessName { get; set; }
        public decimal? Salary { get; set; }
        public string? Description { get; set; }

        [ValidateNever]
        public int PersonId { get; set; }

        [ValidateNever]
        public virtual Person Person { get; set; } = null!;

        [ValidateNever]
        public virtual ICollection<DegreeJobPerson> DegreeJobPeople { get; set; }
    }
}
