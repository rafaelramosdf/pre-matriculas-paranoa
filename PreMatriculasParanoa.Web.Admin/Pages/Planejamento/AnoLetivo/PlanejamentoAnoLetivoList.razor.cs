using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages;

namespace PreMatriculasParanoa.Web.Admin.Pages.Planejamento.AnoLetivo
{
    public partial class PlanejamentoAnoLetivoList : ListPageBase<PlanejamentoAnoLetivo, PlanejamentoAnoLetivoFilter, PlanejamentoAnoLetivoViewModel, IPlanejamentoAnoLetivoApiContract>
    {
        protected override void OnInit()
        {
            State.TituloPagina = "Planejamento Ano Letivo / Lista";
        }
    }
}
