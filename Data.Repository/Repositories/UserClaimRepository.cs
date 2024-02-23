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

    public class UserClaimRepository : IUserClaimRepository
    {
        private readonly OfficesAccessDbContext dbContext;
        private readonly ILogger<UserClaimRepository> logger;

        public UserClaimRepository(OfficesAccessDbContext dbContext, ILogger<UserClaimRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<List<UserClaim>> GetUserClaimByUserIdAsync(Guid userId)
        {
            try
            {
                return await this.dbContext.UserClaims.Where(uc => uc.UserID == userId).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting userClaims by userId");
                throw new RepositoryException(nameof(GetUserClaimByUserIdAsync), ex.Message, ex);
            }
        }

        public async Task<UserClaim> AddUserClaimAsync(UserClaim userClaim)
        {
            try
            {
                await this.dbContext.UserClaims.AddAsync(userClaim).ConfigureAwait(false);
                await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

                return userClaim;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while adding userClaims.");
                throw new RepositoryException(nameof(AddUserClaimAsync), ex.Message, ex);
            }
        }
    }
}
