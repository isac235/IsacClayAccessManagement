namespace DTO
{
    using System;

    public class UserClaim
    {
        public Guid UserClaimID { get; set; }

        public Guid UserID { get; set; }

        public Guid ClaimID { get; set; }
    }
}
