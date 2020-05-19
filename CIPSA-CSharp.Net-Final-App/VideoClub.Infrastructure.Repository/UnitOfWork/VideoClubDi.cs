using System;

namespace VideoClub.Infrastructure.Repository.UnitOfWork
{
    public class VideoClubDi : IDisposable
    {
        public VideoClubContext VideoClubContext { get; }

        public VideoClubDi(VideoClubContext videoClubContext)
        {
            VideoClubContext = videoClubContext;
        }

        public int Save() => VideoClubContext.SaveChanges();

        public void Dispose() => VideoClubContext?.Dispose();
    }
}
