using PreMatriculasParanoa.Domain.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PreMatriculasParanoa.Api.Helpers
{
    public static class TokenHelper
    {
        public static TokenAutenticacaoViewModel GenerateToken(UsuarioViewModel user)
        {
            var tokenVm = new TokenAutenticacaoViewModel();
            tokenVm.ExpiraEm = DateTime.Now.AddHours(2);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("PrivateKey"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Hash, user.IdUsuario.ToString()),
                new Claim(ClaimTypes.GroupSid, user.Perfil),
                new Claim(ClaimTypes.Role, user.Perfil)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = tokenVm.ExpiraEm.ToUniversalTime(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);            
            tokenVm.Token = tokenHandler.WriteToken(token);

            return tokenVm;
        }
    }
}
