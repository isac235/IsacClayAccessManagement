namespace Services
{
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Services.Interfaces;
    using Services.Mappers;

    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository claimRepository;

        public ClaimService(IClaimRepository claimRepository)
        {
            this.claimRepository = claimRepository;
        }

        public async Task<ClaimDto> CreateClaimAsync(ClaimDto claimDto)
        {
            var claim = claimDto.MapToClaimDomain();

            var claimResult = await this.claimRepository.AddClaimAsync(claim);

            return claimResult.MapToClaimDto();
        }
    }
}
