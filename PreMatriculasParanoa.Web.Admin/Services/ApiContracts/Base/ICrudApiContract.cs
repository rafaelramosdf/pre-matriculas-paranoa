using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters.Base;
using Refit;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Web.Admin.Services.ApiContracts.Base
{
    public interface ICrudApiContract<TEntity, TFilter, TViewModel>
        where TEntity : Entity
        where TFilter : Filter
        where TViewModel : ViewModel<TEntity>
    {
        [Post("")]
        Task<ApiResponse<CommandResult>> Incluir([Body] TViewModel model);

        [Put("/{id}")]
        Task<ApiResponse<CommandResult>> Alterar(int id, [Body] TViewModel model);

        [Delete("/{id}")]
        Task<ApiResponse<CommandResult>> Excluir(int id);

        [Get("/{id}")]
        Task<TViewModel> Buscar(int id);

        [Get("")]
        Task<DataTableModel<TViewModel>> Buscar([Query] TFilter filtro);
    }
}
