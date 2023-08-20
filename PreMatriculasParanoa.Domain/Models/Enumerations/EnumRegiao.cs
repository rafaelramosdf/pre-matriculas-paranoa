using System.ComponentModel;

namespace PreMatriculasParanoa.Domain.Models.Enumerations
{
    [DefaultValue(Indefinido)]
    public enum EnumRegiao
    {
        [Description("Indefinido")]
        Indefinido = 0,
        [Description("Região I (zona urbana)")]
        RegiaoI = 1,
        [Description("Região II (zona rural)")]
        RegiaoII = 2,
    }
}
