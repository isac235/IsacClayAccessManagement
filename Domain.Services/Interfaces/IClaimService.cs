namespace Services.Interfaces
{
    using System.Threading.Tasks;
    using DTO;

    public interface IClaimService
    {
        Task<ClaimDto> CreateClaimAsync(ClaimDto claim);
    }
}
