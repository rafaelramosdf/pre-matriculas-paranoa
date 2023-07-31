using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Queries.Contracts;
using PreMatriculasParanoa.Domain.Queries.Filters;
using System;
using System.Linq.Expressions;

namespace PreMatriculasParanoa.Domain.Queries
{
    public class PlanejamentoMatriculaSequencialQuery : IPlanejamentoMatriculaSequencialQuery
    {
        public Expression<Func<PlanejamentoMatriculaSequencial, bool>> ObterPesquisa(PlanejamentoMatriculaSequencialFilter filtro)
        {
            if (string.IsNullOrEmpty(filtro?.Search))
                return _ => true;

            return x => x.EscolaOrigem.Nome.ToString() == filtro.Search;
        }

        public Expression<Func<PlanejamentoMatriculaSequencial, object>> ObterOrdenacao(string campo)
        {
            switch (campo)
            {
                default:
                    return x => x.EscolaOrigem.Nome;
            }
        }
    }
}
