namespace Services.Mappers
{
    using Domain.Model;
    using DTO;

    public static class ClaimMapper
    {
        public static ClaimDto MapToClaimDto(this Claim claim)
        {
            if (claim == null)
            {
                return null;
            }

            return new ClaimDto
            {
                ClaimID = claim.ClaimID,
                ClaimName = claim.ClaimName
            };
        }

        public static Claim MapToClaimDomain(this ClaimDto claimDto)
        {
            if (claimDto == null)
            {
                return null;
            }

            return new Claim
            {
                ClaimID = claimDto.ClaimID,
                ClaimName = claimDto.ClaimName
            };
        }
    }
}
