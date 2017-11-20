using dtso.data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.auth.Models.DataTransfer.Response
{
    public class UserResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Permissions { get; set; }

        public static UserResponse MapFromObject(User user)
        {
            var response = new UserResponse()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Permissions = Enum.Permissions.GetString(user.Permissions)
            };

            return response;
        }
    }
}
