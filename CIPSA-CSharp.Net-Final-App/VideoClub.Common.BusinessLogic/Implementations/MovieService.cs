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
    public class MovieService : IService<MovieDto>
    {
        private readonly MovieRepository _movieRepository;
        public static MovieService Instance { get; } = new MovieService();
        public MovieService()
        {
            var videoClubDi = new VideoClubDi(VideoClubContext.GetVideoClubContext());
            _movieRepository = new MovieRepository(videoClubDi);
        }

        #region private methods

        private static IMapper MapperToModel()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MovieDto, Movie>());
            var mapper = config.CreateMapper();
            return mapper;
        }
        private static IMapper MapperToDto()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieDto>());
            var mapper = config.CreateMapper();
            return mapper;
        }

        #endregion

        #region common public methods

        public bool Add(MovieDto model, out string id)
        {
            var mapper = MapperToModel();
            var movie = mapper.Map<MovieDto, Movie>(model);

            return _movieRepository.Add(movie, out id);
        }

        public bool Remove(string id)
        {
            return _movieRepository.Remove(id);
        }

        public MovieDto Get(string id)
        {
            var movie = _movieRepository.Get(id);
            var mapper = MapperToDto();
            var movieDto = mapper.Map<Movie, MovieDto>(movie);
            return movieDto;
        }

        public bool Update(MovieDto modelDto)
        {
            var mapper = MapperToModel();
            var movie = mapper.Map<MovieDto, Movie>(modelDto);
            return _movieRepository.Update(movie);
        }

        public List<MovieDto> All()
        {
            var movieDtos = new List<MovieDto>();
            _movieRepository.All().ForEach(movie =>
            {
                var mapper = MapperToDto();
                var movieDto = mapper.Map<Movie, MovieDto>(movie);
                movieDtos.Add(movieDto);
            });
            return movieDtos;
        }

        #endregion

        #region custom methods

        public bool HasBeenChangeState(MovieDto movie, StateProductEnum state)
        {
            movie.State = state;
            return Update(movie);
        }

        public List<MovieDto> GetMoviesByState(StateProductEnum state)
        {
            var movies = new List<MovieDto>();
            All().ForEach(movieDto =>
            {
                if (movieDto.State.Equals(state))
                {
                    movies.Add(movieDto);
                }
            });

            return movies;
        }

        #endregion




    }
}
