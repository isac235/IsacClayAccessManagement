namespace Data.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Domain.Model;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.EntityFrameworkCore;

    public class ClaimRepository : IClaimRepository
    {
        private readonly OfficesAccessDbContext dbContext;

        public ClaimRepository(OfficesAccessDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<Claim> GetClaimByIdAsync(Guid claimId)
        {
            try
            {
                return await this.dbContext.Claims.FirstOrDefaultAsync(c => c.ClaimID == claimId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
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
                throw new RepositoryException(nameof(AddClaimAsync), ex.Message, ex);
            }
        }
    }
}
