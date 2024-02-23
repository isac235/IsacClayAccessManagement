namespace Domain.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Door
    {
        [Key]
        public Guid DoorID { get; set; }

        [Required]
        [StringLength(255)]
        public string DoorName { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        public Guid OfficeID { get; set; }

        [ForeignKey("OfficeID")]
        public Office Office { get; set; }
    }
}
