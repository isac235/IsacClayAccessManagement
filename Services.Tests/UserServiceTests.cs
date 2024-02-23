namespace Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services;

    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public async Task GetAllUsersAsync_ReturnsListOfUsers()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object);

            var usersFromRepository = new List<Domain.Model.User>();
            userRepositoryMock.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(usersFromRepository);

            // Act
            var result = await userService.GetAllUsersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(usersFromRepository.Count, result.Count);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ValidUserId_ReturnsUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object);

            var userFromRepository = new Domain.Model.User { UserID = userId };
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(userFromRepository);

            // Act
            var result = await userService.GetUserByIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserId);
        }

        [TestMethod]
        public async Task CreateUserAsync_ValidUserDto_CreatesUserAndReturnsDto()
        {
            // Arrange
            var newUserDTO = new User
            {
                UserId = new Guid("8d0dd02b-712e-48c7-8666-2fe9c3e99042"),
                Password = "secretPassword",
                FullName = "FullName",
                Username = "Username"
            };

            var officeId = Guid.NewGuid();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object);

            var newUserDomain = new Domain.Model.User
            {
                UserID = new Guid("8d0dd02b-712e-48c7-8666-2fe9c3e99042"),
                FullName = "FullName",
                Username = "Username"
            };

            userRepositoryMock.Setup(repo => repo.CreateUserAsync(It.IsAny<Domain.Model.User>())).ReturnsAsync(newUserDomain);

            // Act
            var result = await userService.CreateUserAsync(newUserDTO, officeId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ValidateUserCredentialsAsync_ValidLoginRequest_ReturnsUserDto()
        {
            // Arrange
            var loginRequest = new UserLoginRequest
            {
                Username = "username",
                Password = "password"
            };
            var officeId = Guid.NewGuid();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object);

            var userFromRepository = new Domain.Model.User
            {
                PasswordHash = HashPassword("password"),
                FullName = "fullName",
                Username = "username"
            };
            userRepositoryMock.Setup(repo => repo.GetUserByUserNameAndOffice(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(userFromRepository);

            // Act
            var result = await userService.ValidateUserCredentialsAsync(loginRequest, officeId);

            // Assert
            Assert.IsNotNull(result);
        }

        private static string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(null, password);
        }
    }
}
