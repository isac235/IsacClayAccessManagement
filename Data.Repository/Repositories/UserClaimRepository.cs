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

    public class UserClaimRepository : IUserClaimRepository
    {
        private readonly OfficesAccessDbContext dbContext;

        public UserClaimRepository(OfficesAccessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<UserClaim>> GetUserClaimByUserIdAsync(Guid userId)
        {
            try
            {
                return await this.dbContext.UserClaims.Where(uc => uc.UserID == userId).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
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
                throw new RepositoryException(nameof(AddUserClaimAsync), ex.Message, ex);
            }
        }
    }
}
