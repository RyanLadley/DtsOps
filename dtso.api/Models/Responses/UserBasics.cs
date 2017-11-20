using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class UserBasics
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Permissions { get; set; }

        public static UserBasics MapFromEntity(data.Entities.User entity)
        {
            return new UserBasics()
            {
                UserId = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Permissions = entity.Permissions
            };
        }
    }
}
