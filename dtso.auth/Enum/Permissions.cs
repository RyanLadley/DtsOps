using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.auth.Enum
{
    public static class Permissions
    {
        public static readonly string ADMIN = "Admin";
        public static readonly string USER = "User";


        public static string GetString(int permssionLevel)
        {
            switch (permssionLevel)
            {
                case 0:
                    return Permissions.ADMIN;
                case 1:
                    return Permissions.USER;
                default:
                    return Permissions.USER;
            }
        }
    }
}
