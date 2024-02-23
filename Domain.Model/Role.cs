namespace Domain.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Role
    {
        [Key]
        public Guid RoleID { get; set; }

        [Required]
        [StringLength(255)]
        public string RoleName { get; set; }
    }
}
