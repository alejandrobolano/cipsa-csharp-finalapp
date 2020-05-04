using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Extensions;

namespace VideoClub.Common.BusinessLogic.Dto
{
    public class VideoGameDto : ProductDto
    {
        public GamePlatformEnum Platform { get; set; }

        public string PlatformDescription => Platform.GetDescription();
    }
}
