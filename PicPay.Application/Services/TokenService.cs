using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _chave;
        public TokenService(IConfiguration config)
        {
            _chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["chaveSecreta"]!));
        }

        public string GerarToken(string email, string password)
        {
            var claims = new List<Claim> 
            {
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Sub, password)

            };

            var credencials = new SigningCredentials(_chave, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = credencials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}
