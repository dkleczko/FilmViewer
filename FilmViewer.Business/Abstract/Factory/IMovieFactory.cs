using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Abstract.Factory
{
    public interface IMovieFactory
    {
        void SetValuesByMovieDetailsDto(Movie movie, MovieDetailsDto movieDetails);
        void SetDirector(Movie movie, int directorId);
        void SetActors(Movie movie, List<int> actorIds);
        void SetCategories(Movie movie, List<int> categoryIds);
        void SetMetadata(Movie movie, List<int> metadataIds);
    }
}
