namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Model;

    public interface IClaimRepository
    {
        Task<Claim> GetClaimByIdAsync(Guid claimId);

        Task<Claim> AddClaimAsync(Claim claim);
    }
}
