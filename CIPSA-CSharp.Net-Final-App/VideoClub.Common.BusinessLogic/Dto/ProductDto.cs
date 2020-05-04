using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Extensions;

namespace VideoClub.Common.BusinessLogic.Dto
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int QuantityDisc { get; set; }
        public StateProductEnum State { get; set; }
        public decimal Price { get; set; }
        public string StateDescription => State.GetDescription();

    }
}
