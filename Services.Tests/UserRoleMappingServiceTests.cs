namespace Services.Tests
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class UserRoleMappingServiceTests
    {
        [TestMethod]
        public async Task CreateUserRoleMappingAsync_ValidUserRoleMapping_CreatesUserRoleMappingAndReturnsDto()
        {
            // Arrange
            var userRoleMapping = new UserRoleMapping();
            var officeId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Domain.Model.User { UserID = userId, OfficeID = officeId });

            var userRoleMappingRepositoryMock = new Mock<IUserRoleMappingRepository>();
            var userRoleMappingService = new UserRoleMappingService(userRoleMappingRepositoryMock.Object, userRepositoryMock.Object);

            userRoleMappingRepositoryMock.Setup(repo => repo.CreateUserRolesAsync(It.IsAny<Domain.Model.UserRoleMapping>())).ReturnsAsync(new Domain.Model.UserRoleMapping { UserID = userId });

            // Act
            var result = await userRoleMappingService.CreateUserRoleMappingAsync(userRoleMapping, officeId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserId);
        }

        [TestMethod]
        public async Task CreateUserRoleMappingAsync_InvalidUserId_ThrowsValidationException()
        {
            // Arrange
            var userRoleMapping = new UserRoleMapping();
            var officeId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync((Domain.Model.User)null);

            var userRoleMappingService = new UserRoleMappingService(Mock.Of<IUserRoleMappingRepository>(), userRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ValidationException>(() => userRoleMappingService.CreateUserRoleMappingAsync(userRoleMapping, officeId));
        }

        [TestMethod]
        public async Task CreateUserRoleMappingAsync_UserNotBelongToOffice_ThrowsValidationException()
        {
            // Arrange
            var userRoleMapping = new UserRoleMapping();
            var officeId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(new Domain.Model.User { UserID = userId, OfficeID = Guid.NewGuid() });

            var userRoleMappingService = new UserRoleMappingService(Mock.Of<IUserRoleMappingRepository>(), userRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ValidationException>(() => userRoleMappingService.CreateUserRoleMappingAsync(userRoleMapping, officeId));
        }
    }
}
