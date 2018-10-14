using System.Collections.Generic;
using System.Linq;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;

namespace FilmViewer.Business.DataProviders
{
    public class RoleDataProvider : IRoleDataProvider
    {
        private readonly IUnitOfWork _uow;
        public RoleDataProvider(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        public List<RoleDto> GetAllRoles()
        {
            var roleEntities = _uow.RoleRepository.GetAll();

            return BusinessMapper.Mapper.Map<List<RoleDto>>(roleEntities);
        }

        public RoleDto GetRoleById(string roleId)
        {
            var roleEntities = _uow.RoleRepository.GetAll();

            var roleEntity = roleEntities.FirstOrDefault(p => p.Id == roleId);

            return BusinessMapper.Mapper.Map<RoleDto>(roleEntity);

        }
    }
}
