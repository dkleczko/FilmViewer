using System.Collections.Generic;
using System.IO;
using System.Linq;
using FilmViewer.Business.Abstract.Factory;
using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Actor;
using FilmViewer.Business.Enums;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Services
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDirectoryFactory _directoryFactory;
        public ActorService(IUnitOfWork uow, IDirectoryFactory directoryFactory)
        {
            _uow = uow;
            _directoryFactory = directoryFactory;
        }

        public bool VoteActor(VoteActorDto voteActor)
        {
            var actor = _uow.ActorRepository.Get(voteActor.ActorId);
            var user = _uow.UserRepository.GetApplicationUserWithVoteMoviePerson(voteActor.UserId);
            var earlierVotes = user.VoteMoviePerson.Where(p => p.MoviePerson.Id == actor.Id).ToList();
            foreach (var vote in earlierVotes)
            {
                actor.VoteScores -= vote.VoteScore;
                actor.VoteCount--;
                user.VoteMoviePerson.Remove(vote);
            }
            actor.VoteScores += voteActor.Score;
            actor.VoteCount++;
            user.VoteMoviePerson.Add(new VoteMoviePerson()
            {
                ApplicationUser = user,
                MoviePerson = actor,
                VoteScore = voteActor.Score
            });

            var result = _uow.Complete();
            return result > 0;
        }

        public int AddActor(ActorDetailsDto actorDto, string serverPath, string virtualPath)
        {
            var actor = BusinessMapper.Mapper.Map<Actor>(actorDto);
            actor.Folder = _directoryFactory.CreateDirectoryForMovie(serverPath, actor.Name, virtualPath);
            _uow.ActorRepository.Add(actor);
            _uow.Complete();

            return actor.Id;
        }

        public void AddPhotosToActor(ActorPhotosMoviesDto actorDto)
        {
            var actorEntity = _uow.ActorRepository.GetActorByIdWithPhotosMoviesVoteActor(actorDto.Id);
            if (actorEntity != null)
            {
                actorEntity.PhotoUrl = actorDto.PhotoUrl;
                actorEntity.PhotoUrls = BusinessMapper.Mapper.Map<List<PhotoPath>>(actorDto.Photos);
                _uow.Complete();
            }

        }

        public void DeleteActor(int actorId)
        {
            var actorEntity = _uow.ActorRepository.Get(actorId);
            var actorVotes = _uow.VoteMoviePersonRepository.GetAllVotesOfMoviePerson(actorId);

            _uow.ActorRepository.Remove(actorEntity);
            _uow.VoteMoviePersonRepository.RemoveRange(actorVotes);

            _uow.Complete();
        }

        public void DeletePhotoFromActor(string fileName, int actorId)
        {
            var actorPhotos = _uow.ActorRepository.GetActorByIdWithPhotosMoviesVoteActor(actorId);

            var photoToRemove = actorPhotos.PhotoUrls.Where(p => Path.GetFileName(p.Path) == fileName);

            _uow.PhotoPathRepository.RemoveRange(photoToRemove);

            _uow.Complete();
        }

        public void EditActor(ActorDetailsDto actorDto)
        {
            var actorEntity = _uow.ActorRepository.Get(actorDto.Id);

            actorEntity.Biography = actorDto.Biography;
            actorEntity.Name = actorDto.Name;
            actorEntity.BirthPlace = actorDto.BirthPlace;
            actorEntity.Birthdate = actorDto.Birthdate;
            actorEntity.MaritalStatus = actorDto.MaritalStatus;

            _uow.Complete();
        }
    }
}
