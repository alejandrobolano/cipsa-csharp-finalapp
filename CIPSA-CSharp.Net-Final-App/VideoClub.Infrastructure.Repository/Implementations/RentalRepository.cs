using System;
using System.Collections.Generic;
using System.Linq;
using VideoClub.Common.Model.Utils;
using VideoClub.Infrastructure.Repository.Entity;
using VideoClub.Infrastructure.Repository.Extensions;
using VideoClub.Infrastructure.Repository.UnitOfWork;
using VideoClub.Infrastructure.Repository.Utils;

namespace VideoClub.Infrastructure.Repository.Implementations
{
    public class RentalRepository : CommonRepository<Rental>
    {
        private readonly VideoClubContext _videoClubContext;
        public RentalRepository(VideoClubDi videoClubDi) : base(videoClubDi)
        {
            _videoClubContext = videoClubDi.VideoClubContext;
        }
        
        public override bool Remove(string id)
        {
            var result = false;
            try
            {
                var item = Get(id);
                _videoClubContext.Set<Rental>().Remove(item ?? throw new InvalidOperationException());
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

        public override bool Add(Rental model)
        {
            var random = new Random();
            model.Id = Helper.GetCodeNumber(CommonHelper.Rental, 6, random);
            return base.Add(model);
        }

        public override Rental Get(string id)
        {
            return _videoClubContext.Set<Rental>().FirstOrDefault(rental => 
                rental.Id.ToLower().Equals(id.ToLower()));
        }

        public List<Rental> GetRentalsByProduct(string id)
        {
            return _videoClubContext.Set<Rental>().Where(rental =>
                rental.Product.Id.ToLower().Equals(id.ToLower()) ||
                rental.Product.Title.ToLower().Equals(id.ToLower())).ToList();
        }
        public List<Rental> GetRentalsByClient(string id)
        {
            return _videoClubContext.Set<Rental>().Where(rental =>
                rental.Client.Id.ToLower().Equals(id.ToLower()) ||
                (rental.Client.Name.ToLower() + rental.Client.LastName.ToLower()).Replace(" ", "")
                .Equals(id.ToLower().Replace(" ", ""))).ToList();
        }
        public Rental GetRentalByClientAndProduct(string idClient, string idProduct)
        {
            return _videoClubContext.Set<Rental>().FirstOrDefault(rental =>
                rental.Client.Id.ToLower().Equals(idClient.ToLower()) ||
                (rental.Client.Name.ToLower() + rental.Client.LastName.ToLower()).Replace(" ", "")
                .Equals(idClient.ToLower().Replace(" ", "")) &&
                rental.Product.Id.ToLower().Equals(idProduct.ToLower()) ||
                rental.Product.Title.ToLower().Equals(idProduct.ToLower()));
        }


    }
}
