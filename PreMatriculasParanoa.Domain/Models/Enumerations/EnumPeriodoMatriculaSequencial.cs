using System.ComponentModel;

namespace PreMatriculasParanoa.Domain.Models.Enumerations
{
    [DefaultValue(SegundoPeriodoPreEscolaParaPrimeiroAnoFundamental)]
    public enum EnumPeriodoMatriculaSequencial
    {
        [Description("2º Período (pré-escola) p/ 1º Ano (fundamental)")]
        SegundoPeriodoPreEscolaParaPrimeiroAnoFundamental = 0,

        [Description("5° Ano (fundamental) p/ 6° Ano (fundamental)")]
        QuintoAnoFundamentalParaSextoAnoFundamental = 1,

        [Description("9° Ano (fundamental) p/ 1ª Série (médio))")]
        NonoAnoFundamentalParaPrimeiraSerieMedio = 2,
    }
}
