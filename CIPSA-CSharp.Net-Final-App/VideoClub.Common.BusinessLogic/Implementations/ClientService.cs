using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VideoClub.Common.BusinessLogic.Contracts;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.Model.Enums;
using VideoClub.Infrastructure.Repository;
using VideoClub.Infrastructure.Repository.Entity;
using VideoClub.Infrastructure.Repository.Implementations;
using VideoClub.Infrastructure.Repository.UnitOfWork;

namespace VideoClub.Common.BusinessLogic.Implementations
{
    public class ClientService : IService<ClientDto>
    {
        private readonly ClientRepository _clientRepository;
        public static ClientService Instance { get; } = new ClientService();

        public ClientService()
        {
            var videoClubDi = new VideoClubDi(VideoClubContext.GetVideoClubContext());
            _clientRepository = new ClientRepository(videoClubDi);
        }

        #region private methods

        private static IMapper MapperToModel()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ClientDto, Client>());
            var mapper = config.CreateMapper();
            return mapper;
        }
        private static IMapper MapperToDto()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDto>());
            var mapper = config.CreateMapper();
            return mapper;
        }

        #endregion

        #region common public methods

        public bool Add(ClientDto model)
        {
            var mapper = MapperToModel();
            var client = mapper.Map<ClientDto, Client>(model);

            return _clientRepository.Add(client);
        }

        public bool Remove(string id)
        {
            return _clientRepository.Remove(id);
        }

        public ClientDto Get(string id)
        {
            var client = _clientRepository.Get(id);
            var mapper = MapperToDto();
            var clientDto = mapper.Map<Client, ClientDto>(client);
            return clientDto;
        }

        public bool Update(ClientDto modelDto)
        {
            var mapper = MapperToModel();
            var client = mapper.Map<ClientDto, Client>(modelDto);
            return _clientRepository.Update(client);
        }

        public List<ClientDto> All()
        {
            var clientDtos = new List<ClientDto>();
            _clientRepository.All().ForEach(client =>
            {
                var mapper = MapperToDto();
                var clientDto = mapper.Map<Client, ClientDto>(client);
                clientDtos.Add(clientDto);
            });
            return clientDtos;
        }

        #endregion

        #region custom public methods
        public void UpdateDiscount(ClientDto client)
        {
            var quantityRentalMonth = 0;
            if (quantityRentalMonth >= 5 && quantityRentalMonth < 10)
                client.Discount = 10;
            else if (quantityRentalMonth >= 10 && quantityRentalMonth < 15)
                client.Discount = 15;
            else if (quantityRentalMonth >= 15 && quantityRentalMonth < 20)
                client.Discount = 20;
            else if (quantityRentalMonth >= 20 && quantityRentalMonth < 30)
                client.Discount = 25;
            else if (quantityRentalMonth >= 30) client.Discount = 50;
        }

        public void UpdateClientsForVip()
        {
            All().ForEach(client =>
            {
                if (client.RentalQuantity >= 60)
                {
                    client.IsVip = true;
                }
            });
        }

        public List<ClientDto> GetClientsByState(StateClientEnum stateClient)
        {
            var activatedClients = new List<ClientDto>();
            All().ForEach(client =>
            {
                if (client.State.Equals(stateClient))
                {
                    activatedClients.Add(client);
                }
            });

            return activatedClients;
        }

        private void UpdateStateClient(ClientDto client, StateClientEnum stateNew)
        {
            client.State = stateNew;
            Update(client);
        }

        public void ProcessOfChangeStateToBlocked()
        {
            var clients = All();
            clients.ForEach(client =>
            {
                var rentalsByClient = RentalService.Instance.GetRentalsByClient(client.Id);
                if (rentalsByClient.Any(x => x.FinishRental < DateTime.Today))
                {
                    UpdateStateClient(client, StateClientEnum.Blocked);
                }
            });

        }

        public void AddQuantityRental(string id, int newQuantityRentals)
        {
            var client = Get(id);
            client.RentalQuantity += newQuantityRentals;
            Update(client);
        }

        #endregion

    }
}
