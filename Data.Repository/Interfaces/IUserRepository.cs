namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Model;

    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(Guid userId);

        Task<User> GetUserByUserNameAndOffice(string userName, Guid officeId);

        Task<User> CreateUserAsync(User newUser);
    }
}
