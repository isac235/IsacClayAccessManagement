namespace Data.Repository.Repositories
{
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Domain.Model;
    using Infrastructure.CrossCutting.CustomExceptions;

    public class RoleRepository : IRoleRepository
    {
        private readonly OfficesAccessDbContext dbContext;

        public RoleRepository(OfficesAccessDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                throw new RepositoryException(nameof(CreateRoleAsync), ex.Message, ex);
            }

        }
    }
}
