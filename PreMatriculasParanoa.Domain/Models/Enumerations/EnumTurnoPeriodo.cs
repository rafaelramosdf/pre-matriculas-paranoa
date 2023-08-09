using System.ComponentModel;

namespace PreMatriculasParanoa.Domain.Models.Enumerations
{
    [DefaultValue(Matutino)]
    public enum EnumTurnoPeriodo
    {
        [Description("Matutino")]
        Matutino = 0,
        [Description("Vespertino")]
        Vespertino = 1,
        [Description("Integral")]
        Integral = 2,
    }
}
