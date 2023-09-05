using System.ComponentModel;

namespace PreMatriculasParanoa.Domain.Models.Enumerations
{
    [DefaultValue(CC)]
    public enum EnumTipoAtendimento
    {
        [Description("CC - Classe Comum")]
        CC = 0,
        [Description("CCI - Classe Comum Inclusiva")]
        CCI = 1,
        [Description("CII - Classe de Integração Inversa")]
        CII = 2,
        [Description("CBM - Classe Bilingue Mediada")]
        CBM = 3,
        [Description("CE - Classe Especial")]
        CE = 4,
        [Description("SR - Sala de Recursos")]
        SR = 5,
        [Description("ECM - Educação Com Movimento")]
        CM = 6,
        [Description("CID")]
        CID = 7,
    }
}
