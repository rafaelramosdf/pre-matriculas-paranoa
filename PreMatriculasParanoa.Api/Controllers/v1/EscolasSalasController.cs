using PreMatriculasParanoa.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Domain.Handlers.EscolaSala;
using Microsoft.AspNetCore.Http;
using PreMatriculasParanoa.Domain.Resources;

namespace PreMatriculasParanoa.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/escolas-salas")]
    [Produces("application/json")]
    public class EscolasSalasController : ControllerBase
    {
        [HttpPost]
        public ActionResult<CommandResult> Incluir(
            [FromServices] IIncluirOuAtualizarEscolaSalaCommandHandler handler, 
            [FromBody] EscolaViewModel vm)
        {
            var result = handler.Execute(vm);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<EscolaViewModel> Buscar(int id,
            [FromServices] IBuscarEscolaSalaPorIdQueryHandler handler)
        {
            var result = handler.Execute(id);

            if (result == null)
                return NotFound(GeneralMessageResource.NenhumRecursoEncontradoParaID);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        public ActionResult<DataTableModel<EscolaViewModel>> Buscar(
            [FromServices] IObterDataTableEscolaSalaQueryHandler handler, 
            [FromQuery] EscolaFilter filtro)
        {
            var result = handler.Execute(filtro);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CommandResult> Alterar(int id,
            [FromServices] IIncluirOuAtualizarEscolaSalaCommandHandler handler, 
            [FromBody] EscolaViewModel vm)
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
            [FromServices] IExcluirEscolaSalaCommandHandler handler)
        {
            var result = handler.Execute(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
