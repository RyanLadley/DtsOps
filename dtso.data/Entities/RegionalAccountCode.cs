using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class RegionalAccountCode
    {
        public int RegionalAccountCodeId { get; set; }
        public int AccountNumber { get; set; }
        public int FundNumber { get; set; }
        public int DeptartmentNumber { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectDescription { get; set; }
        public string AccountPrefix { get; set; }
    }
}
