using System;
using System.Collections.Generic;

namespace DegreeJobTracker.Models
{
    public partial class DegreeJobPerson
    {
        public int PersonId { get; set; }
        public int JobId { get; set; }
        public int DegreeId { get; set; }

        public virtual Degree Degree { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
    }
}
