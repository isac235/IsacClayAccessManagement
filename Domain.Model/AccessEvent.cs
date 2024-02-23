namespace Domain.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AccessEvent
    {
        [Key]
        public Guid EventID { get; set; }

        public Guid UserID { get; set; }

        public Guid DoorID { get; set; }

        public DateTime EventTime { get; set; }

        [Required]
        [StringLength(50)]
        public string AccessMethod { get; set; }

        [Required]
        [StringLength(50)]
        public string AccessResult { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("DoorID")]
        public Door Door { get; set; }
    }
}
