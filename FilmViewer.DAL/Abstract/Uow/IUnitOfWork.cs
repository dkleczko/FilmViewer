using System;
using FilmViewer.DAL.Abstract.Repository;

namespace FilmViewer.DAL.Abstract.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        IActorRepository ActorRepository { get; }
        IMovieRepository MovieRepository { get; }
        IUserRepository UserRepository { get; }
        IMainMovieRepository MainMovieRepository { get; }
        ICommentRepository CommentRepository { get; }
        IVoteMovieRepository VoteMovieRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IDirectorRepository DirectorRepository { get; }
        IMetadataRepository MetadataRepository { get; }
        IVoteMoviePersonRepository VoteMoviePersonRepository { get; }
        IPhotoPathRepository PhotoPathRepository { get; }
        IRoleRepository RoleRepository { get; }
    }
}
