using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class Bug
    {
        public int BugId { get; set; }
        public string Severity { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
