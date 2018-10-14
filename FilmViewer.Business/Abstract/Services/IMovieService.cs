using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Email;
using FilmViewer.Business.Dto.Extended.Movie;
using FilmViewer.Business.Enums;

namespace FilmViewer.Business.Abstract.Services
{
    public interface IMovieService
    {
        AddToFavouriteResult AddOrRemoveMovieFromFavourites(int movieId, string userId);
        VoteMovieResult VoteMovie(int voteScore, int movieId, string userId);
        void AddComment(string content, int movieId, string userId);

        void RemoveComment(int commentId);

        int AddMovie(MovieDetailsDto movieDetails, List<int> actorsIds, int? directorId, List<int> metadataIds,
            List<int> categoryIds, string serverPath, string virtualPath);

        void AddPhotosToMovie(MovieDetailsExtendedDto movie);
        void DeleteMovie(int id);
        void DeletePhotoFromMovie(string fileName, int movieId);
        void EditMovie(MovieDetailsDto movieDetailsDto, List<int> actorIds, int? directorId, List<int> metadataIds, List<int> categoryIds);
    }
}
