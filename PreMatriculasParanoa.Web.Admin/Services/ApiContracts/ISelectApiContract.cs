using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Services.ApiContracts
{
    public interface ISelectApiContract
    {
        [Get("/escolas")]
        Task<IEnumerable<SelectResult>> Escolas([Query] SelectSearchParam param);

        [Get("/escolas/{id}")]
        Task<EscolaViewModel> EscolaPorId(int id);

        [Get("/escolas/{id_escola}/salas")]
        Task<IEnumerable<SelectResult>> Salas([Query] SelectSearchParam param, int id_escola);

        [Get("/salas/{id}")]
        Task<SalaViewModel> SalaPorId(int id);
    }
}
