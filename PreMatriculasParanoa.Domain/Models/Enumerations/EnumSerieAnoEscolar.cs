using System.ComponentModel;

namespace PreMatriculasParanoa.Domain.Models.Enumerations
{
    /// <summary>
    /// - Creche e Pré Escola: 0 ao 2, 
    /// - Ensino Fundamental: 11 ao 19, 
    /// - Ensino Médio: 21 ao 23.
    /// </summary>
    [DefaultValue(Creche)]
    public enum EnumSerieAnoEscolar
    {
        // Creche e Pré Escola
        [Description("Creche")]
        Creche = 0,
        [Description("1° período")]
        PrimeiroPeriodo = 1,
        [Description("2° período")]
        SegundoPeriodo = 2,

        // Ensino Fundamental
        [Description("1° ano")]
        PrimeiroAnoFundamental = 11,
        [Description("2° ano")]
        SegundoAnoFundamental = 12,
        [Description("3° ano")]
        TerceiroAnoFundamental = 13,
        [Description("4° ano")]
        QuartoAnoFundamental = 14,
        [Description("5° ano")]
        QuintoAnoFundamental = 15,
        [Description("6° ano")]
        SextoAnoFundamental = 16,
        [Description("7° ano")]
        SetimoAnoFundamental = 17,
        [Description("8° ano")]
        OitavoAnoFundamental = 18,
        [Description("9° ano")]
        NonoAnoFundamental = 19,

        // Ensino Médio
        [Description("1ª série")]
        PrimeiraSerieMedio = 21,
        [Description("2ª série")]
        SegundaSerieMedio = 22,
        [Description("3ª série")]
        TerceiraSerieMedio = 23,
    }
}
