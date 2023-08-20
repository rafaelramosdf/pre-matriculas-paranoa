using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.Enumerations;
using PreMatriculasParanoa.Domain.Queries.Contracts;
using PreMatriculasParanoa.Domain.Queries.Filters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PreMatriculasParanoa.Domain.Queries
{
    public class EscolaQuery : IEscolaQuery
    {
        public Expression<Func<Escola, bool>> ObterPesquisa(EscolaFilter filtro)
        {
            if (string.IsNullOrEmpty(filtro?.Search))
                return _ => true;

            return x => x.Nome.Contains(filtro.Search);
        }

        public Expression<Func<Escola, object>> ObterOrdenacao(string campo)
        {
            switch (campo)
            {
                default:
                    return x => x.Nome;
            }
        }
    }
}
