using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Actor;
using FilmViewer.Business.Enums;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Abstract.DataProviders
{
    public interface IActorDataProvider
    {
        List<ActorCurrentUserVoteDto> GetActorBySearchString(string searchString, ApplicationUser user);
        List<ActorDetailsDto> GetActorBySearchString(string searchString, SortParam sortParam, SortOrder sortOrder);
        ActorPhotosMoviesDto GetActorDetailsPhotosAndMovies(int id, ApplicationUser user = null);
        ActorDetailsDto GetActorDetails(int id);
        CurrentActorVoteDto GetCurrentActorVote(int actorId);
        List<ActorDto> GetTop100VotedActors();
        int GetAllActorsCount();
    }
}
