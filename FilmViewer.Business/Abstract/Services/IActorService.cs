using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Actor;

namespace FilmViewer.Business.Abstract.Services
{
    public interface IActorService
    {
        bool VoteActor(VoteActorDto voteActor);
        int AddActor(ActorDetailsDto actor, string serverPath, string virtualPath);
        void AddPhotosToActor(ActorPhotosMoviesDto actorDto);
        void DeleteActor(int actorId);
        void DeletePhotoFromActor(string fileName, int actorId);
        void EditActor(ActorDetailsDto actor);
    }
}
