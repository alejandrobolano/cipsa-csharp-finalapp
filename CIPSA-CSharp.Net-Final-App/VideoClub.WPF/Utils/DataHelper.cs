using System;
using System.Linq;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.BusinessLogic.Implementations;
using VideoClub.Common.Model.Enums;

namespace VideoClub.WPF.Utils
{
    public class DataHelper
    {
        public static void LoadData()
        {
            if (!ClientService.Instance.All().Any())
            {
                var client = new ClientDto();
                AddClient(client, "40565712Z", AccreditationEnum.Dni, "Poeta Llombart",
                    "micorreo@cor.es", "Alfa", "Alfacet", "666909090", DateTime.Now, StateClientEnum.Activated, false);
                ClientService.Instance.Add(client, out _);
                client = new ClientDto();
                AddClient(client, "48915757R", AccreditationEnum.Dni, "Poeta Gonzalez",
                    "gonzalez@cor.es", "Gonzalo", "Gonzalez", "666903445", DateTime.Now, StateClientEnum.Blocked, false);
                ClientService.Instance.Add(client, out _);
                client = new ClientDto();
                AddClient(client, "Z6591166S", AccreditationEnum.Nie, "Poeta Gonzalez",
                    "alejandro@cor.es", "Alejandro", "Bolaño", "666234445", DateTime.Now, StateClientEnum.Leave, false);
                ClientService.Instance.Add(client, out _);
                client = new ClientDto();
                AddClient(client, "Z4017538H", AccreditationEnum.Nie, "Poeta Gonzalez",
                    "manuel@cor.es", "Manuel", "Bolaño", "662234345", DateTime.Now, StateClientEnum.Leave, true);
                ClientService.Instance.Add(client, out _);
            }

            if (!MovieService.Instance.All().Any())
            {
                var movie = new MovieDto();
                AddMovie(movie, "La casa blanca", 3, 1, 2018, 2020, new TimeSpan(
                        Convert.ToInt32(1),
                        Convert.ToInt32(23),
                        Convert.ToInt32(33)),
                    StateProductEnum.Available);
                MovieService.Instance.Add(movie, out _);
                movie = new MovieDto();
                AddMovie(movie, "La flor blanca", 20, 2, 2016, 2019, new TimeSpan(
                        Convert.ToInt32(2),
                        Convert.ToInt32(13),
                        Convert.ToInt32(35)),
                    StateProductEnum.BadState);
                MovieService.Instance.Add(movie, out _);
                movie = new MovieDto();
                AddMovie(movie, "La flor blanca", 15, 3, 2017, 208, new TimeSpan(
                        Convert.ToInt32(1),
                        Convert.ToInt32(13),
                        Convert.ToInt32(35)),
                    StateProductEnum.Available);
                MovieService.Instance.Add(movie, out _);
            }

            if (!VideoGameService.Instance.All().Any())
            {
                var videoGame = new VideoGameDto();
                AddVideoGame(videoGame, "Tetris", 3, 2, GamePlatformEnum.Pc, StateProductEnum.Available);
                VideoGameService.Instance.Add(videoGame, out _);
                videoGame = new VideoGameDto();
                AddVideoGame(videoGame, "Call of duty", 5, 3, GamePlatformEnum.Wii, StateProductEnum.Lost);
                VideoGameService.Instance.Add(videoGame, out _);
                videoGame = new VideoGameDto();
                AddVideoGame(videoGame, "Need for Speed", 5, 1, GamePlatformEnum.XBox, StateProductEnum.Available);
                VideoGameService.Instance.Add(videoGame, out _);
                videoGame = new VideoGameDto();
                AddVideoGame(videoGame, "Counter", 10, 1, GamePlatformEnum.XBox, StateProductEnum.Available);
                VideoGameService.Instance.Add(videoGame, out _);
            }
        }

        private static void AddClient(ClientDto client,
            string accreditation, AccreditationEnum accreditationType, string address, string email,
            string name, string lastName, string phone, DateTime subscriptionDate, StateClientEnum state, bool isVip)
        {
            client.Accreditation = accreditation;
            client.AccreditationType = accreditationType;
            client.Address = address;
            client.Email = email;
            client.Name = name;
            client.LastName = lastName;
            client.PhoneContact = "34" + phone;
            client.SubscriptionDate = subscriptionDate;
            client.State = state;
            client.IsVip = isVip;
        }

        private static void AddMovie(MovieDto movie,
            string title, decimal price, int quantityDisc, int productionYear, int buyYear,
            TimeSpan durationTimeSpan, StateProductEnum state)
        {
            movie.Title = title;
            movie.Price = price;
            movie.QuantityDisc = quantityDisc;
            movie.ProductionYear = productionYear;
            movie.BuyYear = buyYear;
            movie.Duration = durationTimeSpan;
            movie.State = state;
        }
        private static void AddVideoGame(VideoGameDto videoGame,
            string title, decimal price, int quantityDisc, GamePlatformEnum platformEnum, StateProductEnum state)
        {
            videoGame.Title = title;
            videoGame.Price = price;
            videoGame.QuantityDisc = quantityDisc;
            videoGame.Platform = platformEnum;
            videoGame.State = state;
        }


    }
}
