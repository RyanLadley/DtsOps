using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public int Permissions { get; set; }
    }
}
