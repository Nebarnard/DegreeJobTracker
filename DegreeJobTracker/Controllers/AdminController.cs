using DegreeJobTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DegreeJobTracker.Controllers {
    public class AdminController : Controller {

        private DegreeJobTrackerContext context { get; set; }

        public AdminController(DegreeJobTrackerContext ctx) => context = ctx;

        // Admin Home
        public IActionResult Index() {
            var query = context.People
            .Join(context.DegreeJobPeople,
                  p => p.PersonId,
                  djp => djp.PersonId,
                  (p, djp) => new { Person = p, DegreeJobPerson = djp })
            .Join(context.Jobs,
                  pd => pd.DegreeJobPerson.JobId,
                  j => j.JobId,
                  (pd, j) => new { pd.Person, pd.DegreeJobPerson, Job = j })
            .Join(context.Degrees,
                  pdj => pdj.DegreeJobPerson.DegreeId,
                  d => d.DegreeId,
                  (pdj, d) => new { pdj.Person, DegreeJobPerson = pdj.DegreeJobPerson, pdj.Job, Degree = d })
            .GroupBy(
                pdjd => new { pdjd.Person.PersonId, pdjd.Person.FirstName, pdjd.Person.LastName, pdjd.Degree.Type, pdjd.Degree.Major, pdjd.Job.JobTitle, pdjd.Job.Salary },
                (key, group) => new DegreeJobPersonAdminViewModel {
                    PersonId = key.PersonId,
                    Name = $"{key.FirstName} {key.LastName}",
                    Degree = $"{key.Type} {key.Major}",
                    Job = key.JobTitle,
                    Salary = (decimal)key.Salary
                })
            .ToList();

            return View(query);
        } // end method

        // Admin View All Info
        public IActionResult Info(int id) {
            // Get Degree
            var degrees = context.Degrees
            .Where(d => d.PersonId == id)
            .ToList();

            // Get Job
            var jobs = context.Jobs
            .Where(j => j.PersonId == id)
            .ToList();

            // Get Name
            var name = context.People
                .Where(p => p.PersonId == id)
                .Select(p => p.FirstName + " " + p.LastName)
                .FirstOrDefault();

            return View(new ViewAllInfoViewModel(id, name, jobs, degrees));
        } // end method

        // Add Person View
        [HttpGet]
        public IActionResult Person() {
            // Action Name for page
            ViewBag.Action = "Add";

            return View(); 
        } // end method

        // Add Degree View
        [HttpGet]
        public IActionResult Degree() {
            // Action Name for page
            ViewBag.Action = "Add";

            // Person name for page
            ViewBag.Name = "Person Name";

            return View();
        } // end method

        // Add Job View
        [HttpGet]
        public IActionResult Job() {
            // Action Name for page
            ViewBag.Action = "Add";

            // Person name for page
            ViewBag.Name = "Person Name";

            return View();
        } // end method

    } // end class
} // end namespace
