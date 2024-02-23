namespace Services
{
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Services.Interfaces;
    using Services.Mappers;

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository) 
        {
            this.roleRepository = roleRepository;
        }

        public async Task<Role> CreateRoleAsync(Role role) 
        {
            var roleDomain =  await this.roleRepository.CreateRoleAsync(role.MapToRoleDomain());

            return roleDomain.MapToRoleDto();
        }
    }
}
