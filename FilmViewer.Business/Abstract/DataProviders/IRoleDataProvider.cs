using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Abstract.DataProviders
{
    public interface IRoleDataProvider
    {
        List<RoleDto> GetAllRoles();
        RoleDto GetRoleById(string roleId);
    }
}
