using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class EPhotoPathRepository : ERepository<PhotoPath>, IPhotoPathRepository
    {
        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }
        public EPhotoPathRepository(FilmViewerDbContext context) : base(context)
        {
        }
    }
}
