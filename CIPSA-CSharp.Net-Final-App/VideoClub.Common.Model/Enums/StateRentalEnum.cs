using System.ComponentModel;

namespace VideoClub.Common.Model.Enums
{
    public enum StateRentalEnum
    {
        [Description("Activa")]
        Activated,
        [Description("Devuelta")]
        Returned,
        All
    }
}
