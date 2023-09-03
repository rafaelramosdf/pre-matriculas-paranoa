using PreMatriculasParanoa.Domain.Models.Entities;
using PreMatriculasParanoa.Domain.Models.Enumerations;
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

        public Expression<Func<PlanejamentoMatriculaSequencial, bool>> ObterFiltroAnoLetivo(int anoLetivo)
        {
            if (anoLetivo <= 0)
                return x => x.AnoLetivo == DateTime.Now.AddYears(1).Year;

            return x => x.AnoLetivo == anoLetivo;
        }

        public Expression<Func<PlanejamentoMatriculaSequencial, bool>> ObterFiltroMatriculaSequencial(PlanejamentoMatriculaSequencialFilter filtro)
        {
            switch (filtro.PeriodoMatriculaSequencial)
            {
                case EnumPeriodoMatriculaSequencial.SegundoPeriodoPreEscolaParaPrimeiroAnoFundamental:
                    return x => (x.EscolaOrigem.ModalidadeEnsino == EnumModalidadeEducacao.CrechePreEscola)
                    && (x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalI
                    || x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.CrecheMaisPreEscolaMaisFundamentalI 
                    || x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.PreEscolaMaisFundamentalI
                    || x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalCompleto);

                case EnumPeriodoMatriculaSequencial.QuintoAnoFundamentalParaSextoAnoFundamental:
                    return x => (x.EscolaOrigem.ModalidadeEnsino == EnumModalidadeEducacao.CrecheMaisPreEscolaMaisFundamentalI
                    || x.EscolaOrigem.ModalidadeEnsino == EnumModalidadeEducacao.PreEscolaMaisFundamentalI
                    || x.EscolaOrigem.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalI)
                    && (x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalII
                    || x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalCompleto
                    || x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.FundamentalEMedio);

                case EnumPeriodoMatriculaSequencial.NonoAnoFundamentalParaPrimeiraSerieMedio:
                    return x => (x.EscolaOrigem.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalII
                    || x.EscolaOrigem.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalCompleto)
                    && (x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoMedio
                    || x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.FundamentalEMedio);

                default:
                    return x => (x.EscolaOrigem.ModalidadeEnsino == EnumModalidadeEducacao.CrechePreEscola)
                    && (x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalI
                    || x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.PreEscolaMaisFundamentalI
                    || x.EscolaDestino.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalCompleto);
            }
        }

        public Expression<Func<Escola, bool>> ObterFiltroEscolaSequencial(EnumPeriodoMatriculaSequencial filtro, bool escolaOrigem)
        {
            switch (filtro)
            {
                case EnumPeriodoMatriculaSequencial.SegundoPeriodoPreEscolaParaPrimeiroAnoFundamental:
                    return x => escolaOrigem 
                    ? (x.ModalidadeEnsino == EnumModalidadeEducacao.CrechePreEscola)
                    : (x.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalI
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.CrecheMaisPreEscolaMaisFundamentalI
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.PreEscolaMaisFundamentalI
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalCompleto);

                case EnumPeriodoMatriculaSequencial.QuintoAnoFundamentalParaSextoAnoFundamental:
                    return x => escolaOrigem
                    ? (x.ModalidadeEnsino == EnumModalidadeEducacao.PreEscolaMaisFundamentalI
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.CrecheMaisPreEscolaMaisFundamentalI
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalI)
                    : (x.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalII
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalCompleto
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.FundamentalEMedio);

                case EnumPeriodoMatriculaSequencial.NonoAnoFundamentalParaPrimeiraSerieMedio:
                    return x => escolaOrigem
                    ? (x.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalII
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalCompleto)
                    : (x.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoMedio
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.FundamentalEMedio);

                default:
                    return x => escolaOrigem
                    ? (x.ModalidadeEnsino == EnumModalidadeEducacao.CrechePreEscola)
                    : (x.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalI
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.PreEscolaMaisFundamentalI
                    || x.ModalidadeEnsino == EnumModalidadeEducacao.EnsinoFundamentalCompleto);
            }
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
