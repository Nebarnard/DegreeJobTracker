using System;
using System.Collections.Generic;

namespace DegreeJobTracker.Models {
    public partial class JobDegreePersonPost : Job {

        public JobDegreePersonPost() { }

        public JobDegreePersonPost(Job job) { 
            this.JobId = job.JobId;
            this.JobTitle= job.JobTitle;
            this.BusinessName = job.BusinessName;
            this.Salary = job.Salary;
            this.Description= job.Description;
            this.PersonId = job.PersonId;
        }


        public int DegreeId { get; set; }
    }
}
