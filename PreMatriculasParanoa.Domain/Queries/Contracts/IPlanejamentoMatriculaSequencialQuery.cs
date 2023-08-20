using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Queries.Base;
using PreMatriculasParanoa.Domain.Queries.Filters;
using System.Linq.Expressions;
using System;
using PreMatriculasParanoa.Domain.Models.Enumerations;

namespace PreMatriculasParanoa.Domain.Queries.Contracts
{
    public interface IPlanejamentoMatriculaSequencialQuery : IBaseQuery<PlanejamentoMatriculaSequencial, PlanejamentoMatriculaSequencialFilter>
    {
        Expression<Func<PlanejamentoMatriculaSequencial, bool>> ObterFiltroAnoLetivo(int anoLetivo);
        Expression<Func<PlanejamentoMatriculaSequencial, bool>> ObterFiltroMatriculaSequencial(PlanejamentoMatriculaSequencialFilter filtro);
        Expression<Func<Escola, bool>> ObterFiltroEscolaSequencial(EnumPeriodoMatriculaSequencial filtro, bool escolaOrigem);
    }
}
