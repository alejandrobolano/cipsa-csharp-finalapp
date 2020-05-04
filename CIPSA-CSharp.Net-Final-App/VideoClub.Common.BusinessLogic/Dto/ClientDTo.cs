using System;
using System.Collections.Generic;
using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Extensions;
using VideoClub.Common.Model.Utils;

namespace VideoClub.Common.BusinessLogic.Dto
{
    public class ClientDto
    {
        #region Public properties
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public AccreditationEnum AccreditationType { get; set; }
        public string AccreditationTypeDescription => AccreditationType.GetDescription();
        public string Accreditation { get; set; }
        public string PhoneContact { get; set; }
        public string PhoneAux { get; set; }
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public StateClientEnum State { get; set; }
        public string StateDescription => State.GetDescription();
        public int RentalQuantity { get; set; }
        public bool IsVip { get; set; }
        public int Discount { get; set; }

        #endregion


    }
}
