using DegreeJobTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;

namespace DegreeJobTracker.Controllers {
    public class AdminController : Controller {

        // Allow For Connection String Access for places that dont use EFCore
        private readonly string _connectionString;

        private DegreeJobTrackerContext context { get; set; }

        public AdminController(DegreeJobTrackerContext ctx, IConfiguration configuration) {
            // Set DbContext
            context = ctx;

            // Set ConnectionString
            _connectionString = configuration.GetConnectionString("DegreeJobTrackerContext");
        } // end method

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

        // Logout
        public IActionResult Logout() {
            // Set Session
            HttpContext.Session.SetString("LoggedIn", "false");
            return RedirectToAction("Index", "Home");
        } // end method


        // Cancel Button for adding person
        public IActionResult Cancel(int id) {
            // Switch based on what page on
            switch (id) {

                // Add Person
                case 0:
                    return RedirectToAction("Index", "Admin");

                // Add Degree
                case 1:
                    // Get Latest Person
                    var query = context.People.OrderByDescending(p => p.PersonId).FirstOrDefault();

                    // Remove From Database
                    context.Remove(query);
                    context.SaveChanges();

                    return RedirectToAction("Index", "Admin");

                // Add Job
                case 2:
                    // Get Latest Person
                    var personQuery = context.People.OrderByDescending(p => p.PersonId).FirstOrDefault();

                    // Get Latest Degree
                    var degreeQuery = context.Degrees.OrderByDescending(d => d.DegreeId).FirstOrDefault();

                    // Remove From Database
                    context.Remove(personQuery);
                    context.Remove(degreeQuery);
                    context.SaveChanges();

                    return RedirectToAction("Index", "Admin");

                default:
                    return RedirectToAction("Index", "Admin");
            } // end switch
        } // end method

        #region Add Views
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
        #endregion

        #region Edits
        // Edit Person
        [HttpGet("Admin/Person/{id}")]
        public IActionResult Person(int id) {
            ViewBag.Action = "Edit";

            var person = context.People.Find(id);
            return View(person);
        } // end method

        // Edit Degree View
        [HttpGet("Admin/Degree/{id}")]
        public IActionResult Degree(int id) {
            // Action Name for page
            ViewBag.Action = "Edit";

            // Get person_id
            var person_id = context.DegreeJobPeople
                .Where(djp => djp.DegreeId == id)
                .Select(djp => djp.PersonId)
                .FirstOrDefault();

            // Get Person name
            var name = context.People
                .Where(p => p.PersonId == person_id)
                .Select(p => p.FirstName + " " + p.LastName)
                .FirstOrDefault();

            // Get Degree
            var degree = context.Degrees.Find(id);

            // Person name for page
            ViewBag.Name = name;

            // PersonID for page
            ViewBag.PersonId = person_id;

            return View("Degree", degree);
        } // end method

        // Edit Job View
        [HttpGet("Admin/Job/{id}")]
        public IActionResult Job(int id) {
            // Action Name for page
            ViewBag.Action = "Edit";

            // Get person_id
            var person_id = context.DegreeJobPeople
                .Where(djp => djp.JobId == id)
                .Select(djp => djp.PersonId)
                .FirstOrDefault();

            // Get Person name
            var name = context.People
                .Where(p => p.PersonId == person_id)
                .Select(p => p.FirstName + " " + p.LastName)
                .FirstOrDefault();

            // Get degree_id
            var degree_id = context.DegreeJobPeople
                .Where(djp => djp.JobId == id)
                .Select(djp => djp.DegreeId)
                .FirstOrDefault();

            // Get job
            var job = context.Jobs.Find(id);

            // Set Job Id
            job.JobId = id;

            // Person name for page
            ViewBag.Name = name;

            // PersonID for page
            ViewBag.PersonId = person_id;

            // Degrees For Page
            ViewBag.Degrees = context.Degrees.OrderBy(d => d.DegreeId).Where(d => d.PersonId == person_id).ToList();

            JobDegreePersonPost jdp = new JobDegreePersonPost(job);
            jdp.DegreeId = degree_id;

            return View("Job", jdp);
        } // end method
        #endregion

