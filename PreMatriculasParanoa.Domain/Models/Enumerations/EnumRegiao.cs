using System.ComponentModel;

namespace PreMatriculasParanoa.Domain.Models.Enumerations
{
    [DefaultValue(RegiaoI)]
    public enum EnumRegiao
    {
        [Description("Região I (zona urbana)")]
        RegiaoI = 1,
        [Description("Região II (zona rural)")]
        RegiaoII = 2,
    }
}
