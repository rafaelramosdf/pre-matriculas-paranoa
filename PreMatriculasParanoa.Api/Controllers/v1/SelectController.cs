using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreMatriculasParanoa.Domain.Handlers.Select;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;

namespace PreMatriculasParanoa.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/selects")]
    [Produces("application/json")]
    public class SelectController : ControllerBase
    {
        [HttpGet("escolas")]
        public ActionResult<SelectResult> Escolas(
            [FromServices] IEscolaSelectQueryHandler handler,
            [FromQuery] SelectSearchParam param)
        {
            var result = handler.Execute(param.Term, param.Size, param.Selected);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("escolas/{id}")]
        public ActionResult<EscolaViewModel> EscolaPorId(
            [FromServices] IEscolaPorIdSelectQueryHandler handler,
            int id)
        {
            var result = handler.Execute(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("escolas/{id_escola}/salas")]
        public ActionResult<SelectResult> Salas(
            int id_escola,
            [FromServices] ISalaSelectQueryHandler handler,
            [FromQuery] SelectSearchParam param)
        {
            var filter = new SalaFilter 
            {
                Search = param.Term,
                Limit = param.Size,
                IdSala = param.Selected,
                IdEscola = id_escola
            };
            var result = handler.Execute(filter);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("salas/{id}")]
        public ActionResult<SalaViewModel> SalaPorId(
            [FromServices] ISalaPorIdSelectQueryHandler handler,
            int id)
        {
            var result = handler.Execute(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
