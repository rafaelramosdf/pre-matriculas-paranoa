using PreMatriculasParanoa.Domain.Handlers.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PreMatriculasParanoa.Api.Helpers;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using PreMatriculasParanoa.Domain.Models.ViewModels;

namespace PreMatriculasParanoa.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/token")]
    [Produces("application/json")]
    [SwaggerTag("Rotas para autenticação e consulta de dados de usuário autenticado")]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        public ActionResult<TokenAutenticacaoViewModel> Post(
            [FromBody] AutenticacaoUsuarioViewModel vm,
            [FromServices] IAutenticarUsuarioCommandHandler handler)
        {
            var result = handler.Execute(vm);

            if (result == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var token = TokenHelper.GenerateToken(result);

            return StatusCode(StatusCodes.Status200OK, token);
        }

        [Authorize]
        [HttpGet("status")]
        [SwaggerOperation("Retorna o status do usuário autenticado")]
        public ActionResult<AutenticacaoUsuarioViewModel> Status()
        {
            var status = new DadosAutenticacaoUsuarioViewModel
            {
                IdUsuario = User.Claims.Where(c => c.Type == ClaimTypes.Hash).Select(c => c.Value).FirstOrDefault(),
                IdGrupoPermissao = User.Claims.Where(c => c.Type == ClaimTypes.GroupSid).Select(c => c.Value).FirstOrDefault(),
                Usuario = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault(),
                Nome = User.Identity.Name,
                Email = User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault(),
                Autenticado = User.Identity.IsAuthenticated,
                Perfil = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault()
            };

            return StatusCode(StatusCodes.Status200OK, status);
        }
    }
}
