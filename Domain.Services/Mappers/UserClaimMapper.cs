namespace Services.Mappers
{
    using DTO;

    public static class UserClaimMapper
    {
        public static UserClaim MapUserClaimToDto(this Domain.Model.UserClaim userClaim)
        {
            if (userClaim == null)
            {
                return null;
            }

            return new UserClaim
            {
                UserClaimID = userClaim.UserClaimID,
                UserID = userClaim.UserID,
                ClaimID = userClaim.ClaimID
            };
        }

        public static Domain.Model.UserClaim MapUserClaimToDomain(this UserClaim userClaimDto)
        {
            if (userClaimDto == null)
            {
                return null;
            }

            return new Domain.Model.UserClaim
            {
                UserClaimID = userClaimDto.UserClaimID,
                UserID = userClaimDto.UserID,
                ClaimID = userClaimDto.ClaimID
            };
        }
    }
}
