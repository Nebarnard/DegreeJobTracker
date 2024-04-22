using DegreeJobTracker.Models.Context;
using DegreeJobTracker.Models.ViewModel;
using DegreeJobTracker.Models.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;

namespace DegreeJobTracker.Controllers
{
    public class AccountController : Controller {

        // Allow For Connection String Access for places that dont use EFCore
        private readonly string _connectionString;
        private DegreeJobTrackerContext context { get; set; }

        public AccountController(DegreeJobTrackerContext ctx, IConfiguration configuration) {
            // Set DbContext
            context = ctx;

            // Set ConnectionString
            _connectionString = configuration.GetConnectionString("DegreeJobTrackerContext");
        } // end method

        public IActionResult Index() {
            return View();
        } // end method


        // Change Username
        [HttpGet]
        public IActionResult Username() {
            // Create Model
            UsernameViewModel un = new UsernameViewModel();

            // Set Username
            un.Username = HttpContext.Session.GetString("Username");

            return View("Username", un);
        } // end method

        [HttpPost]
        public IActionResult Username(UserCredential uc) {
            // Check if equal
            if (uc.Username != null && uc.Username != string.Empty && uc.Username.Length < 51 && (!context.DoesPrimaryKeyExist<UserCredential>(uc.Username) || uc.Username == HttpContext.Session.GetString("Username"))) {
                // Asssing Old Username to Variable
                string old_un = HttpContext.Session.GetString("Username");

                // Get Password
                uc.Password = context.UserCredentials
                            .Where(u => u.Username == old_un)
                            .Select(u => u.Password)
                            .FirstOrDefault();


                string sqlStr = "DELETE FROM user_credential WHERE username = @oldUsername;" +
                                "INSERT INTO user_credential (username, password) " +
                                "VALUES (@newUsername, @password)";

                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlStr, connection)) {
                        // Add parameters
                        command.Parameters.AddWithValue("@oldUsername", old_un);
                        command.Parameters.AddWithValue("@newUsername", uc.Username);
                        command.Parameters.AddWithValue("@password", uc.Password);

                        // Execute the SQL command
                        command.ExecuteNonQuery();
                    }
                }

                // Update Session
                HttpContext.Session.SetString("Username", uc.Username);

                return RedirectToAction("Index");
            } else {
                // Determine which error occured
                if (uc.Username == string.Empty || uc.Username == null) {
                    ViewData["Error"] = "Username must be at least 1 character.";
                } else if (uc.Username.Length > 50) {
                    ViewData["Error"] = "Username must be 50 characters or less.";
                } else if (context.DoesPrimaryKeyExist<UserCredential>(uc.Username) || !(uc.Username == HttpContext.Session.GetString("Username"))) {
                    ViewData["Error"] = "Username already exists";
                } // end if

                // Return to View
                return View("Username");
            } // end if
        } // end method


        // Change Password
        [HttpGet]
        public IActionResult Password() {
            // Create Model
            PasswordViewModel p = new PasswordViewModel();

            return View("Password", p);
        } // end method

        [HttpPost]
        public IActionResult Password(PasswordViewModel p) {
            // Get Username
            string username = HttpContext.Session.GetString("Username");

            // Get Current Password
            var password = context.UserCredentials
                .Where(u => u.Username == username)
                .Select(u => u.Password)
                .FirstOrDefault();

            // Validation
            if (ModelState.IsValid) {
                if (PasswordHasher.HashPassword(p.Password) != password) {
                    // Create UserCredentials Object
                    UserCredential uc = new UserCredential();
                    uc.Username = username;
                    uc.Password = PasswordHasher.HashPassword(p.Password);


                    context.UserCredentials.Update(uc);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                } else {
                    return RedirectToAction("Index");
                } // end else
            } else {
                return View();
            } // end else
        } // end method


        // Delete Credentials
        [HttpGet]
        public IActionResult Delete() {
            return View();
        } // end method

        [HttpPost]
        public IActionResult Delete(UserCredential uc) {
            // Get Username
            string username = HttpContext.Session.GetString("Username");

            // Get Current Password
            var password = context.UserCredentials
                .Where(u => u.Username == username)
                .Select(u => u.Password)
                .FirstOrDefault();

            // Get Count of Users in Database
            int userCount = context.UserCredentials.Count();

            // Create UserCredentials Object
            uc.Username = username;
            uc.Password = password;

            // Validation
            if (uc != null) {
                if (userCount > 1) {
                    context.UserCredentials.Remove(uc);
                    context.SaveChanges();

                    // Remove Session
                    HttpContext.Session.Remove("Username");
                    HttpContext.Session.Remove("LoggedIn");

                    return RedirectToAction("Index", "Home");
                } else {
                    ViewData["Error"] = "Cannot delete only user in database.";
                    return View("Index");
                } // end else
            } else {
                ViewData["Error"] = "There was an error deleting your account please try again later.";
                return View("Index");
            } // end else
        } // end method
    } // end class
} // end namespace
