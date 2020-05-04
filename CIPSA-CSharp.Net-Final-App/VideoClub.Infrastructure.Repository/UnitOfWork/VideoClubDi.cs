using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Infrastructure.Repository.Implementations;

namespace VideoClub.Infrastructure.Repository.UnitOfWork
{
    public class VideoClubDi : IDisposable
    {
        private VideoClubContext _videoClubContext;
        public VideoClubContext VideoClubContext => _videoClubContext;

        public VideoClubDi(VideoClubContext videoClubContext)
        {
            _videoClubContext = videoClubContext;
        }

        public int Save() => _videoClubContext.SaveChanges();

        public void Dispose() => _videoClubContext?.Dispose();
    }
}
