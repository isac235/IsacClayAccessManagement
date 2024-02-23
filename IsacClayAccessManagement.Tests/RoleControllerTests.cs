namespace IsacClayAccessManagement.Tests
{
    using DTO;
    using IsacClayAccessManagement.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Interfaces;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class RoleControllerTests
    {
        [TestMethod]
        public async Task CreateRoleAsync_ValidInput_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var role = new Role { RoleId = Guid.NewGuid(), RoleName = "TestRole" };
            var mockService = new Mock<IRoleService>();
            mockService.Setup(service => service.CreateRoleAsync(role)).ReturnsAsync(role);
            var controller = new RoleController(mockService.Object, null);

            // Act
            var result = await controller.CreateRoleAsync(role) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(nameof(controller.CreateRoleAsync), result.ActionName);
            Assert.AreEqual(role, result.Value);
        }

        [TestMethod]
        public async Task CreateRoleAsync_InvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            var role = new Role { RoleName = "" }; // Invalid input
            var mockService = new Mock<IRoleService>();
            var controller = new RoleController(mockService.Object, null);
            controller.ModelState.AddModelError("RoleName", "RoleName is required");

            // Act
            var result = await controller.CreateRoleAsync(role) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateRoleAsync_ServiceThrowsException_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var log = new Mock<ILogger<RoleController>>();
            var role = new Role { RoleName = "TestRole" };
            var mockService = new Mock<IRoleService>();
            mockService.Setup(service => service.CreateRoleAsync(role)).ThrowsAsync(new Exception("Test exception"));
            var controller = new RoleController(mockService.Object, log.Object);

            // Act
            var result = await controller.CreateRoleAsync(role) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Internal server error"));
        }
    }
}
