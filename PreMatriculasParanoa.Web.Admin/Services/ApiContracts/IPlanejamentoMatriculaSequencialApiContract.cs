using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts.Base;
using Refit;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Services.ApiContracts
{
    public interface IPlanejamentoMatriculaSequencialApiContract : ICrudApiContract<PlanejamentoMatriculaSequencialAgrupado, PlanejamentoMatriculaSequencialFilter, PlanejamentoMatriculaSequencialAgrupadoViewModel>
    {
        [Get("")]
        Task<PlanejamentoMatriculaSequencialAgrupadoViewModel> BuscarSequencialAgrupado([Query] PlanejamentoMatriculaSequencialFilter filtro);

        [Get("/escolas/{idEscolaDestino}/anos-letivos/{ano}")]
        Task<int> BuscarTotalPorEscolaEAnoLetivo(int idEscolaDestino, int ano);
    }
}
