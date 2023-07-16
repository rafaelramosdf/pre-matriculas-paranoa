using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts;
using PreMatriculasParanoa.Web.Admin.Shared.CodeBase.Pages;

namespace PreMatriculasParanoa.Web.Admin.Pages.Configuracoes.EscolasSalas
{
    public partial class EscolasSalasList : ListPageBase<Escola, EscolaFilter, EscolaViewModel, IEscolaSalaApiContract>
    {
        protected override void OnInit()
        {
            State.TituloPagina = "Escolas e Salas / Lista";
        }
    }
}
