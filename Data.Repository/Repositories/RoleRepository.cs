namespace Data.Repository.Repositories
{
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Domain.Model;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.Extensions.Logging;

    public class RoleRepository : IRoleRepository
    {
        private readonly OfficesAccessDbContext dbContext;
        private readonly ILogger<RoleRepository> logger;

        public RoleRepository(OfficesAccessDbContext dbContext, ILogger<RoleRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<Role> CreateRoleAsync(Role newRole)
        {
            try
            {
                await this.dbContext.Roles.AddAsync(newRole).ConfigureAwait(false);
                await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

                return newRole;
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while creating role.");
                throw new RepositoryException(nameof(CreateRoleAsync), ex.Message, ex);
            }

        }
    }
}
