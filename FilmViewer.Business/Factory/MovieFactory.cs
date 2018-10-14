using System.Collections.Generic;
using System.Linq;
using FilmViewer.Business.Abstract.Factory;
using FilmViewer.Business.Abstract.Helpers;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Helpers;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;
using Newtonsoft.Json;

namespace FilmViewer.Business.Factory
{
    public class MovieFactory : IMovieFactory
    {
        private readonly IUnitOfWork _uow;
        private readonly IMetadataHelper _metadataHelper;
        public MovieFactory(IUnitOfWork unitOfWork, IMetadataHelper metadataHelper)
        {
            _uow = unitOfWork;
            _metadataHelper = metadataHelper;
        }

        public void SetValuesByMovieDetailsDto(Movie movie, MovieDetailsDto movieDetails)
        {
            if (movie != null)
            {
                movie.TitleEng = movieDetails.Title;
                movie.TitlePl = movieDetails.Title;
                movie.Content = movieDetails.Content;
                movie.Duration = movieDetails.Duration;
                movie.PremiereDate = movieDetails.PremiereDate;
                movie.HeraldUrl = movieDetails.HearldUrl;
                movie.ProductionCountries = movieDetails.ProductionCountries;
            }
        }

        public void SetActors(Movie movie, List<int> actorIds)
        {
            if (movie != null && actorIds.Any())
            {
                var actors = _uow.ActorRepository.GetActorsByListOfIds(actorIds);

                movie.Actors = actors.ToList();
            }
        }

        public void SetDirector(Movie movie, int directorId)
        {
            if (movie != null)
            {
                var director = _uow.DirectorRepository.Get(directorId);

                movie.Director = director;
            }
        }

        public void SetCategories(Movie movie, List<int> categoryIds)
        {
            if (movie != null)
            {
                var categories = _uow.CategoryRepository.GetCategoriesByIds(categoryIds);

                movie.Categories = categories.ToList();
            }
        }

        public void SetMetadata(Movie movie, List<int> metadataIds)
        {
            if (movie != null)
            {
                var metadatas = _uow.MetadataRepository.GetMetadatasByIds(metadataIds);

                var listOfGeneratedMetadata = _metadataHelper.GenerateMetadataList(metadatas, movie);

                movie.Metadata = JsonConvert.SerializeObject(listOfGeneratedMetadata);
            }
        }
    }
}
