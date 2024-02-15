using Microsoft.AspNetCore.Mvc;

namespace DegreeJobTracker.Controllers {
    public class AdminController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
