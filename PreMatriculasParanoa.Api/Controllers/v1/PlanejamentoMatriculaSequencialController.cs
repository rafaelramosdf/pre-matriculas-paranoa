using PreMatriculasParanoa.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PreMatriculasParanoa.Domain.Models.Base;
using Microsoft.AspNetCore.Http;
using PreMatriculasParanoa.Domain.Resources;
using PreMatriculasParanoa.Domain.Handlers.PlanejamentoMatriculaSequencial;
using PreMatriculasParanoa.Domain.Queries.Filters;

namespace PreMatriculasParanoa.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/planejamentos-matriculas-sequenciais")]
    [Produces("application/json")]
    public class PlanejamentoMatriculaSequencialController : ControllerBase
    {
        [HttpPost]
        public ActionResult<CommandResult> Incluir(
            [FromServices] IIncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler handler,
            [FromBody] PlanejamentoMatriculaSequencialAgrupadoViewModel vm)
        {
            var result = handler.Execute(vm);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PlanejamentoMatriculaSequencialAgrupadoViewModel> Buscar(int id,
            [FromServices] IBuscarPlanejamentoMatriculaSequencialPorIdQueryHandler handler)
        {
            var result = handler.Execute(id);

            if (result == null)
                return NotFound(GeneralMessageResource.NenhumRecursoEncontradoParaID);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        public ActionResult<DataTableModel<PlanejamentoMatriculaSequencialAgrupadoViewModel>> Buscar(
            [FromServices] IObterDataTablePlanejamentoMatriculaSequencialQueryHandler handler,
            [FromQuery] PlanejamentoMatriculaSequencialFilter filtro)
        {
            var result = handler.Execute(filtro);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CommandResult> Alterar(int id,
            [FromServices] IIncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler handler,
            [FromBody] PlanejamentoMatriculaSequencialAgrupadoViewModel vm)
        {
            if (id != vm.Id)
            {
                var commandResult = new CommandResult(StatusCodes.Status400BadRequest);
                commandResult.SetError(ValidationMessageResource.IdInvalido);
                return StatusCode(commandResult.StatusCode, commandResult);
            }

            var result = handler.Execute(vm);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CommandResult> Excluir(int id,
            [FromServices] IExcluirPlanejamentoMatriculaSequencialCommandHandler handler)
        {
            var result = handler.Execute(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
