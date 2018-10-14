using System;
using System.Collections.Generic;

namespace FilmViewer.Business.Dto.Domain
{
    public class ApplicationUserDetailsDto : ApplicationUserDto
    {
        public bool IsLockedOutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
