using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using FilmViewer.DAL.Model;

namespace FilmViewer.Models.AdminModels
{
    public class UserRoleViewModel
    {
        public ApplicationUser user { get; set; }
        public List<IdentityRole> roles { get; set; }
    }
    public class UserSetRoleViewModel
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public IEnumerable<SelectListItem> allRoles { get; set; }
    }
}