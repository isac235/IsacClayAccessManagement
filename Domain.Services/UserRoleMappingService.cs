namespace Services
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Services.Interfaces;
    using Services.Mappers;

    public class UserRoleMappingService : IUserRoleMappingService
    {
        private readonly IUserRoleMappingRepository userRoleMappingRepository;

        private readonly IUserRepository userRepository;

        public UserRoleMappingService(IUserRoleMappingRepository userRoleMappingRepository, IUserRepository userRepository)
        {
            this.userRoleMappingRepository = userRoleMappingRepository;
            this.userRepository = userRepository;
        }

        public async Task<UserRoleMapping> CreateUserRoleMappingAsync(UserRoleMapping userRoleMapping, Guid officeId)
        {
            var user = await this.userRepository.GetUserByIdAsync(userRoleMapping.UserId).ConfigureAwait(false);

            if (user == null)
            {
                throw new ValidationException("The user for whom you are create the role doesn't exist");
            }
            if (user.OfficeID != officeId)
            {
                throw new ValidationException("The user for whom you are creating the role doesn't belong to this office");
            }

            var result = await this.userRoleMappingRepository.CreateUserRolesAsync(userRoleMapping.MapToUserRoleMappingDomain()).ConfigureAwait(false);

            return result.MapToUserRoleMappingDto();
        }
    }
}
