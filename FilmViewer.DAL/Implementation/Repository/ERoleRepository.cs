using FilmViewer.DAL.Abstract.Repository;
using FilmViewer.DAL.Context;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmViewer.DAL.Implementation.Repository
{
    internal class ERoleRepository : ERepository<IdentityRole>, IRoleRepository
    {
        public ERoleRepository(FilmViewerDbContext context) : base(context)
        {
        }

        protected FilmViewerDbContext FilmViewerDbContext
        {
            get { return Context as FilmViewerDbContext; }
        }

    }
}
