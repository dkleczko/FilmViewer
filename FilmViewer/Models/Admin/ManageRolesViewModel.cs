using System.Collections.Generic;
using System.Web.Mvc;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Models.Admin
{
    public class ManageRolesViewModel
    {
        public ApplicationUserDetailsDto AppUser { get; set; }
        public SelectList AllRoles { get; set; }
    }
}