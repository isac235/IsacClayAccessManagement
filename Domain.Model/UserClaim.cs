namespace Domain.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserClaim
    {
        [Key]
        public Guid UserClaimID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public Guid ClaimID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("ClaimID")]
        public Claim Claim { get; set; }
    }
}
