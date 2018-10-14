using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FilmViewer.Business.Abstract.Factory;
using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Email;
using FilmViewer.Business.Dto.Extended.Movie;
using FilmViewer.Business.Enums;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMovieFactory _movieFactory;
        private readonly IDirectoryFactory _directoryFactory;
        public MovieService(IUnitOfWork unitOfWork, IMovieFactory movieFactory, IDirectoryFactory directoryFactory)
        {
            _uow = unitOfWork;
            _movieFactory = movieFactory;
            _directoryFactory = directoryFactory;
        }

        public AddToFavouriteResult AddOrRemoveMovieFromFavourites(int movieId, string userId)
        {
            var user = _uow.UserRepository.GetApplicationUserWithFavourites(userId);
            var movie = _uow.MovieRepository.GetMovieById(movieId);
            if (user != null && movie != null)
            {
                var isThisMovieAlreadyFavourite = user.Favourites.Any(p => p.Id == movieId);
                if (isThisMovieAlreadyFavourite)
                {
                    user.Favourites.Remove(movie);
                    var res = _uow.Complete();
                    return res > 0 ? AddToFavouriteResult.MovieRemovedFromFavourite : AddToFavouriteResult.UnknownError;
                }
                else
                {
                    user.Favourites.Add(movie);
                    var res = _uow.Complete();
                    return res > 0 ? AddToFavouriteResult.MovieAddedToFavourite : AddToFavouriteResult.UnknownError;
                }
            }
            return AddToFavouriteResult.UnknownError;
        }

        public VoteMovieResult VoteMovie(int voteScore, int movieId, string userId)
        {
            var user = _uow.UserRepository.GetApplicationUserWithVoteMovie(userId);
            var votesToRemove = _uow.VoteMovieRepository.GetAllVotesMakesInMovieByUser(userId, movieId);
            var movie = _uow.MovieRepository.Get(movieId);
            foreach (var vote in votesToRemove)
            {
                movie.VoteScores -= vote.VoteScore;
                movie.VoteCount--;
                _uow.VoteMovieRepository.Remove(vote);
            }
            movie.VoteScores += voteScore;
            movie.VoteCount++;
            var newVote = new VoteMovie()
            {
                ApplicationUser = user,
                Movie = movie,
                VoteScore = voteScore
            };
            user.VoteMovie.Add(newVote);
            _uow.Complete();

            return new VoteMovieResult()
            {
                VoteCount = movie.VoteCount,
                VoteScores = Math.Round(((double)movie.VoteScores / (double)movie.VoteCount), 1)
            };
        }

        public void AddComment(string content, int movieId, string userId)
        {
            var user = _uow.UserRepository.Get(userId);
            var movie = _uow.MovieRepository.Get(movieId);
            var newComment = new Comment()
            {
                Content = content,
                DateTime = DateTime.Now,
                Movie = movie,
                User = user
            };
            _uow.CommentRepository.Add(newComment);
            _uow.Complete();

        }

        public void RemoveComment(int commentId)
        {
            var commentEntity = _uow.CommentRepository.Get(commentId);

            if (commentEntity != null)
            {
                _uow.CommentRepository.Remove(commentEntity);

                _uow.Complete();
            }
        }

        public int AddMovie(MovieDetailsDto movieDetails, List<int> actorsIds, int? directorId, List<int> metadataIds,
            List<int> categoryIds, string serverPath, string virtualPath)
        {
            var movie = new Movie();
            _movieFactory.SetValuesByMovieDetailsDto(movie, movieDetails);
            if (directorId.HasValue)
            {
                _movieFactory.SetDirector(movie, directorId.Value);
            }
            if (actorsIds != null && actorsIds.Any())
            {
                _movieFactory.SetActors(movie, actorsIds);
            }
            if (categoryIds != null && categoryIds.Any())
            {
                _movieFactory.SetCategories(movie, categoryIds);
            }
            _movieFactory.SetMetadata(movie, metadataIds);
            movie.Folder = _directoryFactory.CreateDirectoryForMovie(serverPath, movie.TitleEng, virtualPath);

            _uow.MovieRepository.Add(movie);

            _uow.Complete();

            return movie.Id;
        }

        public void AddPhotosToMovie(MovieDetailsExtendedDto movie)
        {
            var movieEntity = _uow.MovieRepository.GetMovieByIdWithPhotos(movie.MovieId);
            if (movieEntity != null)
            {
                movieEntity.PhotoPath = movie.PhotoUrl;
                movieEntity.PhotoUrls = BusinessMapper.Mapper.Map<List<PhotoPath>>(movie.Photos);

                _uow.Complete();
            }
        }

        public void DeleteMovie(int id)
        {
            var movieEntity = _uow.MovieRepository.GetMovieById(id);
            var votesEntites = _uow.VoteMovieRepository.GetVotesByMovieId(id);

            _uow.MovieRepository.Remove(movieEntity);
            _uow.VoteMovieRepository.RemoveRange(votesEntites);

            _uow.Complete();
        }

        public void DeletePhotoFromMovie(string fileName, int movieId)
        {
            var actorPhotos = _uow.MovieRepository.GetMovieByIdWithPhotos(movieId);

            var photoToRemove = actorPhotos.PhotoUrls.Where(p => Path.GetFileName(p.Path) == fileName);

            _uow.PhotoPathRepository.RemoveRange(photoToRemove);

            _uow.Complete();
        }

        public void EditMovie(MovieDetailsDto movieDetailsDto, List<int> actorIds, int? directorId, List<int> metadataIds, List<int> categoryIds)
        {
            var movie = _uow.MovieRepository.GetMovieById(movieDetailsDto.MovieId);
            _movieFactory.SetValuesByMovieDetailsDto(movie, movieDetailsDto);

            if (directorId.HasValue)
            {
                _movieFactory.SetDirector(movie, directorId.Value);
            }
            else
            {
                movie.Director = null;
            }
            if (actorIds != null && actorIds.Any())
            {
                _movieFactory.SetActors(movie, actorIds);
            }
            else
            {
                movie.Actors = new List<Actor>();
            }
            if (categoryIds != null && categoryIds.Any())
            {
                _movieFactory.SetCategories(movie, categoryIds);
            }
            else
            {
                movie.Categories = new List<Category>();
            }
            _movieFactory.SetMetadata(movie, metadataIds);

            _uow.Complete();
        }
    }
}
