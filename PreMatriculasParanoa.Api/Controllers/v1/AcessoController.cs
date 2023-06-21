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
    [Route("api/v1/acessos")]
    [Produces("application/json")]
    [SwaggerTag("Rotas para autenticação e controle de acessos de usuaários de sistema")]
    public class AcessoController : ControllerBase
    {
        [HttpPost("autenticar")]
        public ActionResult<TokenAutenticacaoViewModel> Autenticar(
            [FromBody] AutenticacaoUsuarioViewModel vm,
            [FromServices] IAutenticarUsuarioCommandHandler handler)
        {
            var result = handler.Execute(vm);

            if (result == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var tokenDto = TokenHelper.GenerateToken(result);

            return StatusCode(StatusCodes.Status200OK, tokenDto);
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
                Permissoes = string.Join(", ", User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList())
            };

            return StatusCode(StatusCodes.Status200OK, status);
        }
    }
}
