using System;
using System.Linq;
using VideoClub.Common.Model.Utils;
using VideoClub.Infrastructure.Repository.Entity;
using VideoClub.Infrastructure.Repository.Extensions;
using VideoClub.Infrastructure.Repository.UnitOfWork;
using VideoClub.Infrastructure.Repository.Utils;

namespace VideoClub.Infrastructure.Repository.Implementations
{
    public class VideoGameRepository : CommonRepository<VideoGame>
    {
        private readonly VideoClubContext _videoClubContext;
        public VideoGameRepository(VideoClubDi videoClubDi) : base(videoClubDi)
        {
            _videoClubContext = videoClubDi.VideoClubContext;
        }
        
        public override bool Remove(string id)
        {
            var result = false;
            try
            {
                var item = Get(id);
                _videoClubContext.Set<VideoGame>().Remove(item ?? throw new InvalidOperationException());
                SaveChanges();
                Helper.Log.Info($"Has been {item.GetType().Name} deleted successfully with ID: {item.Id}");
                result = true;
            }
            catch (Exception exception)
            {
                exception.CustomDescription();
            }

            return result;
        }

        public override VideoGame Get(string id)
        {
            return _videoClubContext.Set<VideoGame>().FirstOrDefault(videoGame => 
                videoGame.Id.ToLower().Equals(id.ToLower()) || 
                videoGame.Title.ToLower().Equals(id.ToLower()));
        }

        public override bool Add(VideoGame model)
        {
            var random = new Random();
            model.Id = Helper.GetCodeNumber(CommonHelper.VideoGame, 6, random);
            return base.Add(model);
        }

    }
}
