using System;
using System.Collections.Generic;

namespace DegreeJobTracker.Models
{
    public partial class Degree
    {
        public Degree()
        {
            DegreeJobPeople = new HashSet<DegreeJobPerson>();
        }

        public int DegreeId { get; set; }
        public string Type { get; set; } = null!;
        public string? Program { get; set; }
        public string? Major { get; set; }
        public string? Concentration { get; set; }
        public short? YearAwarded { get; set; }
        public int PersonId { get; set; }

        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<DegreeJobPerson> DegreeJobPeople { get; set; }
    }
}
