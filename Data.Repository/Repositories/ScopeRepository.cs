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

    public class ScopeRepository : IScopeRepository
    {
        private readonly OfficesAccessDbContext dbContext;
        private readonly ILogger<ScopeRepository> logger;

        public ScopeRepository(OfficesAccessDbContext dbContext, ILogger<ScopeRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
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
                this.logger.LogError(ex, "Error occurred while creating scope");
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
                this.logger.LogError(ex, "Error occurred while getting scopes by doorId");
                throw new RepositoryException(nameof(GetScopesByDoorIdAsync), ex.Message, ex);
            }
        }
    }
}
