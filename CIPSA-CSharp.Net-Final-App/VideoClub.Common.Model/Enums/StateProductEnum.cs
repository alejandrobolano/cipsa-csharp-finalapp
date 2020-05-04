using System.ComponentModel;

namespace VideoClub.Common.Model.Enums
{
    public enum StateProductEnum
    {
        [Description("Disponible")]
        Available,
        [Description("No disponible")]
        NonAvailable,
        [Description("Perdido")]
        Lost,
        [Description("Mal Estado")]
        BadState
    }
}
