using System.ComponentModel.DataAnnotations;
using VideoClub.Common.Model.Enums;

namespace VideoClub.Infrastructure.Repository.Entity
{
    public class VideoGame : Product
    {
        [Required]
        public GamePlatformEnum Platform { get; set; }

    }
}
