namespace Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DTO;
    public interface IUserClaimService
    {
        Task<UserClaim> AssociateUserWithClaimAsync(Guid userId, Guid claimId);

        Task<List<string>> GetUserClaimsByUserIdAsync(Guid userId);
    }
}
