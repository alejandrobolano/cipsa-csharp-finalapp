using System.Collections.Generic;
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
    public class VideoGameService : IService<VideoGameDto>
    {
        private readonly VideoGameRepository _videoGameRepository;
        public static VideoGameService Instance { get; } = new VideoGameService();
        public VideoGameService()
        {
            var videoClubDi = new VideoClubDi(VideoClubContext.GetVideoClubContext());
            _videoGameRepository = new VideoGameRepository(videoClubDi);
        }

        #region private methods

        private static IMapper MapperToModel()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VideoGameDto, VideoGame>());
            var mapper = config.CreateMapper();
            return mapper;
        }
        private static IMapper MapperToDto()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VideoGame, VideoGameDto>());
            var mapper = config.CreateMapper();
            return mapper;
        }

        #endregion

        #region common public methods

        public bool Add(VideoGameDto model, out string id)
        {
            var mapper = MapperToModel();
            var videoGame = mapper.Map<VideoGameDto, VideoGame>(model);

            return _videoGameRepository.Add(videoGame, out id);
        }

        public bool Remove(string id)
        {
            return _videoGameRepository.Remove(id);
        }

        public VideoGameDto Get(string id)
        {
            var videoGame = _videoGameRepository.Get(id);
            var mapper = MapperToDto();
            var videoGameDto = mapper.Map<VideoGame, VideoGameDto>(videoGame);
            return videoGameDto;
        }

        public bool Update(VideoGameDto modelDto)
        {
            var mapper = MapperToModel();
            var videoGame = mapper.Map<VideoGameDto, VideoGame>(modelDto);
            return _videoGameRepository.Update(videoGame);
        }

        public List<VideoGameDto> All()
        {
            var videoGameDtos = new List<VideoGameDto>();
            _videoGameRepository.All().ForEach(videoGame =>
            {
                var mapper = MapperToDto();
                var videoGameDto = mapper.Map<VideoGame, VideoGameDto>(videoGame);
                videoGameDtos.Add(videoGameDto);
            });
            return videoGameDtos;
        }

        #endregion

        #region custom public methods
        public bool HasBeenChangeState(VideoGameDto videoGame, StateProductEnum state)
        {
            videoGame.State = state;
            return Update(videoGame);
        }

        public List<VideoGameDto> GetVideoGamesByState(StateProductEnum state)
        {
            var videoGames = new List<VideoGameDto>();
            All().ForEach(videoGameDto =>
            {
                if (videoGameDto.State.Equals(state))
                {
                    videoGames.Add(videoGameDto);
                }
            });

            return videoGames;
        }

        #endregion


    }
}
