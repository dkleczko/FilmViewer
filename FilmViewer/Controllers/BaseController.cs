using System.Web;
using System.Web.Mvc;
using FilmViewer.DAL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FilmViewer.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUser _applicationUser;

        protected ApplicationUser ApplicationUser
        {
            get
            {
                if (_applicationUser == null && User.Identity != null)
                {
                    var usrId = User.Identity.GetUserId();

                    _applicationUser = UserManager.FindById(usrId);

                }
                return _applicationUser;
            }
            private set { _applicationUser = value; }
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}