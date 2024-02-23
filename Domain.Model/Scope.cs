namespace Domain.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Scope
    {
        [Key]
        public Guid ScopeID { get; set; }

        [Required]
        [StringLength(255)]
        public string ScopeName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public Guid DoorID { get; set; }

        public Guid RoleID { get; set; }

        [ForeignKey("DoorID")]
        public Door Door { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; set; }
    }
}
