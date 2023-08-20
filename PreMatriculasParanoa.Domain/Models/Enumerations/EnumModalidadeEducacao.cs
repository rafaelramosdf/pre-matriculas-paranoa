using System.ComponentModel;

namespace PreMatriculasParanoa.Domain.Models.Enumerations
{
    [DefaultValue(CrechePreEscola)]
    public enum EnumModalidadeEducacao
    {
        /// <summary>
        /// Creche / Pré-Escola (até o 2° período pré-escola)
        /// </summary>
        [Description("Creche / Pré-Escola (até o 2° período pré-escola)")]
        CrechePreEscola = 0,

        /// <summary>
        /// Pré-Escola + Fundamental I (do 1° período (pré-escola) ao 5° ano fundamental)
        /// </summary>
        [Description("Pré-Escola + Fundamental I (do 1° período (pré-escola) ao 5° ano fundamental)")]
        PreEscolaMaisFundamentalI = 1,

        /// <summary>
        /// Ensino Fundamento I (do 1° ao 5° ano)
        /// </summary>
        [Description("Ensino Fundamento I (do 1° ao 5° ano)")]
        EnsinoFundamentalI = 2,

        /// <summary>
        /// Ensino Fundamento II (do 6° ao 9° ano)
        /// </summary>
        [Description("Ensino Fundamento II (do 6° ao 9° ano)")]
        EnsinoFundamentalII = 3,

        /// <summary>
        /// Ensino Fundamento Completo (do 1° ao 9° ano)
        /// </summary>
        [Description("Ensino Fundamento Completo (do 1° ao 9° ano)")]
        EnsinoFundamentalCompleto = 4,

        /// <summary>
        /// Ensino Médio (da 1ª a 3ª série)
        /// </summary>
        [Description("Ensino Médio (da 1ª a 3ª série)")]
        EnsinoMedio = 5,

        /// <summary>
        /// Centro Educacional (do 6° ano fundamental a 3ª série do ensino médio)
        /// </summary>
        [Description("Centro Educacional (do 6° ano fundamental a 3ª série do ensino médio)")]
        FundamentalEMedio = 6,
    }
}
