using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmViewer.DAL.Abstract.Repository
{
    public interface IRoleRepository : IRepository<IdentityRole>
    {
    }
}
