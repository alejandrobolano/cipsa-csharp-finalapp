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
        public static RentalService Instance { get; } = new RentalService();
        public RentalService()
        {
            var videoClubDi = new VideoClubDi(VideoClubContext.GetVideoClubContext());
            _rentalRepository = new RentalRepository(videoClubDi);
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

        public bool StartRentalProduct(RentalDto rentalDto, int quantityRental, StateProductEnum stateNew, out decimal price)
        {
            bool result;
            price = 0;
            try
            {
                var finishRental = rentalDto.StartRental.AddDays(quantityRental);
                rentalDto.FinishRental = finishRental;
                result = Add(rentalDto, out var id);
                if (!result) return false;

                var product = HasBeenChangeStateProduct(rentalDto, stateNew, out result);

                price = product.Price;

                if (result)
                {
                    ClientService.Instance.AddQuantityRental(rentalDto.ClientId, quantityRental);
                    var rental = Get(id);
                    var client = ClientService.Instance.Get(rental.ClientId);
                    price *= quantityRental;
                    price -= price * decimal.Divide(client.Discount, 100);
                    rental.Price = decimal.Round(price,2);
                    rental.State = StateRentalEnum.Activated;
                    Update(rental);
                }


            }
            catch (Exception exception)
            {
                Helper.HandleLogError(exception.Message);
                result = false;
            }

            return result;
        }

        private static ProductDto HasBeenChangeStateProduct(RentalDto rentalDto, StateProductEnum stateNew, out bool result)
        {
            ProductDto product;
            if (rentalDto.ProductId.Contains(CommonHelper.Movie))
            {
                product = MovieService.Instance.Get(rentalDto.ProductId);
                result = MovieService.Instance.HasBeenChangeState((MovieDto) product, stateNew);
            }
            else
            {
                product = VideoGameService.Instance.Get(rentalDto.ProductId);
                result = VideoGameService.Instance.HasBeenChangeState((VideoGameDto) product, stateNew);
            }

            return product;
        }

        public bool FinishRentalProduct(string idClient, string idProduct, out decimal differencePrice)
        {
            bool result;
            var rental = GetRentalByClientAndProduct(idClient, idProduct);
            var differenceDays = (DateTime.Today - rental.FinishRental).Days;
            differencePrice = 0;
            try
            {
                if (idProduct.Contains(CommonHelper.Movie))
                {
                    var movie = MovieService.Instance.Get(idProduct);
                    movie.State = StateProductEnum.Available;
                    result = MovieService.Instance.Update(movie);
                    differencePrice = differenceDays * movie.Price;
                }
                else
                {
                    var videoGame = VideoGameService.Instance.Get(idProduct);
                    videoGame.State = StateProductEnum.Available;
                    result = VideoGameService.Instance.Update(videoGame);
                    differencePrice = differenceDays * videoGame.Price;
                }

                if (result)
                {
                    ChangeStateRental(rental, StateRentalEnum.Returned);
                }
            }
            catch (Exception exception)
            {
                Helper.HandleLogError(exception.Message);
                result = false;
            }
            return result;
        }

        private void ChangeStateRental(RentalDto rental, StateRentalEnum state)
        {
            rental.State = state;
            Update(rental);
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

        public List<RentalDto> GetRentalsByState(StateRentalEnum stateRental)
        {
            var rentals = All();
            var rentalsByState = new List<RentalDto>();
            rentals.ForEach(rental =>
            {
                if (rental.State.Equals(stateRental))
                {
                    rentalsByState.Add(rental);
                }
            });
            return rentalsByState;
        }
        public List<RentalDto> GetRentalsByState(StateRentalEnum stateRental, StateProductEnum stateProduct)
        {
            var rentals = All();
            var rentalsByState = new List<RentalDto>();
            rentals.ForEach(rental =>
            {
                if (rental.ProductId.Contains(CommonHelper.Movie))
                {
                    var movie = MovieService.Instance.Get(rental.ProductId);
                    CheckProductByState(stateRental, stateProduct, movie, rentalsByState, rental);
                }
                else
                {
                    var videoGame = VideoGameService.Instance.Get(rental.ProductId);
                    CheckProductByState(stateRental, stateProduct, videoGame, rentalsByState, rental);
                }
            });
            return rentalsByState;
        }

        private static void CheckProductByState(StateRentalEnum stateRental, StateProductEnum stateProduct,
            ProductDto productDto, ICollection<RentalDto> rentalsByState,
            RentalDto rental)
        {
            if (rental.State.Equals(stateRental) && productDto.State.Equals(stateProduct))
            {
                rentalsByState.Add(rental);
            }
        }

        #endregion

    }
}
