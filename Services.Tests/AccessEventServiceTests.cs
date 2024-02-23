namespace Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AccessEventServiceTests
    {
        [TestMethod]
        public async Task AccessEventAsync_ValidAccessEvent_ReturnsAccessEventResult()
        {
            // Arrange
            var accessEvent = new AccessEvent();
            var officeId = Guid.NewGuid();

            var door = new Domain.Model.Door();
            var userRoles = new List<Domain.Model.UserRoleMapping>
            {
                new Domain.Model.UserRoleMapping()
            };
            var scopes = new List<Domain.Model.Scope>
            {
                new Domain.Model.Scope()
            };

            var doorRepositoryMock = new Mock<IDoorRepository>();
            doorRepositoryMock.Setup(repo => repo.GetDoorByOfficeIdAndDoorIdAsync(officeId, accessEvent.DoorId)).ReturnsAsync(door);

            var userRoleMappingRepositoryMock = new Mock<IUserRoleMappingRepository>();
            userRoleMappingRepositoryMock.Setup(repo => repo.GetUserRoleMappingByUserIdAsync(accessEvent.UserId)).ReturnsAsync(userRoles);

            var scopeRepositoryMock = new Mock<IScopeRepository>();
            scopeRepositoryMock.Setup(repo => repo.GetScopesByDoorIdAsync(accessEvent.DoorId)).ReturnsAsync(scopes);

            var accessEventRepositoryMock = new Mock<IAccessEventRepository>();
            accessEventRepositoryMock.Setup(repo => repo.CreateAccessEventAsync(It.IsAny<Domain.Model.AccessEvent>())).ReturnsAsync(new Domain.Model.AccessEvent());

            var service = new AccessEventService(
                accessEventRepositoryMock.Object,
                doorRepositoryMock.Object,
                userRoleMappingRepositoryMock.Object,
                scopeRepositoryMock.Object);

            // Act
            var result = await service.AccessEventAsync(accessEvent, officeId);

            // Assert
            Assert.IsNotNull(result);
            // Add additional assertions as needed based on the returned AccessEventResult object
        }

        [TestMethod]
        public async Task AccessEventAsync_InvalidDoor_ThrowsValidationException()
        {
            // Arrange
            var accessEvent = new AccessEvent();
            var officeId = Guid.NewGuid();

            var doorRepositoryMock = new Mock<IDoorRepository>();
            doorRepositoryMock.Setup(repo => repo.GetDoorByOfficeIdAndDoorIdAsync(officeId, accessEvent.DoorId)).ReturnsAsync((Domain.Model.Door)null);

            var service = new AccessEventService(
                Mock.Of<IAccessEventRepository>(),
                doorRepositoryMock.Object,
                Mock.Of<IUserRoleMappingRepository>(),
                Mock.Of<IScopeRepository>());

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ValidationException>(() => service.AccessEventAsync(accessEvent, officeId));
        }

        [TestMethod]
        public async Task AccessEventAsync_InvalidUserRoles_ThrowsValidationException()
        {

            // Arrange
            var accessEvent = new AccessEvent();
            var officeId = Guid.NewGuid();

            var door = new Domain.Model.Door();
            var userRoles = new List<Domain.Model.UserRoleMapping>();
           
            var scopes = new List<Domain.Model.Scope>
            {
                new Domain.Model.Scope()
            };

            var doorRepositoryMock = new Mock<IDoorRepository>();
            doorRepositoryMock.Setup(repo => repo.GetDoorByOfficeIdAndDoorIdAsync(officeId, accessEvent.DoorId)).ReturnsAsync(door);

            var userRoleMappingRepositoryMock = new Mock<IUserRoleMappingRepository>();
            userRoleMappingRepositoryMock.Setup(repo => repo.GetUserRoleMappingByUserIdAsync(accessEvent.UserId)).ReturnsAsync(userRoles);

            var scopeRepositoryMock = new Mock<IScopeRepository>();
            scopeRepositoryMock.Setup(repo => repo.GetScopesByDoorIdAsync(accessEvent.DoorId)).ReturnsAsync(scopes);

            var accessEventRepositoryMock = new Mock<IAccessEventRepository>();
            accessEventRepositoryMock.Setup(repo => repo.CreateAccessEventAsync(It.IsAny<Domain.Model.AccessEvent>())).ReturnsAsync(new Domain.Model.AccessEvent());

            var service = new AccessEventService(
                accessEventRepositoryMock.Object,
                doorRepositoryMock.Object,
                userRoleMappingRepositoryMock.Object,
                scopeRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ValidationException>(() => service.AccessEventAsync(accessEvent, officeId));
        }

        [TestMethod]
        public async Task AccessEventAsync_InvalidScopes_ThrowsValidationException()
        {

            // Arrange
            var accessEvent = new AccessEvent();
            var officeId = Guid.NewGuid();

            var door = new Domain.Model.Door();
            var userRoles = new List<Domain.Model.UserRoleMapping> 
            {
                new Domain.Model.UserRoleMapping()
            };

            var scopes = new List<Domain.Model.Scope>();
            

            var doorRepositoryMock = new Mock<IDoorRepository>();
            doorRepositoryMock.Setup(repo => repo.GetDoorByOfficeIdAndDoorIdAsync(officeId, accessEvent.DoorId)).ReturnsAsync(door);

            var userRoleMappingRepositoryMock = new Mock<IUserRoleMappingRepository>();
            userRoleMappingRepositoryMock.Setup(repo => repo.GetUserRoleMappingByUserIdAsync(accessEvent.UserId)).ReturnsAsync(userRoles);

            var scopeRepositoryMock = new Mock<IScopeRepository>();
            scopeRepositoryMock.Setup(repo => repo.GetScopesByDoorIdAsync(accessEvent.DoorId)).ReturnsAsync(scopes);

            var accessEventRepositoryMock = new Mock<IAccessEventRepository>();
            accessEventRepositoryMock.Setup(repo => repo.CreateAccessEventAsync(It.IsAny<Domain.Model.AccessEvent>())).ReturnsAsync(new Domain.Model.AccessEvent());

            var service = new AccessEventService(
                accessEventRepositoryMock.Object,
                doorRepositoryMock.Object,
                userRoleMappingRepositoryMock.Object,
                scopeRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ValidationException>(() => service.AccessEventAsync(accessEvent, officeId));
        }
    }
}
