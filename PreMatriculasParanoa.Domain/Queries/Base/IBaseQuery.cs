using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters.Base;
using System;
using System.Linq.Expressions;

namespace PreMatriculasParanoa.Domain.Queries.Base
{
    public interface IBaseQuery<TEntity, TFilter>
        where TEntity : Entity
        where TFilter : Filter
    {
        Expression<Func<TEntity, bool>> ObterPesquisa(TFilter filtro);
        Expression<Func<TEntity, object>> ObterOrdenacao(string campo);
    }
}
