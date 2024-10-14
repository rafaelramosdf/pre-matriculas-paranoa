using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreMatriculasParanoa.Domain.Handlers.Dashboard;
using PreMatriculasParanoa.Domain.Models.ViewModels;

namespace PreMatriculasParanoa.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/dashboard")]
    [Produces("application/json")]
    public class DashboardController : ControllerBase
    {
        [HttpGet("totalizadores-vagas")]
        public ActionResult<DashboardTotalizadoresViewModel> TotalizadoresVagas(
            [FromServices] IBuscarTotalizadoresQueryHandler handler,
            [FromQuery] int anoLetivo)
        {
            var result = handler.Execute(anoLetivo);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("progresso-preenchimento-escolas")]
        public ActionResult<DashboardProgressoPreenchimentoViewModel> ProgressoPreenchimentoEscolas(
            [FromServices] IBuscarProgressoPreenchimentoEscolasQueryHandler handler,
            [FromQuery] int anoLetivo)
        {
            var result = handler.Execute(anoLetivo);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
