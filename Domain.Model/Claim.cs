namespace Domain.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Claim
    {
        [Key]
        public Guid ClaimID { get; set; }

        [Required]
        [StringLength(50)]
        public string ClaimName { get; set; }
    }
}
