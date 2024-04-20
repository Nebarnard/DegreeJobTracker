﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DegreeJobTracker.Models {
    public partial class UserCredential {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    } // end class
} // end namespace
