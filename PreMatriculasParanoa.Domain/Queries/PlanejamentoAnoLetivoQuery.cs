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
                return x => x.AnoLetivo == filtro.Year;

            return x => x.AnoLetivo == filtro.Year
                && x.Escola.Nome.Contains(filtro.Search);
        }

        public Expression<Func<PlanejamentoAnoLetivo, object>> ObterOrdenacao(string campo)
        {
            switch (campo)
            {
                default:
                    return x => x.Escola.Nome;
            }
        }
    }
}
