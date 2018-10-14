using System.Collections.Generic;
using System.Linq;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Actor;
using FilmViewer.Business.Enums;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.DataProviders
{
    public class ActorDataProvider : IActorDataProvider
    {
        private readonly IUnitOfWork _uow;
        public ActorDataProvider(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public List<ActorCurrentUserVoteDto> GetActorBySearchString(string searchString, ApplicationUser user)
        {
            var actorsDtoList= new List<ActorCurrentUserVoteDto>();
            var actorsList = _uow.ActorRepository.GetActorsBySearchString(searchString).ToList();
            foreach (var act in actorsList)
            {
                var actorDto = BusinessMapper.Mapper.Map<Actor, ActorCurrentUserVoteDto>(act);
                if (user != null)
                {
                    var userVote = act.Votes.FirstOrDefault(p => p.ApplicationUser != null && p.ApplicationUser.Id == user.Id);
                    if (userVote != null)
                    {
                        actorDto.CurrentUserVote = userVote.VoteScore;
                        actorDto.HasUserVoteActor = true;
                    }
                }
                actorsDtoList.Add(actorDto);
            }
            return actorsDtoList;

        }

        public List<ActorDetailsDto> GetActorBySearchString(string searchString, SortParam sortParam, SortOrder sortOrder)
        {
            var actorsList = _uow.ActorRepository.GetActorsBySearchString(searchString);
            switch (sortParam)
            {
                case SortParam.Name:
                    actorsList = sortOrder == SortOrder.Asc ? actorsList.OrderBy(p => p.Name) : actorsList.OrderByDescending(p => p.Name);
                    break;
                case SortParam.Date:
                    actorsList = sortOrder == SortOrder.Asc
                        ? actorsList.OrderBy(p => p.Birthdate)
                        : actorsList.OrderByDescending(p => p.Birthdate);
                    break;
            }

            return BusinessMapper.Mapper.Map<List<ActorDetailsDto>>(actorsList);
        }


        public ActorPhotosMoviesDto GetActorDetailsPhotosAndMovies(int id, ApplicationUser user = null)
        {
            var actor = _uow.ActorRepository.GetActorByIdWithPhotosMoviesVoteActor(id);

            var actorDto =  BusinessMapper.Mapper.Map<Actor, ActorPhotosMoviesDto>(actor);

            if (user != null)
            {
                var userVote = actor.Votes.FirstOrDefault(p => p.ApplicationUser.Id == user.Id);
                if (userVote != null)
                {
                    actorDto.CurrentUserVote = userVote.VoteScore;
                    actorDto.HasUserVoteActor = true;
                }
            }
            return actorDto;
        }

        public ActorDetailsDto GetActorDetails(int id)
        {
            var actor = _uow.ActorRepository.Get(id);

            return BusinessMapper.Mapper.Map<ActorDetailsDto>(actor);
        }

        public CurrentActorVoteDto GetCurrentActorVote(int actorId)
        {
            var actor = _uow.ActorRepository.Get(actorId);

            return BusinessMapper.Mapper.Map<Actor, CurrentActorVoteDto>(actor);
        }

        public List<ActorDto> GetTop100VotedActors()
        {
            var actors = _uow.ActorRepository.GetTop100VotedActors();
            return BusinessMapper.Mapper.Map<IEnumerable<Actor>, List<ActorDto>>(actors);
        }

        public int GetAllActorsCount()
        {
            var count = _uow.ActorRepository.GetAll().Count();
            return count;
        }
    }
}
