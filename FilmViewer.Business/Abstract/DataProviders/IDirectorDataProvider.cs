using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Director;
using FilmViewer.Business.Enums;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Abstract.DataProviders
{
    public interface IDirectorDataProvider
    {
        List<DirectorDto> GetDirectorsBySearchString(string searchString);
        DirectorDto GetDirectorDtoById(int id);

        List<DirectorDetailsDto> GetDirectorBySearchString(string searchString, SortParam sortParam,
            SortOrder sortOrder);

        DirectorDetailsPhotosDto GetDirectorWithPhotosById(int directorId);
        List<DirectorCurrentUserVoteDto> GetDirectorBySearchString(string searchString, ApplicationUser applicationUser = null);
        DirectorPhotosMoviesDto GetDirectorDetailsPhotosAndMovies(int id, ApplicationUser applicationUser = null);
        CurrentDirectorVoteDto GetCurrentDirectorVote(int directorId);
        List<DirectorDto> GetTop100VotedDirectors();
    }
}
