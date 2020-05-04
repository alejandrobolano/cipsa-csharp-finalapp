using System;
using System.Linq;
using VideoClub.Common.Model.Utils;
using VideoClub.Infrastructure.Repository.Entity;
using VideoClub.Infrastructure.Repository.Extensions;
using VideoClub.Infrastructure.Repository.UnitOfWork;
using VideoClub.Infrastructure.Repository.Utils;

namespace VideoClub.Infrastructure.Repository.Implementations
{
    public class MovieRepository : CommonRepository<Movie>
    {
        private readonly VideoClubContext _videoClubContext;
        public MovieRepository(VideoClubDi videoClubDi) : base(videoClubDi)
        {
            _videoClubContext = videoClubDi.VideoClubContext;
        }
        
        public override bool Remove(string id)
        {
            var result = false;
            try
            {
                var item = Get(id);
                _videoClubContext.Set<Movie>().Remove(item ?? throw new InvalidOperationException());
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

        public override Movie Get(string id)
        {
            return _videoClubContext.Set<Movie>().FirstOrDefault(videoGame => 
                videoGame.Id.ToLower().Equals(id.ToLower()) || 
                videoGame.Title.ToLower().Equals(id.ToLower()));
        }

        public override bool Add(Movie model)
        {
            var random = new Random();
            model.Id = Helper.GetCodeNumber(CommonHelper.Movie, 6, random);
            return base.Add(model);
        }


    }
}
