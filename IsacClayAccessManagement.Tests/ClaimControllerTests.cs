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

    [TestClass]
    public class ClaimControllerTests
    {
        [TestMethod]
        public async Task CreateClaim_ValidClaim_ReturnsOkResult()
        {
            // Arrange
            var claimDto = new ClaimDto();
            var mockService = new Mock<IClaimService>();
            mockService.Setup(service => service.CreateClaimAsync(claimDto))
                       .ReturnsAsync(new ClaimDto { /* initialize created claimDto properties */ });
            var controller = new ClaimController(mockService.Object);

            // Act
            var result = await controller.CreateClaim(claimDto) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
        }

        [TestMethod]
        public async Task CreateClaim_InvalidModel_ReturnsBadRequestResult()
        {
            // Arrange
            var claimDto = new ClaimDto();
            var controller = new ClaimController(Mock.Of<IClaimService>());
            controller.ModelState.AddModelError("PropertyName", "Error message");

            // Act
            var result = await controller.CreateClaim(claimDto) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateClaim_ServiceThrowsException_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var claimDto = new ClaimDto();
            var mockService = new Mock<IClaimService>();
            mockService.Setup(service => service.CreateClaimAsync(claimDto))
                       .ThrowsAsync(new Exception("Test exception"));
            var controller = new ClaimController(mockService.Object);

            // Act
            var result = await controller.CreateClaim(claimDto) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Internal server error"));
        }
    }
}
