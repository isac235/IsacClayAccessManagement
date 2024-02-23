namespace Domain.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Office
    {
        [Key]
        public Guid OfficeID { get; set; }

        [Required]
        [StringLength(255)]
        public string OfficeName { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }
    }
}
