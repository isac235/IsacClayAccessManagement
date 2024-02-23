namespace Domain.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserRoleMapping
    {
        [Key]
        public Guid UserRoleID { get; set; }

        public Guid UserID { get; set; }

        public Guid RoleID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; set; }
    }
}