        #region Posts
        // Post Person
        [HttpPost]
        public IActionResult Person(Person person) {
            if (ModelState.IsValid) {
                if (person.PersonId != 0 && person.PersonId != null) {
                    context.People.Update(person);
                    context.SaveChanges();
                    return RedirectToAction("Info","Admin", new { id = person.PersonId });
                }
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

        // Post Degree
        [HttpPost]
        public IActionResult Degree(Degree degree) {
            if (ModelState.IsValid) {
                if (degree.DegreeId != 0 && degree.DegreeId != null) {
                    context.Degrees.Update(degree);
                    context.SaveChanges();
                    return RedirectToAction("Info", "Admin", new { id = degree.PersonId });
                }
                if (degree.DegreeId == null || degree.DegreeId == 0) {
                    degree.DegreeId = null;
                    context.Degrees.Add(degree);
                }
                else
                    context.Degrees.Update(degree);
                context.SaveChanges();
                return RedirectToAction("Job", "Admin");
            } else {
                ViewBag.Action = (degree.DegreeId == null || degree.DegreeId == 0) ? "Add" : "Edit";
                ViewBag.PersonId = degree.PersonId;
                ViewBag.Name = context.People.Where(p => p.PersonId == degree.PersonId).Select(p => p.FirstName + ' ' + p.LastName).FirstOrDefault();
                return View(degree);
            }
        } // end method

        // Post Job
        [HttpPost]
        public IActionResult Job(JobDegreePersonPost jdp) {
            // Convert jdp To job
            Job job = jdp;

            // Insert Job into Database
            if (ModelState.IsValid) {
                if (job.JobId != 0 && job.JobId != null) {
                    context.Jobs.Update(job);
                    context.SaveChanges();

                    // Change jdp if necessary
                    // Get old degree_id
                    var degree_id = context.DegreeJobPeople
                        .Where(djp => djp.JobId == job.JobId)
                        .Select(djp => djp.DegreeId)
                        .FirstOrDefault();

                    // Check if equal
                    if (jdp.DegreeId != degree_id) {
                        // Create DegreeJobPerson Object
                        DegreeJobPerson djpRow = new DegreeJobPerson();
                        djpRow.DegreeId = jdp.DegreeId;    // DegreeId
                        djpRow.JobId = (int)jdp.JobId;     // JobId
                        djpRow.PersonId = jdp.PersonId;    // PersonId

                        // Sql Statement
                        string sqlStr = "INSERT INTO degree_job_person (degree_id, job_id, person_id)" +
                                    $"VALUES({djpRow.DegreeId}, {djpRow.JobId}, {djpRow.PersonId});";
                        // Insert DegreeJobPerson Into Database
                        using (SqlConnection connection = new SqlConnection(_connectionString)) {
                            // Open the connection
                            connection.Open();

                            // Create a SqlCommand object with the SQL statement and the SqlConnection
                            using (SqlCommand command = new SqlCommand(sqlStr, connection)) {
                                // Execute the SQL command
                                using (SqlDataReader reader = command.ExecuteReader()) {
                                }
                            }
                        }

                        return RedirectToAction("Info", "Admin", new { id = djpRow.PersonId});
                    } else {
                        return RedirectToAction("Info", "Admin", new { id = jdp.PersonId });
                    } // end if
                }
                if (job.JobId == null || job.JobId == 0) {
                    job.JobId = null;
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

                // Sql Statement
                string sql = "INSERT INTO degree_job_person (degree_id, job_id, person_id)" +
                            $"VALUES({djp.DegreeId}, {djp.JobId}, {djp.PersonId});";
                // Insert DegreeJobPerson Into Database
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
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
                ViewBag.Action = (job.JobId == null || job.JobId == 0) ? "Add" : "Edit";
                ViewBag.PersonId = jdp.PersonId;
                ViewBag.Name = context.People.Where(p => p.PersonId == jdp.PersonId).Select(p => p.FirstName + ' ' + p.LastName).FirstOrDefault();
                ViewBag.Degrees = context.Degrees.OrderBy(d => d.DegreeId).Where(d => d.PersonId == jdp.PersonId).ToList();
                return View(job);
            }
        } // end method
        #endregion

        #region Delete
        // Delete Person
        [HttpGet]
        public IActionResult DeletePerson(int id) {
            var person = context.People.Find(id);
            return View(person);
        }
        [HttpPost]
        public IActionResult DeletePerson(Person person) {

            // Get all entries in DJP with person id
            var query = context.DegreeJobPeople.Where(p => p.PersonId == person.PersonId).ToList();

            // Delete Jobs
            foreach (var item in query) {
                // Delete DJP
                // Create DegreeJobPerson Object
                DegreeJobPerson djp = new DegreeJobPerson();
                djp.DegreeId = item.DegreeId;    // DegreeId
                djp.JobId = (int)item.JobId;     // JobId
                djp.PersonId = item.PersonId;    // PersonId

                // Sql Statement
                string sqlStr = "DELETE FROM degree_job_person " +
                                $"WHERE degree_id = {djp.DegreeId} and person_id = {djp.PersonId} and job_id = {djp.JobId};" +
                                "DELETE FROM job " +
                                $"WHERE job_id = {item.JobId};" +
                                "DELETE FROM degree " +
                                $"WHERE degree_id = {item.DegreeId};" +
                                "DELETE FROM person " +
                                $"WHERE person_id = {item.PersonId};";

                // Insert DegreeJobPerson Into Database
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand object with the SQL statement and the SqlConnection
                    using (SqlCommand command = new SqlCommand(sqlStr, connection)) {
                        // Execute the SQL command
                        using (SqlDataReader reader = command.ExecuteReader()) {
                        }
                    }
                }
            } // end foreach
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult DeleteDegree(int id) {
            var degree = context.Degrees.Find(id);
            return View(degree);
        }
        [HttpPost]
        public IActionResult DeleteDegree(Degree degree) {
            // Get all entries in DJP with degree id
            var query = context.DegreeJobPeople.Where(d => d.DegreeId == degree.DegreeId).ToList();

            // Person Id
            int person_id = 0;

            // Delete Jobs
            foreach (var item in query) {
                // Delete DJP
                // Create DegreeJobPerson Object
                DegreeJobPerson djp = new DegreeJobPerson();
                djp.DegreeId = item.DegreeId;    // DegreeId
                djp.JobId = (int)item.JobId;     // JobId
                djp.PersonId = item.PersonId;    // PersonId

                // Set Person Id
                person_id = djp.PersonId;


                // Sql Statement
                string sqlStr = "DELETE FROM degree_job_person " +
                                $"WHERE degree_id = {djp.DegreeId} and person_id = {djp.PersonId} and job_id = {djp.JobId};" +
                                "DELETE FROM degree " +
                                $"WHERE degree_id = {item.DegreeId};" +
                                "DELETE FROM job " +
                                $"WHERE job_id = {item.JobId};";

                // Insert DegreeJobPerson Into Database
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand object with the SQL statement and the SqlConnection
                    using (SqlCommand command = new SqlCommand(sqlStr, connection)) {
                        // Execute the SQL command
                        using (SqlDataReader reader = command.ExecuteReader()) {
                        }
                    }
                }
            } // end foreach
            return RedirectToAction("Info", "Admin", new {id = person_id});
        }

        [HttpGet]
        public IActionResult DeleteJob(int id) { 
            var job = context.Jobs.Find(id);
            return View(job);
        }
        [HttpPost]
        public IActionResult DeleteJob(Job job) {
            // Get all entries in DJP with job id
            var query = context.DegreeJobPeople.Where(j => j.JobId == job.JobId).ToList();

            // Person Id
            int person_id = 0;

            // Delete Jobs
            foreach (var item in query) {
                // Delete DJP
                // Create DegreeJobPerson Object
                DegreeJobPerson djp = new DegreeJobPerson();
                djp.DegreeId = item.DegreeId;    // DegreeId
                djp.JobId = (int)item.JobId;     // JobId
                djp.PersonId = item.PersonId;    // PersonId

                // Set Person Id
                person_id = djp.PersonId;


                // Sql Statement
                string sqlStr = "DELETE FROM degree_job_person " +
                                $"WHERE degree_id = {djp.DegreeId} and person_id = {djp.PersonId} and job_id = {djp.JobId};" +
                                "DELETE FROM job " +
                                $"WHERE job_id = {item.JobId};";

                // Insert DegreeJobPerson Into Database
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand object with the SQL statement and the SqlConnection
                    using (SqlCommand command = new SqlCommand(sqlStr, connection)) {
                        // Execute the SQL command
                        using (SqlDataReader reader = command.ExecuteReader()) {
                        }
                    }
                }
            } // end foreach
            return RedirectToAction("Info", "Admin", new {id = person_id});
        }
        #endregion

    } // end class
} // end namespace
