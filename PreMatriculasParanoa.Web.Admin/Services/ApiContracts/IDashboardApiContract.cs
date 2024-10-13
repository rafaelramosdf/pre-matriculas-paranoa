using PreMatriculasParanoa.Domain.Models.ViewModels;
using Refit;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Services.ApiContracts
{
    public interface IDashboardApiContract
    {
        [Get("/totalizadores-vagas")]
        Task<DashboardTotalizadoresViewModel> TotalizadoresVagas([Query] int anoLetivo);
    }
}
