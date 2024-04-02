using DegreeJobTracker.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Login()
        {
            // Your login logic or just return the view
            return View();
        }

        public IActionResult Admin()
        {
            // Your admin logic or just return the view
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}