using DegreeJobTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DegreeJobTracker.Controllers {
    public class HomeController : Controller {
        private DegreeJobTrackerContext context { get; set; }

        public HomeController(DegreeJobTrackerContext ctx) => context = ctx;


        public IActionResult Index() {
            var query = context.Jobs
            .Join(context.DegreeJobPeople,
                  j => j.JobId,
                  djp => djp.JobId,
                  (j, djp) => new { Job = j, DegreeJobPerson = djp })
            .Join(context.Degrees,
                  jdjp => jdjp.DegreeJobPerson.DegreeId,
                  d => d.DegreeId,
                  (jdjp, d) => new HomeViewModel {
                      Job = jdjp.Job.JobTitle,
                      Business = jdjp.Job.BusinessName,
                      Salary = jdjp.Job.Salary,
                      Description = jdjp.Job.Description,
                      Degree = d.Type + " " + d.Major
                  })
            .ToList();

            return View(query);
        }

        public IActionResult Privacy() {
            return View();
        }

        public IActionResult Login() {
            // Your login logic or just return the view
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserCredential uc) {
            // Get password where username is from database
            var password = context.UserCredentials
                .Where(u => u.Username == uc.Username)
                .Select(u => u.Password)
                .FirstOrDefault();

            // Hash Password
            string user_password = PasswordHasher.HashPassword(uc.Password);

            // Take hash of Credential Passowrd and Compare to Database
            if (user_password == password) {
                // Set Session
                HttpContext.Session.SetString("LoggedIn", "true");
                HttpContext.Session.SetString("Username", uc.Username);

                return RedirectToAction("Index", "Admin");
            }
            ViewData["Error"] = "* Incorrect Username or Password.";
            return View();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}