using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages;

namespace PreMatriculasParanoa.Web.Admin.Pages.Planejamento.MatriculaSequencial
{
    public partial class PlanejamentoMatriculaSequencialList : ListPageBase<PlanejamentoMatriculaSequencialAgrupado, PlanejamentoMatriculaSequencialFilter, PlanejamentoMatriculaSequencialAgrupadoViewModel, IPlanejamentoMatriculaSequencialApiContract>
    {
        protected override void OnInit()
        {
            State.TituloPagina = "Planejamento Matrícula Sequencial / Lista";
        }
    }
}
