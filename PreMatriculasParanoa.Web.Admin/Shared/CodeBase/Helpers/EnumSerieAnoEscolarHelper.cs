
using PreMatriculasParanoa.Domain.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumSerieAnoEscolarHelper
{
    public static List<EnumSerieAnoEscolar> ObterSeriesAnosPorModalidadeEducacao(EnumModalidadeEducacao modalidadeEducacao) 
    {
        List<EnumSerieAnoEscolar> seriesAnos = (Enum.GetValues<EnumSerieAnoEscolar>()).ToList();

        switch (modalidadeEducacao)
        {
            case EnumModalidadeEducacao.CrechePreEscola:
                seriesAnos = seriesAnos.Where(m => m.GetHashCode() <= EnumSerieAnoEscolar.SegundoPeriodo.GetHashCode()).ToList();
                break;
            case EnumModalidadeEducacao.PreEscolaMaisFundamentalI:
                seriesAnos = seriesAnos.Where(m => m.GetHashCode() >= EnumSerieAnoEscolar.PrimeiroPeriodo.GetHashCode()
                && m.GetHashCode() <= EnumSerieAnoEscolar.QuintoAnoFundamental.GetHashCode()).ToList();
                break;
            case EnumModalidadeEducacao.EnsinoFundamentalI:
                seriesAnos = seriesAnos.Where(m => m.GetHashCode() >= EnumSerieAnoEscolar.PrimeiroAnoFundamental.GetHashCode()
                && m.GetHashCode() <= EnumSerieAnoEscolar.QuintoAnoFundamental.GetHashCode()).ToList();
                break;
            case EnumModalidadeEducacao.EnsinoFundamentalII:
                seriesAnos = seriesAnos.Where(m => m.GetHashCode() >= EnumSerieAnoEscolar.SextoAnoFundamental.GetHashCode()
                && m.GetHashCode() <= EnumSerieAnoEscolar.NonoAnoFundamental.GetHashCode()).ToList();
                break;
            case EnumModalidadeEducacao.EnsinoFundamentalCompleto:
                seriesAnos = seriesAnos.Where(m => m.GetHashCode() >= EnumSerieAnoEscolar.PrimeiroAnoFundamental.GetHashCode()
                && m.GetHashCode() <= EnumSerieAnoEscolar.NonoAnoFundamental.GetHashCode()).ToList();
                break;
            case EnumModalidadeEducacao.FundamentalEMedio:
                seriesAnos = seriesAnos.Where(m => m.GetHashCode() >= EnumSerieAnoEscolar.PrimeiroAnoFundamental.GetHashCode()
                && m.GetHashCode() <= EnumSerieAnoEscolar.TerceiraSerieMedio.GetHashCode()).ToList();
                break;
            case EnumModalidadeEducacao.EnsinoMedio:
                seriesAnos = seriesAnos.Where(m => m.GetHashCode() >= EnumSerieAnoEscolar.PrimeiraSerieMedio.GetHashCode()
                && m.GetHashCode() <= EnumSerieAnoEscolar.TerceiraSerieMedio.GetHashCode()).ToList();
                break;
            default:
                break;
        }

        return seriesAnos;
    }

    public static EnumSerieAnoEscolar ObterProximaSerieAnoPorModalidadeEducacao(EnumModalidadeEducacao modalidadeEducacao, EnumSerieAnoEscolar serieAnoAtual)
    {
        switch (modalidadeEducacao)
        {
            case EnumModalidadeEducacao.CrechePreEscola:
                return (serieAnoAtual != EnumSerieAnoEscolar.SegundoPeriodo)
                    ? ((EnumSerieAnoEscolar)(serieAnoAtual.GetHashCode() + 1)) : serieAnoAtual;
            
            case EnumModalidadeEducacao.PreEscolaMaisFundamentalI:
                return (serieAnoAtual != EnumSerieAnoEscolar.QuintoAnoFundamental)
                    ? ((EnumSerieAnoEscolar)(serieAnoAtual.GetHashCode() + 1)) : serieAnoAtual;

            case EnumModalidadeEducacao.EnsinoFundamentalI:
                return (serieAnoAtual != EnumSerieAnoEscolar.QuintoAnoFundamental)
                    ? ((EnumSerieAnoEscolar)(serieAnoAtual.GetHashCode() + 1)) : serieAnoAtual;

            case EnumModalidadeEducacao.EnsinoFundamentalII:
                return (serieAnoAtual != EnumSerieAnoEscolar.NonoAnoFundamental)
                    ? ((EnumSerieAnoEscolar)(serieAnoAtual.GetHashCode() + 1)) : serieAnoAtual;

            case EnumModalidadeEducacao.EnsinoFundamentalCompleto:
                return (serieAnoAtual != EnumSerieAnoEscolar.NonoAnoFundamental)
                    ? ((EnumSerieAnoEscolar)(serieAnoAtual.GetHashCode() + 1)) : serieAnoAtual;

            case EnumModalidadeEducacao.FundamentalEMedio:
                return (serieAnoAtual != EnumSerieAnoEscolar.TerceiraSerieMedio)
                    ? ((EnumSerieAnoEscolar)(serieAnoAtual.GetHashCode() + 1)) : serieAnoAtual;

            case EnumModalidadeEducacao.EnsinoMedio:
                return (serieAnoAtual != EnumSerieAnoEscolar.TerceiraSerieMedio)
                    ? ((EnumSerieAnoEscolar)(serieAnoAtual.GetHashCode() + 1)) : serieAnoAtual;

            default:
                return serieAnoAtual;
        }
    }
}