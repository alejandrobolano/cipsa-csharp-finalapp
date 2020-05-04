using System;
using System.Linq;
using VideoClub.Common.Model.Utils;
using VideoClub.Infrastructure.Repository.Entity;
using VideoClub.Infrastructure.Repository.Extensions;
using VideoClub.Infrastructure.Repository.UnitOfWork;
using VideoClub.Infrastructure.Repository.Utils;

namespace VideoClub.Infrastructure.Repository.Implementations
{
    public class ClientRepository : CommonRepository<Client>
    {
        private readonly VideoClubContext _videoClubContext;
        public ClientRepository(VideoClubDi videoClubDi) : base(videoClubDi)
        {
            _videoClubContext = videoClubDi.VideoClubContext;
        }
        
        public override bool Remove(string id)
        {
            var result = false;
            try
            {
                var item = Get(id);
                _videoClubContext.Set<Client>().Remove(item ?? throw new InvalidOperationException());
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

        public override Client Get(string id)
        {
            return _videoClubContext.Set<Client>().FirstOrDefault(client => 
                client.Id.ToLower().Equals(id.ToLower()) ||
                (client.Name.ToLower() + client.LastName.ToLower()).Equals(id.ToLower()) ||
                client.Accreditation.ToLower().Equals(id.ToLower()));
        }

        public override bool Add(Client model)
        {
            var random = new Random();
            model.Id = Helper.GetCodeNumber(CommonHelper.Client, 6, random);
            return base.Add(model);
        }

    }
}
