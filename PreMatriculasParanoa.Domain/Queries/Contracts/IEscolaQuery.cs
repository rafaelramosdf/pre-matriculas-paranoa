using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Queries.Base;
using PreMatriculasParanoa.Domain.Queries.Filters;

namespace PreMatriculasParanoa.Domain.Queries.Contracts
{
    public interface IEscolaQuery : IBaseQuery<Escola, EscolaFilter>
    {
    }
}
