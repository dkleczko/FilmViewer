using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FilmViewer.Localization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using FilmViewer.Models;
using FilmViewer.Statics;

namespace FilmViewer.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }
        
        
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            switch (message)
            {
                case ManageMessageId.ChangePasswordSuccess:
                    TempData[UserMessages.UserMessage] = new MessageViewModel()
                    {
                        Message = Resources.PasswordChangedSuccesfully,
                        CssClass = "alert-success"
                    };
                    break;
                case ManageMessageId.Error:
                    TempData[UserMessages.UserMessage] = new MessageViewModel()
                    {
                        Message = Resources.UnknownError,
                        CssClass = "alert-danger"
                    };
                    break;
            }
            return View();
        }
    
        public ActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }
        
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

    }
}