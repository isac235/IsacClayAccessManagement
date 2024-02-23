namespace Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTO;

    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(Guid userId);

        Task<User> CreateUserAsync(User newUserDTO, Guid officeId);

        Task<User> ValidateUserCredentialsAsync(UserLoginRequest loginRequest, Guid officeId);
    }
}
