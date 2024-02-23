namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Services.Interfaces;
    using Services.Mappers;

    public class UserClaimService : IUserClaimService
    {
        private readonly IUserClaimRepository userClaimRepository;
        private readonly IClaimRepository claimRepository;

        public UserClaimService(IUserClaimRepository userClaimRepository, IClaimRepository claimRepository)
        {
            this.userClaimRepository = userClaimRepository;
            this.claimRepository = claimRepository;
        }

        public async Task<List<string>> GetUserClaimsByUserIdAsync(Guid userId)
        {
            var userClaims = await this.userClaimRepository.GetUserClaimByUserIdAsync(userId).ConfigureAwait(false);
            var result = new List<String>();

            foreach (var userClaim in userClaims)
            {
                var claim = await this.claimRepository.GetClaimByIdAsync(userClaim.ClaimID);

                result.Add(claim.ClaimName);
            }

            return result;
        }

        public async Task<UserClaim> AssociateUserWithClaimAsync(Guid userId, Guid claimId)
        {
            var userClaim = new Domain.Model.UserClaim
            {
                UserID = userId,
                ClaimID = claimId
            };

            var result = await this.userClaimRepository.AddUserClaimAsync(userClaim);

            return result.MapUserClaimToDto();
        }
    }
}
