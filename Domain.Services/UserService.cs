namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Microsoft.AspNetCore.Identity;
    using Services.Interfaces;
    using Services.Mappers;

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var result = await this.userRepository.GetAllUsersAsync().ConfigureAwait(false);

            return result.MapUsersToDto();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await this.userRepository.GetUserByIdAsync(userId);
            return user.MapToUserDto();
        }

        public async Task<User> CreateUserAsync(User newUserDTO, Guid officeId)
        {
            var newUser = newUserDTO.MapUserToDomain();
            newUser.OfficeID = officeId;
            newUser.PasswordHash = HashPassword(newUserDTO.Password);

            var createdUser = await this.userRepository.CreateUserAsync(newUser);

            return createdUser.MapToUserDto();
        }

        public async Task<User> ValidateUserCredentialsAsync(UserLoginRequest loginRequest, Guid officeId)
        {
            var user = await this.userRepository.GetUserByUserNameAndOffice(loginRequest.Username, officeId);

            if (user == null)
            {
                return null;
            }

            var validation = VerifyHashedPassword(user.PasswordHash, loginRequest.Password);

            if (validation is PasswordVerificationResult.Failed)
            {
                return null;
            }

            return user.MapToUserDto();
        }

        private static string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(null, password);
        }

        private static PasswordVerificationResult VerifyHashedPassword(string passwordHash, string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            var verificationResult = passwordHasher.VerifyHashedPassword(null, passwordHash, password);

            return verificationResult;
        }
    }
}
