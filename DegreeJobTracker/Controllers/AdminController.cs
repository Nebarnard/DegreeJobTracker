using DegreeJobTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

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
                    PersonId = (int)key.PersonId,
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

        // Add Views
        // Add Person View
        [HttpGet]
        public IActionResult Person() {
            // Action Name for page
            ViewBag.Action = "Add";

            return View("Person", new Person()); 
        } // end method

        // Add Degree View
        [HttpGet]
        public IActionResult Degree() {
            // Action Name for page
            ViewBag.Action = "Add";

            // Get latest person added to db
            var query = context.People
            .Where(p => p.PersonId == context.People.Max(p => p.PersonId))
            .GroupBy(p => new { p.FirstName, p.LastName })
            .Select(g => new {
                Name = $"{g.Key.FirstName} {g.Key.LastName}",
                MaxPersonId = g.Max(p => p.PersonId)
            })
            .FirstOrDefault();

            // Person name for page
            ViewBag.Name = $"{query.Name}";

            // PersonID for page
            ViewBag.PersonId = query.MaxPersonId;

            return View("Degree", new Degree());
        } // end method

        // Add Job View
        [HttpGet]
        public IActionResult Job() {
            // Action Name for page
            ViewBag.Action = "Add";

            // Get latest person added to db
            var query = context.People
            .Where(p => p.PersonId == context.People.Max(p => p.PersonId))
            .GroupBy(p => new { p.FirstName, p.LastName })
            .Select(g => new {
                Name = $"{g.Key.FirstName} {g.Key.LastName}",
                MaxPersonId = g.Max(p => p.PersonId)
            })
            .FirstOrDefault();

            // Person name for page
            ViewBag.Name = $"{query.Name}";

            // PersonID for page
            ViewBag.PersonId = query.MaxPersonId;

            // Degrees For Page
            ViewBag.Degrees = context.Degrees.OrderBy(d => d.DegreeId).Where(d => d.PersonId == query.MaxPersonId).ToList();

            return View("Job", new JobDegreePersonPost());
        } // end method

        // Posts
        [HttpPost]
        public IActionResult Person(Person person) {
            if (ModelState.IsValid) {
                if (person.PersonId == 0) 
                    context.People.Add(person);
                else
                    context.People.Update(person);
                context.SaveChanges();
                return RedirectToAction("Degree", "Admin");
            } else {
                ViewBag.Action = (person.PersonId == null) ? "Add" : "Edit";
                return View(person);
            }
        } // end method

        // Posts
        [HttpPost]
        public IActionResult Degree(Degree degree) {
            if (ModelState.IsValid) {
                if (degree.DegreeId == null)
                    context.Degrees.Add(degree);
                else
                    context.Degrees.Update(degree);
                context.SaveChanges();
                return RedirectToAction("Job", "Admin");
            } else {
                ViewBag.Action = (degree.DegreeId == null) ? "Add" : "Edit";
                return View(degree);
            }
        } // end method

        // Posts
        [HttpPost]
        public IActionResult Job(JobDegreePersonPost jdp) {
            // Convert jdp To job
            Job job = jdp;

            // Insert Job into Database
            if (ModelState.IsValid) {
                if (job.JobId == null) {
                    context.Jobs.Add(job);
                } else {
                    context.Jobs.Update(job);
                } // end if

                // Save Context
                context.SaveChanges();

                // Get Id of latest job
                var job_id = context.Jobs
                    .Where(j => j.PersonId == jdp.PersonId)
                    .OrderByDescending(j => j.JobId)
                    .Select(j => j.JobId)
                    .FirstOrDefault();

                // Create DegreeJobPerson Object
                DegreeJobPerson djp = new DegreeJobPerson();
                djp.DegreeId = jdp.DegreeId;    // DegreeId
                djp.JobId = (int)job_id;        // JobId
                djp.PersonId = jdp.PersonId;    // PersonId

                // Database Connection String
                string connectionString = "Server=(localdb)\\mssqllocaldb;Database=DegreeJobTracker;";

                // Sql Statement
                string sql = "INSERT INTO degree_job_person (degree_id, job_id, person_id)" +
                            $"VALUES({djp.DegreeId}, {djp.JobId}, {djp.PersonId});";
                // Insert DegreeJobPerson Into Database
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand object with the SQL statement and the SqlConnection
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        // Execute the SQL command
                        using (SqlDataReader reader = command.ExecuteReader()) {
                        }
                    }
                }

                    return RedirectToAction("Index", "Admin");
            } else {
                ViewBag.Action = (job.JobId == null) ? "Add" : "Edit";
                return View(job);
            }
        } // end method

    } // end class
} // end namespace
