using PreMatriculasParanoa.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PreMatriculasParanoa.Domain.Models.Base;
using Microsoft.AspNetCore.Http;
using PreMatriculasParanoa.Domain.Resources;
using PreMatriculasParanoa.Domain.Handlers.PlanejamentoMatriculaSequencial;
using PreMatriculasParanoa.Domain.Queries.Filters;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/planejamentos-matriculas-sequenciais")]
    [Produces("application/json")]
    public class PlanejamentoMatriculaSequencialController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CommandResult>> Incluir(
            [FromServices] IIncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler handler,
            [FromBody] PlanejamentoMatriculaSequencialAgrupadoViewModel vm)
        {
            return await Task.Run(() => 
            {
                var result = handler.Execute(vm);
                return StatusCode(result.StatusCode, result);
            });
        }

        [HttpGet("{id:int}")]
        public ActionResult<PlanejamentoMatriculaSequencialAgrupadoViewModel> Buscar(int id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpGet]
        public async Task<ActionResult<PlanejamentoMatriculaSequencialAgrupadoViewModel>> Buscar(
            [FromServices] IBuscarAgrupadoPlanejamentoMatriculaSequencialQueryHandler handler,
            [FromQuery] PlanejamentoMatriculaSequencialFilter filtro)
        {
            var result = await handler.Execute(filtro);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CommandResult>> Alterar(int id,
            [FromServices] IIncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler handler,
            [FromBody] PlanejamentoMatriculaSequencialAgrupadoViewModel vm)
        {
            return await Task.Run(() => 
            {
                if (id != vm.Id)
                {
                    var commandResult = new CommandResult(StatusCodes.Status400BadRequest);
                    commandResult.SetError(ValidationMessageResource.IdInvalido);
                    return StatusCode(commandResult.StatusCode, commandResult);
                }

                var result = handler.Execute(vm);
                return StatusCode(result.StatusCode, result);
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CommandResult>> Excluir(int id,
            [FromServices] IExcluirPlanejamentoMatriculaSequencialCommandHandler handler)
        {
            return await Task.Run(() => 
            {
                var result = handler.Execute(id);
                return StatusCode(result.StatusCode, result);
            });
        }

        [HttpGet("escolas/{idEscolaDestino:int}/anos-letivos/{ano:int}")]
        public async Task<ActionResult<int>> BuscarTotalPorEscolaEAnoLetivo(
            int idEscolaDestino, int ano,
            [FromServices] IBuscarTotalMatriculasSequencialPorPlanejamentoQueryHandler handler
            )
        {
            var filtro = new PlanejamentoMatriculaSequencialFilter 
            {
                IdEscolaDestino = idEscolaDestino,
                Year = ano
            };
            var result = await handler.Execute(filtro);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
