using System;
using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Extensions;

namespace VideoClub.Common.BusinessLogic.Dto
{
    public class RentalDto
    {
        public string Id { get; set; }
        public string ProductTitle { get; set; }
        public string ProductId { get; set; }
        //public ProductDto Product { get; set; }
        public string ClientAccreditation { get; set; }
        public string ClientId { get; set; }
        //public ClientDto Client { get; set; }
        public DateTime StartRental { get; set; }
        public DateTime FinishRental { get; set; }
        public StateRentalEnum State { get; set; }
        public string StateDescription => State.GetDescription();
        public decimal Price { get; set; }
    }
}
