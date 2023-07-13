using PreMatriculasParanoa.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters;

namespace PreMatriculasParanoa.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/planejamentos-anos-letivos")]
    [Produces("application/json")]
    public class PlanejamentoAnoLetivoController : ControllerBase
    {
        [HttpPost]
        public ActionResult<CommandResult> Incluir(
            [FromBody] PlanejamentoAnoLetivoViewModel vm)
        {
            return Ok();
            // TODO: A implementar...
            //var result = handler.Execute(vm);
            //return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PlanejamentoAnoLetivoViewModel> Buscar(int id)
        {
            return Ok(new PlanejamentoAnoLetivoViewModel());
            // TODO: A implementar...
            //var result = handler.Execute(id);

            //if (result == null)
            //    return NotFound(GeneralMessageResource.NenhumRecursoEncontradoParaID);

            //return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        public ActionResult<DataTableModel<PlanejamentoAnoLetivoViewModel>> Buscar(
            [FromQuery] PlanejamentoAnoLetivoFilter filtro)
        {
            return Ok(new DataTableModel<PlanejamentoAnoLetivoViewModel>());
            // TODO: A implementar...
            //var result = handler.Execute(filtro);
            //return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CommandResult> Alterar(int id, 
            [FromBody] PlanejamentoAnoLetivoViewModel vm)
        {
            return Ok();
            // TODO: A implementar...
            //if (id != vm.Id)
            //{
            //    var commandResult = new CommandResult(StatusCodes.Status400BadRequest);
            //    commandResult.SetError(ValidationMessageResource.IdInvalido);
            //    return StatusCode(commandResult.StatusCode, commandResult);
            //}

            //var result = handler.Execute(vm);
            //return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CommandResult> Excluir(int id)
        {
            return Ok();
            // TODO: A implementar...
            //var result = handler.Execute(id);
            //return StatusCode(result.StatusCode, result);
        }
    }
}
