namespace DTO
{
    using System;

    public class User
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }
    }
}
