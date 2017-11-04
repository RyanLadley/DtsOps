using dtso.core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class MaterialListing
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        
        public static MaterialListing MapFromObject(Material obj)
        {
            return new MaterialListing()
            {
                MaterialId = obj.MaterialId,
                Name = obj.Name,
                Unit = obj.Unit
            };
        }
        
    }
}
