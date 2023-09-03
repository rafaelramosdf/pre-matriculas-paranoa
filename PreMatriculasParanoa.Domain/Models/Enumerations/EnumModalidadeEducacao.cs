using System.ComponentModel;

namespace PreMatriculasParanoa.Domain.Models.Enumerations
{
    [DefaultValue(CrechePreEscola)]
    public enum EnumModalidadeEducacao
    {
        /// <summary>
        /// Creche e Pré-Escola (da creche até o 2° período pré-escola)
        /// </summary>
        [Description("Creche e Pré-Escola (da creche até o 2° período pré-escola)")]
        CrechePreEscola = 0,

        /// <summary>
        /// Creche + Pré-Escola + Fundamental I (da creche ao 5° ano fundamental)
        /// </summary>
        [Description("Creche + Pré-Escola + Fundamental I (da creche ao 5° ano fundamental)")]
        CrecheMaisPreEscolaMaisFundamentalI = 1,

        /// <summary>
        /// Pré-Escola + Fundamental I (do 1° período (pré-escola) ao 5° ano fundamental)
        /// </summary>
        [Description("Pré-Escola + Fundamental I (do 1° período (pré-escola) ao 5° ano fundamental)")]
        PreEscolaMaisFundamentalI = 2,

        /// <summary>
        /// Ensino Fundamental I (do 1° ao 5° ano)
        /// </summary>
        [Description("Ensino Fundamental I (do 1° ao 5° ano)")]
        EnsinoFundamentalI = 3,

        /// <summary>
        /// Ensino Fundamental II (do 6° ao 9° ano)
        /// </summary>
        [Description("Ensino Fundamental II (do 6° ao 9° ano)")]
        EnsinoFundamentalII = 4,

        /// <summary>
        /// Ensino Fundamental Completo (do 1° ao 9° ano)
        /// </summary>
        [Description("Ensino Fundamental Completo (do 1° ao 9° ano)")]
        EnsinoFundamentalCompleto = 5,

        /// <summary>
        /// Ensino Médio (da 1ª a 3ª série)
        /// </summary>
        [Description("Ensino Médio (da 1ª a 3ª série)")]
        EnsinoMedio = 6,

        /// <summary>
        /// Centro Educacional (do 6° ano fundamental a 3ª série do ensino médio)
        /// </summary>
        [Description("Centro Educacional (do 6° ano fundamental a 3ª série do ensino médio)")]
        FundamentalEMedio = 7,

        /// <summary>
        /// EJA Fase I (do 1° ao 5° ano fundamental)
        /// </summary>
        [Description("EJA Fase I (do 1° ao 5° ano fundamental)")]
        EjaFaseI = 8,

        /// <summary>
        /// EJA Fase II (do 6° ao 9° ano fundamental)
        /// </summary>
        [Description("EJA Fase II (do 6° ao 9° ano fundamental)")]
        EjaFaseII = 9,

        /// <summary>
        /// EJA Médio (da 1ª a 3ª série do ensino médio)
        /// </summary>
        [Description("EJA Médio (da 1ª a 3ª série do ensino médio)")]
        EjaMedio = 10,
    }
}
