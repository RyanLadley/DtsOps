using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class InvoiceTypeListing
    {
        public int InvoiceTypeId { get; set; }
        public string Name { get; set; }

        public static InvoiceTypeListing MapFromObject(InvoiceType obj)
        {
            if (obj == null)
                return new InvoiceTypeListing();

            return new InvoiceTypeListing()
            {
                InvoiceTypeId = obj.InvoiceTypeId,
                Name = obj.Name
            };
        }
    }
}
