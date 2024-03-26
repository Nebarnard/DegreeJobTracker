using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace DegreeJobTracker.Models
{
    public partial class Degree {
        public Degree() {
            DegreeJobPeople = new HashSet<DegreeJobPerson>();
        }

        [ValidateNever]
        public int DegreeId { get; set; }
        public string Type { get; set; } = null!;
        public string? Program { get; set; }
        public string Major { get; set; }
        public string? Concentration { get; set; }
        public short? YearAwarded { get; set; }

        [ValidateNever]
        public int? PersonId { get; set; }

        [ValidateNever]
        public virtual Person Person { get; set; } = null!;

        [ValidateNever]
        public virtual ICollection<DegreeJobPerson> DegreeJobPeople { get; set; }
    }
}
