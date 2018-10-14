using System.Collections.Generic;
using System.Linq;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Actor;
using FilmViewer.Business.Dto.Extended.Director;
using FilmViewer.Business.Dto.Extended.Movie;
using FilmViewer.Business.Mappings;
using FilmViewer.Business.RecommendationsEngine;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.DataProviders
{
    public class UserDataProvider : IUserDataProvider
    {
        private readonly IUnitOfWork _uow;

        public UserDataProvider(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public int GetUserFavouritesCount(string userId)
        {
            return _uow.UserRepository.GetUserFavouritesCount(userId);
        }

        public int GetUserMovieVotesCount(string userId)
        {
            return _uow.UserRepository.GetUserMovieVotesCount(userId);
        }

        public int GetUserActorVotesCount(string userId)
        {
            return _uow.UserRepository.GetUserActorVotesCount(userId);
        }

        public List<VotedMoviesByUserDto> GetVotedMoviesByUser(string userId)
        {
            var votedMoviesList = _uow.UserRepository.GetApplicationUserWithVoteMovie(userId).VoteMovie;

            return BusinessMapper.Mapper.Map<List<VotedMoviesByUserDto>>(votedMoviesList);
        }

        public List<VotedActorsByUserDto> GetVotesActorsByUser(string userId)
        {
            var votedActorsList = new List<VoteMoviePerson>();
            var applicationUser = _uow.UserRepository.GetApplicationUserWithVoteMoviePerson(userId);
            var votedMoviePersons = applicationUser?.VoteMoviePerson;
            if (votedMoviePersons != null)
            {
                votedActorsList = votedMoviePersons.Where(p => p.MoviePerson is Actor).ToList();
            }

            return BusinessMapper.Mapper.Map<List<VotedActorsByUserDto>>(votedActorsList);
        }

        public List<MovieDto> GetFavouritesMoviesByUser(string userId)
        {
            var appUser = _uow.UserRepository.GetApplicationUserWithFavourites(userId);

            var favouriteMoviesList = appUser != null ? appUser.Favourites : new List<Movie>();

            return BusinessMapper.Mapper.Map<List<MovieDto>>(favouriteMoviesList);
        }

        public List<MovieWithSimilarityDto> GetRecommendedMoviesWithSimilarity(ApplicationUser user)
        {
            Recommendation.Initialize(_uow);
            var movies = Recommendation.PrepareMovieForUser(user, ViewType.AllRecomendations);

            return BusinessMapper.Mapper.Map<List<MovieWithSimilarityDto>>(movies);
        }

        public List<VotedDirectorByUserDto> GetVotedDirectorsByUser(string userId)
        {
            var votedActorsList = new List<VoteMoviePerson>();
            var applicationUser = _uow.UserRepository.GetApplicationUserWithVoteMoviePerson(userId);
            var votedMoviePersons = applicationUser?.VoteMoviePerson;
            if (votedMoviePersons != null)
            {
                votedActorsList = votedMoviePersons.Where(p => p.MoviePerson is Director).ToList();
            }

            return BusinessMapper.Mapper.Map<List<VotedDirectorByUserDto>>(votedActorsList);
        }

        public List<ApplicationUserDetailsDto> SearchUsersByUsername(string searchString = "")
        {
            var userEntities = _uow.UserRepository.SearchUsersByUserName(searchString);
            var result = new List<ApplicationUserDetailsDto>();
            var allRoles = _uow.RoleRepository.GetAll().ToArray();
            foreach (var userEntity in userEntities)
            {
                var appUserDto = BusinessMapper.Mapper.Map<ApplicationUserDetailsDto>(userEntity);
                if (userEntity.Roles.Count > 0)
                {
                    var userRoles = allRoles.Where(p => userEntity.Roles.Select(x => x.RoleId).Contains(p.Id)).ToArray();

                    appUserDto.Roles = BusinessMapper.Mapper.Map<List<RoleDto>>(userRoles);
                }
                result.Add(appUserDto);
            }

            return result;
        }

        public ApplicationUserDetailsDto GetApplicationUserDetailsById(string id)
        {
            ApplicationUserDetailsDto appUserDto = null;
            var appUserEntity = _uow.UserRepository.Get(id);
            if (appUserEntity != null)
            {
                var allRoles = _uow.RoleRepository.GetAll().ToArray();
                appUserDto = BusinessMapper.Mapper.Map<ApplicationUserDetailsDto>(appUserEntity);
                if (appUserEntity.Roles.Count > 0)
                {
                    var userRoles = allRoles.Where(p => appUserEntity.Roles.Select(x => x.RoleId).Contains(p.Id)).ToArray();

                    appUserDto.Roles = BusinessMapper.Mapper.Map<List<RoleDto>>(userRoles);
                }
            }
            return appUserDto;
        }
    }
}
