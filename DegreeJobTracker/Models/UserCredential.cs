using System;
using System.Collections.Generic;

namespace DegreeJobTracker.Models
{
    public partial class UserCredential
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
