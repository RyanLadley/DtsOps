using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.auth.Models.DataTransfer.Response
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public UserResponse User{ get; set; }
    }
}
