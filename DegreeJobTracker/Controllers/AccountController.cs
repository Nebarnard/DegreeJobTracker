using Microsoft.AspNetCore.Mvc;

namespace DegreeJobTracker.Controllers {
    public class AccountController : Controller {
        public IActionResult Index() {
            return View();
        } // end method


        // Change Username
        [HttpGet]
        public IActionResult Username() {
            return View();
        } // end method

        [HttpPost]
        public IActionResult Username(string username) {
            return RedirectToAction("Index");
        } // end method


        // Change Password
        [HttpGet]
        public IActionResult Password() {
            return View();
        } // end method

        [HttpPost]
        public IActionResult Password(string password) {
            return RedirectToAction("Index");
        } // end method


        // Delete Credentials
        [HttpGet]
        public IActionResult Delete() {
            return View();
        } // end method

        [HttpPost]
        public IActionResult Delete(string delete) {
            return RedirectToAction("Index");
        } // end method
    } // end class
} // end namespace
