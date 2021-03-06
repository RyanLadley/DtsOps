﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace dtso.auth.Models.DataTransfer.Form
{
    public class UserRegistrationForm
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Passphrase { get; set; }
        public int Permissions { get; set; }
    }
}
