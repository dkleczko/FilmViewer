using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Implementation.Uow;
using Ninject.Modules;
using Ninject.Web.Common;

namespace FilmViewer.DAL.Infrastructure
{
    public class UnitOfWorkNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>()
                .InRequestScope();
        }
    }
}
