using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.auth.Settings
{
    public class TokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; }
        public string SecurityKey { get; set; }

        private SymmetricSecurityKey _signingKey;
        /// <summary>
        /// Uses the Security key to create an encrypted signing key. Only Created Once
        /// </summary>
        public SymmetricSecurityKey SigningKey
        {
            get
            {
                if (_signingKey == null)
                    _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
                return _signingKey;
            }
        }

        /// <summary>
        /// Parses the configuration to create a TokenSettings object 
        /// </summary>
        public static TokenSettings parseFromConfig(IConfigurationRoot config)
        {
            return new TokenSettings()
            {
                Issuer = config["TokenSettings:Issuer"],
                Audience = config["TokenSettings:Audience"],
                Expiration = TimeSpan.Parse(config["TokenSettings:Expiration"]),
                SecurityKey = config["TokenSettings:SecurityKey"]
            };
        }
    }
}
