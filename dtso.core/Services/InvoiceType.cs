using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Services
{
    public class InvoiceType
    {
        public int InvoiceTypeId { get; set; }
        public string Name { get; set; }

        public static InvoiceType MapFromEntity(data.Entities.InvoiceType entity)
        {
            if (entity == null)
                return new InvoiceType();

            return new InvoiceType()
            {
                InvoiceTypeId = entity.InvoiceTypeId,
                Name = entity.Name
            };
        }
    }
}
