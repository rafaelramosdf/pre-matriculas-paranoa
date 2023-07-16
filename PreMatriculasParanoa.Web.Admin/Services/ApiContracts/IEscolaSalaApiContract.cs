using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Filters;
using PreMatriculasParanoa.Web.Admin.Services.ApiContracts.Base;

namespace PreMatriculasParanoa.Web.Admin.Services.ApiContracts
{
    public interface IEscolaSalaApiContract : ICrudApiContract<Escola, EscolaFilter, EscolaViewModel>
    {
    }
}
