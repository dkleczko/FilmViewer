using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Abstract.Factory;
using FilmViewer.Business.Abstract.Helpers;
using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.DataProviders;
using FilmViewer.Business.Factory;
using FilmViewer.Business.Helpers;
using FilmViewer.Business.Services;
using Ninject.Modules;

namespace FilmViewer.Business.Infrastructure
{
    public class FilmViewerBusinessNinejctModule : NinjectModule
    {
        public override void Load()
        {
            //DataProviders
            Bind<IMovieDataProvider>().To<MovieDataProvider>();
            Bind<IMainMovieDataProvider>().To<MainMovieDataProvider>();
            Bind<IActorDataProvider>().To<ActorDataProvider>();
            Bind<ICommentDataProvider>().To<CommentDataProvider>();
            Bind<IUserDataProvider>().To<UserDataProvider>();
            Bind<ICategoryDataProvider>().To<CategoryDataProvider>();
            Bind<IMetadataDataProvider>().To<MetadataDataProvider>();
            Bind<IDirectorDataProvider>().To<DirectorDataProvider>();
            Bind<IRoleDataProvider>().To<RoleDataProvider>();
            //Services
            Bind<IActorService>().To<ActorService>();
            Bind<IEmailService>().To<EmailService>();
            Bind<IMovieService>().To<MovieService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IMetadataService>().To<MetadataService>();
            Bind<IMainMovieService>().To<MainMovieService>();
            Bind<IDirectorService>().To<DirectorService>();
            //Factories
            Bind<IMovieFactory>().To<MovieFactory>();
            Bind<IDirectoryFactory>().To<DirectoryFactory>();
            //Helpers
            Bind<IMetadataHelper>().To<MetadataHelper>();


        }
    }
}
