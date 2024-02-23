namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Model;

    public interface IUserClaimRepository
    {
        Task<List<UserClaim>> GetUserClaimByUserIdAsync(Guid userId);

        Task<UserClaim> AddUserClaimAsync(UserClaim userClaim);
    }
}
