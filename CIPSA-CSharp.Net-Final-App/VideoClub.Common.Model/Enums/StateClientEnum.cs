using System.ComponentModel;

namespace VideoClub.Common.Model.Enums
{
    public enum StateClientEnum
    {
        [Description("Activo")]
        Activated,
        [Description("Bloqueado")]
        Blocked,
        [Description("Baja")]
        Leave
    }
}
