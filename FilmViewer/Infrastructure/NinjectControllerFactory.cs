using System;
using System.Web;
using Ninject;
using System.Web.Mvc;
using System.Web.Routing;
using FilmViewer.Business.Infrastructure;
using FilmViewer.DAL.Infrastructure;
using Ninject.Modules;

namespace FilmViewer.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel(GetNinjectModules());
            AddBindings();
        }

        private INinjectModule[] GetNinjectModules()
        {
            return new INinjectModule[]
            {
                new UnitOfWorkNinjectModule(), 
                new FilmViewerBusinessNinejctModule(), 
            };
        }

        protected override IController GetControllerInstance(RequestContext
        requestContext, Type controllerType)
        {
            return controllerType == null
            ? base.GetControllerInstance(requestContext, controllerType)
            : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
        }
    }
}