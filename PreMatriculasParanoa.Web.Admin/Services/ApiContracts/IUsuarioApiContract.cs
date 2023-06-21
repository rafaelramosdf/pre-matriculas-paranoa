using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts.Base;
using PreMatriculasParanoa.Domain.Queries.Filters.Base;

namespace PreMatriculasParanoa.Web.Admin.Services.ApiContracts
{
    public interface IUsuarioApiContract : ICrudApiContract<Usuario, Filter, UsuarioViewModel>
    {
    }
}
