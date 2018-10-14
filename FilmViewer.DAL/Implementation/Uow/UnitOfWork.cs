using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Implementation.Repository;

namespace FilmViewer.DAL.Implementation.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FilmViewerDbContext _context;
        public IActorRepository ActorRepository { get; }
        public IMovieRepository MovieRepository { get; }
        public IUserRepository UserRepository { get; }
        public IMainMovieRepository MainMovieRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public IVoteMovieRepository VoteMovieRepository { get; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IDirectorRepository DirectorRepository { get; set; }
        public IMetadataRepository MetadataRepository { get; set; }
        public IVoteMoviePersonRepository VoteMoviePersonRepository { get; }
        public IPhotoPathRepository PhotoPathRepository { get; }
        public IRoleRepository RoleRepository { get; }

        public UnitOfWork()
        {
            _context = FilmViewerDbContext.Create();
            ActorRepository = new EActorRepository(_context);
            MovieRepository = new EMovieRepository(_context);
            UserRepository = new EUserRepository(_context);
            MainMovieRepository = new EMainMovieRepository(_context);
            CommentRepository = new ECommentRepository(_context);
            VoteMovieRepository = new EVoteMovieRepository(_context);
            CategoryRepository = new ECategoryRepository(_context);
            DirectorRepository = new EDirectorRepository(_context);
            MetadataRepository = new EMetadataRepository(_context);
            VoteMoviePersonRepository = new EVoteMoviePersonRepository(_context);
            PhotoPathRepository = new EPhotoPathRepository(_context);
            RoleRepository = new ERoleRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }


    }
}
