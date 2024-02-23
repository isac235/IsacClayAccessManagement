namespace Data.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Domain.Model;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private readonly OfficesAccessDbContext dbContext;

        public UserRepository(OfficesAccessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await this.dbContext.Users.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
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

                throw new RepositoryException(nameof(CreateUserAsync), ex.Message, ex);
            }
        }
    }
}
