namespace Data.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Domain.Model;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class UserRepository : IUserRepository
    {
        private readonly OfficesAccessDbContext dbContext;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(OfficesAccessDbContext dbContext, ILogger<UserRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await this.dbContext.Users.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting all users");
                throw new RepositoryException(nameof(GetAllUsersAsync), ex.Message, ex);
            }
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            try
            {
                return await this.dbContext.Users.FindAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting user by id");
                throw new RepositoryException(nameof(GetUserByIdAsync), ex.Message, ex);
            }
        }

        public async Task<User> GetUserByUserNameAndOffice(string userName, Guid officeId)
        {
            try
            {
                var user = await this.dbContext.Users
                    .FirstOrDefaultAsync(u => u.Username == userName && u.OfficeID == officeId)
                    .ConfigureAwait(false);

                return user;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting user by user name and office");
                throw new RepositoryException(nameof(GetUserByUserNameAndOffice), ex.Message, ex);
            }
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            try
            {
                await this.dbContext.Users.AddAsync(newUser).ConfigureAwait(false);
                await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

                return newUser;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while creating user");
                throw new RepositoryException(nameof(CreateUserAsync), ex.Message, ex);
            }
        }
    }
}
