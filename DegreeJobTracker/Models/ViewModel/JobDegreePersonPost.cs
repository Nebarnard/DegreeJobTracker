using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DegreeJobTracker.Models.Context;

namespace DegreeJobTracker.Models.ViewModel {
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

        [Required(ErrorMessage ="Please Select a degree.")]
        public int DegreeId { get; set; }
    }
}
