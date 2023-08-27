using System.ComponentModel;

namespace PreMatriculasParanoa.Domain.Models.Enumerations
{
    [DefaultValue(Indefinido)]
    public enum EnumRegiao
    {
        /// <summary>
        /// Indefinido
        /// </summary>
        [Description("Indefinido")]
        Indefinido = 0,

        /// <summary>
        /// Região I (zona urbana)
        /// </summary>
        [Description("Região I (zona urbana)")]
        RegiaoI = 1,

        /// <summary>
        /// Região II (zona rural)
        /// </summary>
        [Description("Região II (zona rural)")]
        RegiaoII = 2,
    }
}
