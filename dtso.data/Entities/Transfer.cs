using dtso.data.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public string Description { get; set; }

        public int FromAccountId { get; set; }
        public Account FromAccount { get; set; }
        public vAccount FromvAccount { get; set; }

        public int ToAccountId { get; set; }
        public Account ToAccount { get; set; }
        public vAccount TovAccount { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
