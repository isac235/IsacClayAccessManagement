namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Model;

    public interface IUserRoleMappingRepository
    {
        Task<List<UserRoleMapping>> GetUserRoleMappingByUserIdAsync(Guid userId);

        Task<UserRoleMapping> CreateUserRolesAsync(UserRoleMapping newUserRoleMapping);
    }
}
