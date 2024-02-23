namespace Data.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Domain.Model;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class ClaimRepository : IClaimRepository
    {
        private readonly OfficesAccessDbContext dbContext;
        private readonly ILogger<ClaimRepository> logger;

        public ClaimRepository(OfficesAccessDbContext context, ILogger<ClaimRepository> logger)
        {
            this.dbContext = context;
            this.logger = logger;
        }

        public async Task<Claim> GetClaimByIdAsync(Guid claimId)
        {
            try
            {
                return await this.dbContext.Claims.FirstOrDefaultAsync(c => c.ClaimID == claimId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting claim by id");
                throw new RepositoryException(nameof(GetClaimByIdAsync), ex.Message, ex);
            }
        }

        public async Task<Claim> AddClaimAsync(Claim claim)
        {
            try
            {
                await this.dbContext.Claims.AddAsync(claim).ConfigureAwait(false);
                await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

                return claim;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while adding claim");
                throw new RepositoryException(nameof(AddClaimAsync), ex.Message, ex);
            }
        }
    }
}
