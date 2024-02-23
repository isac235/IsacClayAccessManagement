namespace Data.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Domain.Model;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class UserRoleMappingRepository : IUserRoleMappingRepository
    {
        private readonly OfficesAccessDbContext dbContext;
        private readonly ILogger<UserRoleMappingRepository> logger;

        public UserRoleMappingRepository(OfficesAccessDbContext dbContext, ILogger<UserRoleMappingRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<List<UserRoleMapping>> GetUserRoleMappingByUserIdAsync(Guid userId)
        {
            try
            {
                var userRoleMappings = await dbContext.UserRoleMappings
                    .Where(mapping => mapping.UserID == userId)
                    .ToListAsync().ConfigureAwait(false);

                return userRoleMappings;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting userRoleMapping by userId: {UserId}", userId);

                throw new RepositoryException(nameof(GetUserRoleMappingByUserIdAsync), ex.Message, ex);
            }
        }

        public async Task<UserRoleMapping> CreateUserRolesAsync(UserRoleMapping newUserRoleMapping)
        {
            try
            {
                await this.dbContext.UserRoleMappings.AddAsync(newUserRoleMapping).ConfigureAwait(false);
                await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

                return newUserRoleMapping;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while creating userRoles");

                throw new RepositoryException(nameof(CreateUserRolesAsync), ex.Message, ex);
            }
        }
    }
}
