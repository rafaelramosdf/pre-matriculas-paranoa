using PreMatriculasParanoa.Domain.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PreMatriculasParanoa.Api.Helpers
{
    public static class TokenHelper
    {
        public static TokenAutenticacaoViewModel GenerateToken(UsuarioViewModel user)
        {
            var tokenDto = new TokenAutenticacaoViewModel();
            tokenDto.ExpiraEm = DateTime.Now.AddHours(2);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("PrivateKey"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Hash, user.IdUsuario.ToString()),
                new Claim(ClaimTypes.GroupSid, user.IdGrupoDePermissao.ToString())
            };

            //user.GrupoDePermissao.Permissoes.ToList().ForEach(x =>
            //{
            //    if (x.Selecionado)
            //        claims.Add(new Claim(ClaimTypes.Role, x.Nome));
            //});

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = tokenDto.ExpiraEm.ToUniversalTime(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);            
            tokenDto.Token = tokenHandler.WriteToken(token);

            return tokenDto;
        }
    }
}
