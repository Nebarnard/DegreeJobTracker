using Microsoft.AspNetCore.Mvc;

namespace DegreeJobTracker.Controllers {
    public class AdminController : Controller {
        // Admin Home
        public IActionResult Index() {
            return View();
        } // end method

        // Add Person View
        [HttpGet]
        public IActionResult Person() {
            // Action Name for page
            ViewBag.Action = "Add";

            return View(); 
        } // end method

    } // end class
} // end namespace
