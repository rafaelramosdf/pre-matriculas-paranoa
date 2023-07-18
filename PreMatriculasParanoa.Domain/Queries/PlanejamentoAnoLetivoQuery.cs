using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Queries.Contracts;
using PreMatriculasParanoa.Domain.Queries.Filters;
using System;
using System.Linq.Expressions;

namespace PreMatriculasParanoa.Domain.Queries
{
    public class PlanejamentoAnoLetivoQuery : IPlanejamentoAnoLetivoQuery
    {
        public Expression<Func<PlanejamentoAnoLetivo, bool>> ObterPesquisa(PlanejamentoAnoLetivoFilter filtro)
        {
            if (string.IsNullOrEmpty(filtro?.Search))
                return _ => true;

            return x => x.AnoLetivo.ToString() == filtro.Search;
        }

        public Expression<Func<PlanejamentoAnoLetivo, object>> ObterOrdenacao(string campo)
        {
            switch (campo)
            {
                default:
                    return x => x.AnoLetivo;
            }
        }
    }
}
