using dtso.auth.Models.Buisness;
using dtso.auth.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using dtso.auth.Models.DataTransfer.Response;
using dtso.data.Entities;
using dtso.auth.Enum;
using Microsoft.Extensions.Options;

namespace dtso.auth.Logic
{
    public class TokenManager
    {
        private TokenSettings _settings;

        public TokenManager(IOptions<AuthSettings> authSettings)
        {
            _settings = authSettings.Value.TokenSettings;
        }

        /// <summary>
        /// Gets that user id from the collection of claims provided.
        /// </summary>
        public UserInfo ExtractUserInformation(IEnumerable<Claim> claims)
        {
            Int32.TryParse(claims.FirstOrDefault(claim => claim.Type == "UserId").Value, out int userId);

            var userInfo = new UserInfo
            {
                UserId = userId,
                Permissions = claims.FirstOrDefault(claim => claim.Type == "Permissions").Value
            };

            return userInfo;
        }

        /// <summary>
        /// Using the options provided, this returns SigningCredentials for a toke.
        /// </summary>
        /// <returns></returns>
        public SigningCredentials GetSigningCredentials()
        {
            var signingCredentials = new SigningCredentials(
                _settings.SigningKey,
                SecurityAlgorithms.HmacSha512);

            return signingCredentials;
        }

        /// <summary>
        /// Generates a JWT Token tailored to the proved user
        /// </summary>
        /// <returns>JWT Token Response</returns>
        public TokenResponse CreateToken(data.Entities.User user)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Permissions", Permissions.GetString(user.Permissions))
            };

            var signingCredentials = GetSigningCredentials();

            var jwt = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_settings.Expiration),
                signingCredentials: signingCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenResponse
            {
                AccessToken = encodedJwt,
                ExpiresIn = DateTime.Now.AddSeconds((int)_settings.Expiration.TotalSeconds),
                User = UserResponse.MapFromObject(user)
            };

            return response;
        }
    }
}
