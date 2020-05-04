using System;

namespace VideoClub.Common.BusinessLogic.Dto
{
    public class MovieDto : ProductDto
    {
        
        #region Public properties
        public TimeSpan Duration { get; set; }
        public int ProductionYear { get; set; }
        public int BuyYear { get; set; }
        #endregion
        
    }
}
