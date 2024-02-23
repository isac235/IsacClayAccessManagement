namespace Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using DTO;

    public interface IUserRoleMappingService
    {
        Task<UserRoleMapping> CreateUserRoleMappingAsync(UserRoleMapping userRoleMapping, Guid officeId);
    }
}
