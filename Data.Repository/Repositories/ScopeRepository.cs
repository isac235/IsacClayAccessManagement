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

    public class ScopeRepository : IScopeRepository
    {
        private readonly OfficesAccessDbContext dbContext;

        public ScopeRepository(OfficesAccessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Scope> CreateScopeAsync(Scope scope)
        {
            try
            {
                await this.dbContext.Scopes.AddAsync(scope).ConfigureAwait(false);
                await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

                return scope;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(nameof(CreateScopeAsync), ex.Message, ex);
            }
        }

        public async Task<List<Scope>> GetScopesByDoorIdAsync(Guid doorId)
        {
            try
            {
                var scopes = await this.dbContext.Scopes.Where(x => x.DoorID == doorId).ToListAsync().ConfigureAwait(false);

                return scopes;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(nameof(GetScopesByDoorIdAsync), ex.Message, ex);
            }
        }
    }
}
