using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VideoClub.Common.BusinessLogic.Contracts;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Utils;
using VideoClub.Infrastructure.Repository;
using VideoClub.Infrastructure.Repository.Entity;
using VideoClub.Infrastructure.Repository.Implementations;
using VideoClub.Infrastructure.Repository.UnitOfWork;
using VideoClub.Infrastructure.Repository.Utils;

namespace VideoClub.Common.BusinessLogic.Implementations
{
    public class RentalService : IService<RentalDto>
    {
        private readonly RentalRepository _rentalRepository;
        private readonly MovieService _movieService;
        private readonly VideoGameService _videoGameService;
        public RentalService()
        {
            var videoClubDi = new VideoClubDi(VideoClubContext.GetVideoClubContext());
            _rentalRepository = new RentalRepository(videoClubDi);
            _movieService = new MovieService();
            _videoGameService = new VideoGameService();
        }

        #region private methods

        private static IMapper MapperToModel()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDto, Product>();
                cfg.CreateMap<ClientDto, Client>();
                cfg.CreateMap<RentalDto, Rental>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
        private static IMapper MapperToDto()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Product, ProductDto>();
                    cfg.CreateMap<Client, ClientDto>();
                    cfg.CreateMap<Rental, RentalDto>();
                });
            var mapper = config.CreateMapper();
            return mapper;
        }

        #endregion

        #region common public methods

        public bool Add(RentalDto model)
        {
            var mapper = MapperToModel();
            var rental = mapper.Map<RentalDto, Rental>(model);

            return _rentalRepository.Add(rental);
        }

        public bool Remove(string id)
        {
            return _rentalRepository.Remove(id);
        }

        public RentalDto Get(string id)
        {
            var rental = _rentalRepository.Get(id);
            var mapper = MapperToDto();
            var rentalDto = mapper.Map<Rental, RentalDto>(rental);
            return rentalDto;
        }

        public bool Update(RentalDto modelDto)
        {
            var mapper = MapperToModel();
            var client = mapper.Map<RentalDto, Rental>(modelDto);
            return _rentalRepository.Update(client);
        }

        public List<RentalDto> All()
        {
            var rentalDtos = new List<RentalDto>();
            _rentalRepository.All().ForEach(rental =>
            {
                var mapper = MapperToDto();
                var rentalDto = mapper.Map<Rental, RentalDto>(rental);
                rentalDtos.Add(rentalDto);
            });
            return rentalDtos;
        }

        #endregion

        #region custom public methods

        public bool StartRentalProduct(RentalDto rentalDto, StateProductEnum stateNew)
        {
            var result = Add(rentalDto);
            if (result)
            {
                try
                {
                    if (rentalDto.ProductId.Contains(CommonHelper.Movie))
                    {
                        var movie = _movieService.Get(rentalDto.ProductId);
                        movie.State = stateNew;
                        result = _movieService.Update(movie);
                    }
                    else
                    {
                        var videoGame = _videoGameService.Get(rentalDto.ProductId);
                        videoGame.State = stateNew;
                        result = _videoGameService.Update(videoGame);
                    }
                }
                catch (Exception exception)
                {
                    Helper.Log.Error(exception.Message);
                    result = false;
                }

            }

            return result;
        }

        public bool FinishRentalProduct(string idClient, string idProduct, out decimal differencePrice)
        {
            bool result;
            var rental = GetRentalByClientAndProduct(idClient, idProduct);
            var differenceDays = DateTime.Today - rental.FinishRental;
            differencePrice = 0;
            try
            {
                if (idProduct.Contains(CommonHelper.Movie))
                {
                    var movie = _movieService.Get(idProduct);
                    movie.State = StateProductEnum.Available;
                    result = _movieService.Update(movie);
                    differencePrice = differenceDays.Days * movie.Price;
                }
                else
                {
                    var videoGame = _videoGameService.Get(idProduct);
                    videoGame.State = StateProductEnum.Available;
                    result = _videoGameService.Update(videoGame);
                    differencePrice = differenceDays.Days * videoGame.Price;
                }
            }
            catch (Exception exception)
            {
                Helper.Log.Error(exception.Message);
                result = false;
            }
            return result;
        }


        public List<RentalDto> GetRentalsByClient(string id)
        {
            var rentalsByClient = _rentalRepository.GetRentalsByClient(id);
            var mapper = MapperToDto();
            var rentalsByClientDtos = mapper.Map<List<Rental>, List<RentalDto>>(rentalsByClient);
            return rentalsByClientDtos;
        }
        public List<RentalDto> GetRentalsByProduct(string id)
        {
            var rentals = _rentalRepository.GetRentalsByProduct(id);
            var mapper = MapperToDto();
            var rentalsByProductDtos = mapper.Map<List<Rental>, List<RentalDto>>(rentals);
            return rentalsByProductDtos;
        }
        public RentalDto GetRentalByClientAndProduct(string idClient, string idProduct)
        {
            var rental = _rentalRepository.GetRentalByClientAndProduct(idClient, idProduct);
            var mapper = MapperToDto();
            var rentalByClientAndProduct = mapper.Map<Rental, RentalDto>(rental);
            return rentalByClientAndProduct;
        }

        public List<RentalDto> GetRentalsByState(StateProductEnum state)
        {
            var rentals = All();
            var rentalsByState = new List<RentalDto>();
            rentals.ForEach(rental =>
            {
                if (rental.ProductId.Contains(CommonHelper.Movie))
                {
                    var movie = _movieService.Get(rental.ProductId);
                    CheckProductByState(state, movie, rentalsByState, rental);
                }
                else
                {
                    var videoGame = _videoGameService.Get(rental.ProductId);
                    CheckProductByState(state, videoGame, rentalsByState, rental);
                }
            });
            return rentalsByState;
        }

        private static void CheckProductByState(StateProductEnum state, ProductDto productDto, ICollection<RentalDto> rentalsByState,
            RentalDto rental)
        {
            if (productDto.State.Equals(state))
            {
                rentalsByState.Add(rental);
            }
        }

        #endregion

    }
}
