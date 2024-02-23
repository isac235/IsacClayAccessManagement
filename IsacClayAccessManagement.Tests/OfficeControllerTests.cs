namespace IsacClayAccessManagement.Tests
{
    using System;
    using System.Threading.Tasks;
    using IsacClayAccessManagement.Controllers;
    using DTO;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Interfaces;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    [TestClass]
    public class OfficeControllerTests
    {
        [TestMethod]
        public async Task CreateOfficeAsync_ValidOffice_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var office = new Office();
            var mockService = new Mock<IOfficeService>();
            mockService.Setup(service => service.CreateOfficeAsync(office))
                       .ReturnsAsync(new Office { /* initialize created office properties */ });
            var controller = new OfficeController(Mock.Of<IConfiguration>(), mockService.Object, Mock.Of<IUserService>(), Mock.Of<IDoorService>(), Mock.Of<IScopeService>(), Mock.Of<IUserRoleMappingService>(), Mock.Of<IAccessEventService>(), Mock.Of<IUserClaimService>());

            // Act
            var result = await controller.CreateOfficeAsync(office) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.IsNotNull(result.Value);
        }

        [TestMethod]
        public async Task CreateOfficeUserAsync_ValidUser_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var officeId = Guid.NewGuid();
            var user = new User();
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.CreateUserAsync(user, officeId))
                       .ReturnsAsync(new User { /* initialize created user properties */ });
            var controller = new OfficeController(Mock.Of<IConfiguration>(), Mock.Of<IOfficeService>(), mockService.Object, Mock.Of<IDoorService>(), Mock.Of<IScopeService>(), Mock.Of<IUserRoleMappingService>(), Mock.Of<IAccessEventService>(), Mock.Of<IUserClaimService>());

            // Act
            var result = await controller.CreateOfficeUserAsync(officeId, user) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.IsNotNull(result.Value);
        }

        [TestMethod]
        public async Task CreateOfficeUserRoleMappingAsync_ValidUserRoleMapping_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var officeId = Guid.NewGuid();
            var userRoleMapping = new UserRoleMapping();
            var mockService = new Mock<IUserRoleMappingService>();
            mockService.Setup(service => service.CreateUserRoleMappingAsync(userRoleMapping, officeId))
                       .ReturnsAsync(new UserRoleMapping { /* initialize created user role mapping properties */ });
            var controller = new OfficeController(Mock.Of<IConfiguration>(), Mock.Of<IOfficeService>(), Mock.Of<IUserService>(), Mock.Of<IDoorService>(), Mock.Of<IScopeService>(), mockService.Object, Mock.Of<IAccessEventService>(), Mock.Of<IUserClaimService>());

            // Act
            var result = await controller.CreateOfficeUserRoleMappingAsync(officeId, userRoleMapping) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.IsNotNull(result.Value);
            // Add more assertions if necessary
        }

        [TestMethod]
        public async Task CreateOfficeDoorAsync_ValidDoor_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var officeId = Guid.NewGuid();
            var door = new Door();
            var mockService = new Mock<IDoorService>();
            mockService.Setup(service => service.CreateDoorAsync(door, officeId))
                       .ReturnsAsync(new Door { /* initialize created door properties */ });
            var controller = new OfficeController(Mock.Of<IConfiguration>(), Mock.Of<IOfficeService>(), Mock.Of<IUserService>(), mockService.Object, Mock.Of<IScopeService>(), Mock.Of<IUserRoleMappingService>(), Mock.Of<IAccessEventService>(), Mock.Of<IUserClaimService>());

            // Act
            var result = await controller.CreateOfficeDoorAsync(officeId, door) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.IsNotNull(result.Value);
            // Add more assertions if necessary
        }

        [TestMethod]
        public async Task CreateOfficeDoorScopeAsync_ValidScope_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var officeId = Guid.NewGuid();
            var doorId = Guid.NewGuid();
            var scope = new Scope { /* initialize scope properties */ };
            var mockService = new Mock<IScopeService>();
            mockService.Setup(service => service.CreateScopeAsync(scope, doorId, officeId))
                       .ReturnsAsync(new Scope { /* initialize created scope properties */ });
            var controller = new OfficeController(Mock.Of<IConfiguration>(), Mock.Of<IOfficeService>(), Mock.Of<IUserService>(), Mock.Of<IDoorService>(), mockService.Object, Mock.Of<IUserRoleMappingService>(), Mock.Of<IAccessEventService>(), Mock.Of<IUserClaimService>());

            // Act
            var result = await controller.CreateOfficeDoorScopeAsync(officeId, doorId, scope) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.IsNotNull(result.Value);
            // Add more assertions if necessary
        }

        [TestMethod]
        public async Task AccessEventAsync_ValidAccessEvent_ReturnsOkObjectResult()
        {
            // Arrange
            var officeId = Guid.NewGuid();
            var accessEvent = new AccessEvent { /* initialize access event properties */ };
            var mockService = new Mock<IAccessEventService>();
            mockService.Setup(service => service.AccessEventAsync(accessEvent, officeId))
                       .ReturnsAsync(new AccessEventResult { /* initialize access event result properties */ });
            var controller = new OfficeController(Mock.Of<IConfiguration>(), Mock.Of<IOfficeService>(), Mock.Of<IUserService>(), Mock.Of<IDoorService>(), Mock.Of<IScopeService>(), Mock.Of<IUserRoleMappingService>(), mockService.Object, Mock.Of<IUserClaimService>());

            // Act
            var result = await controller.AccessEventAsync(officeId, accessEvent) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            // Add more assertions if necessary
        }

        [TestMethod]
        public async Task CreateOfficeAsync_ServiceThrowsException_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var controller = CreateOfficeControllerWithExceptionThrowingService();
            var office = new Office();

            // Act
            var result = await controller.CreateOfficeAsync(office) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Internal server error"));
        }

        [TestMethod]
        public async Task CreateOfficeUserAsync_ServiceThrowsException_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var controller = CreateOfficeControllerWithExceptionThrowingService();
            var officeId = Guid.NewGuid();
            var user = new User();

            // Act
            var result = await controller.CreateOfficeUserAsync(officeId, user) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Internal server error"));
        }

        [TestMethod]
        public async Task CreateOfficeUserRoleMappingAsync_ServiceThrowsException_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var controller = CreateOfficeControllerWithExceptionThrowingService();
            var officeId = Guid.NewGuid();
            var userRoleMapping = new UserRoleMapping();

            // Act
            var result = await controller.CreateOfficeUserRoleMappingAsync(officeId, userRoleMapping) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Internal server error"));
        }

        [TestMethod]
        public async Task CreateOfficeDoorAsync_ServiceThrowsException_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var controller = CreateOfficeControllerWithExceptionThrowingService();
            var officeId = Guid.NewGuid();
            var door = new Door();

            // Act
            var result = await controller.CreateOfficeDoorAsync(officeId, door) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Internal server error"));
        }

        [TestMethod]
        public async Task CreateOfficeDoorScopeAsync_ServiceThrowsException_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var controller = CreateOfficeControllerWithExceptionThrowingService();
            var officeId = Guid.NewGuid();
            var doorId = Guid.NewGuid();
            var scope = new Scope();

            // Act
            var result = await controller.CreateOfficeDoorScopeAsync(officeId, doorId, scope) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Internal server error"));
        }

        private OfficeController CreateOfficeControllerWithExceptionThrowingService()
        {
            var mockService = new Mock<IOfficeService>();
            mockService.Setup(service => service.CreateOfficeAsync(It.IsAny<Office>()))
                       .ThrowsAsync(new Exception("Test exception"));
            return new OfficeController(Mock.Of<IConfiguration>(), mockService.Object, Mock.Of<IUserService>(), Mock.Of<IDoorService>(), Mock.Of<IScopeService>(), Mock.Of<IUserRoleMappingService>(), Mock.Of<IAccessEventService>(), Mock.Of<IUserClaimService>());
        }
    }
}
