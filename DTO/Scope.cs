namespace DTO
{
    using System;

    public class Scope
    {
        public Guid ScopeId { get; set; }

        public string ScopeName { get; set; }

        public string Description { get; set; }

        public Guid RoleId { get; set; }
    }
}
