using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;
using FilmViewer.Localization;
using FilmViewer.Models;
using FilmViewer.Models.Admin;
using FilmViewer.Roles;
using FilmViewer.Statics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace FilmViewer.Controllers
{
    [Authorize(Roles = CustomRoles.Admin)]
    public class AdminController : BaseController
    {
        private IUserDataProvider _userDataProvider;
        private IRoleDataProvider _roleDataProvider;
        public AdminController(IUserDataProvider userDataProvider, IRoleDataProvider roleDataProvider)
        {
            _userDataProvider = userDataProvider;
            _roleDataProvider = roleDataProvider;
        }

        [HttpGet]
        public ActionResult Index(string searchString, int? page)
        {
            var usersDto = _userDataProvider.SearchUsersByUsername(searchString);

            ViewBag.UserFilter = searchString;

            var pageSize = 10;
            var pageNumber = (page ?? 1);
            var viewModel = new IndexViewModel()
            {
                Users = usersDto.ToPagedList(pageNumber, pageSize)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ViewResult ManageRoles(string id)
        {
            var appUserDto = _userDataProvider.GetApplicationUserDetailsById(id);
            var rolesDto = _roleDataProvider.GetAllRoles();

            var viewModel = new ManageRolesViewModel()
            {
                AppUser = appUserDto,
                AllRoles = new SelectList(rolesDto, "RoleId", "RoleName")
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRoleToUserAsync(string roleId, string userId)
        {
            var roleDto = _roleDataProvider.GetRoleById(roleId);
            if (roleDto != null)
            {
                var result = await UserManager.AddToRoleAsync(userId, roleDto.RoleName);
                switch (result.Succeeded)
                {
                    case true:
                        TempData[UserMessages.UserMessage] = new MessageViewModel()
                        {
                            CssClass = "alert-success",
                            Message = Resources.RoleAddedToUser
                        };
                        break;

                    case false:
                        TempData[UserMessages.UserMessage] = new MessageViewModel()
                        {
                            CssClass = "alert-danger",
                            Message = Resources.UnknownError
                        };
                        break;
                }
            }
            else
            {
                TempData[UserMessages.UserMessage] = new MessageViewModel()
                {
                    CssClass = "alert-danger",
                    Message = Resources.UnknownError
                };
            }
            return RedirectToAction("ManageRoles", "Admin", new { id = userId});

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleFromUserAsync(string roleId, string userId)
        {
            var roleDto = _roleDataProvider.GetRoleById(roleId);
            if (roleDto != null)
            {
                var result = await UserManager.RemoveFromRoleAsync(userId, roleDto.RoleName);
                switch (result.Succeeded)
                {
                    case true:
                        TempData[UserMessages.UserMessage] = new MessageViewModel()
                        {
                            CssClass = "alert-success",
                            Message = Resources.RoleRemovedFromUser
                        };
                        break;

                    case false:
                        TempData[UserMessages.UserMessage] = new MessageViewModel()
                        {
                            CssClass = "alert-danger",
                            Message = Resources.UnknownError
                        };
                        break;
                }
            }
            else
            {
                TempData[UserMessages.UserMessage] = new MessageViewModel()
                {
                    CssClass = "alert-danger",
                    Message = Resources.UnknownError
                };
            }
            return RedirectToAction("ManageRoles", "Admin", new { id = userId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BlockAccount(string id)
        {
            var setLockoutResult = await UserManager.SetLockoutEnabledAsync(id, true);
            if (setLockoutResult.Succeeded)
            {
                var result = await UserManager.SetLockoutEndDateAsync(id, DateTimeOffset.MaxValue);
                switch (result.Succeeded)
                {
                    case true:
                        TempData[UserMessages.UserMessage] = new MessageViewModel()
                        {
                            CssClass = "alert-success",
                            Message = Resources.UserLocked
                        };
                        break;

                    case false:
                        TempData[UserMessages.UserMessage] = new MessageViewModel()
                        {
                            CssClass = "alert-danger",
                            Message = Resources.UnknownError
                        };
                        break;

                }
            }
            else
            {
                TempData[UserMessages.UserMessage] = new MessageViewModel()
                {
                    CssClass = "alert-danger",
                    Message = Resources.UnknownError
                };
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UnblockAccount(string id)
        {
            var result = await UserManager.SetLockoutEnabledAsync(id, false);
            if (result.Succeeded)
            {
                TempData[UserMessages.UserMessage] = new MessageViewModel()
                {
                    CssClass = "alert-success",
                    Message = Resources.UserUnlocked
                };
            }
            else
            {
                TempData[UserMessages.UserMessage] = new MessageViewModel()
                {
                    CssClass = "alert-danger",
                    Message = Resources.UnknownError
                };
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<bool> CreateAdmin()
        {
            RoleManager<IdentityRole> rm= new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new FilmViewerDbContext()));
            var str = rm.Create(new IdentityRole(CustomRoles.Admin));
            var result = await UserManager.CreateAsync(new ApplicationUser()
            {
                UserName = "Administrator"
            }, "Administrator");
            await UserManager.AddToRoleAsync(UserManager.FindByName("Administrator").Id, CustomRoles.Admin);
            return true;
        }

    }
}