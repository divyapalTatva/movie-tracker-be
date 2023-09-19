using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Tracker_Common.CommonMethods
{
    public class CommonMethods
    {
        IConfiguration configuration;
        public CommonMethods(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string CreateJWt()
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var signingCredential = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
            var subject = new ClaimsIdentity(new[] {
            new Claim(JwtRegisteredClaimNames.Sub,"Subject")
            });
            var expires = DateTime.UtcNow.AddDays(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Issuer = issuer,
                Expires = expires,
                Audience = audience,
                SigningCredentials = signingCredential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
