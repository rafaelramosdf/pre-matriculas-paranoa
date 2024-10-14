using PreMatriculasParanoa.Domain.Models.ViewModels;
using Refit;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Services.ApiContracts
{
    public interface IDashboardApiContract
    {
        [Get("/totalizadores-vagas")]
        Task<DashboardTotalizadoresViewModel> TotalizadoresVagas([Query] int anoLetivo);

        [Get("/progresso-preenchimento-escolas")]
        Task<DashboardProgressoPreenchimentoViewModel> ProgressoPreenchimentoEscolas([Query] int anoLetivo);
    }
}
