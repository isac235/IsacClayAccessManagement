namespace IsacClayAccessManagement.Tests
{
    using System;
    using System.Threading.Tasks;
    using DTO;
    using IsacClayAccessManagement.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Interfaces;

    [TestClass]
    public class UserClaimControllerTests
    {
        [TestMethod]
        public async Task AssociateUserWithClaim_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var userClaimDto = new UserClaim { UserID = Guid.NewGuid(), ClaimID = Guid.NewGuid() };
            var mockService = new Mock<IUserClaimService>();
            mockService.Setup(service => service.AssociateUserWithClaimAsync(userClaimDto.UserID, userClaimDto.ClaimID))
                .ReturnsAsync(userClaimDto);
            var controller = new UserClaimController(mockService.Object);

            // Act
            var result = await controller.AssociateUserWithClaim(userClaimDto) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(userClaimDto, result.Value);
        }

        [TestMethod]
        public async Task AssociateUserWithClaim_InvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            var userClaimDto = new UserClaim { UserID = Guid.Empty, ClaimID = Guid.Empty };
            var mockService = new Mock<IUserClaimService>();
            var controller = new UserClaimController(mockService.Object);
            controller.ModelState.AddModelError("UserID", "UserID is required");
            controller.ModelState.AddModelError("ClaimID", "ClaimID is required");

            // Act
            var result = await controller.AssociateUserWithClaim(userClaimDto) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task AssociateUserWithClaim_ServiceThrowsException_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var userClaimDto = new UserClaim { UserID = Guid.NewGuid(), ClaimID = Guid.NewGuid() };
            var mockService = new Mock<IUserClaimService>();
            mockService.Setup(service => service.AssociateUserWithClaimAsync(userClaimDto.UserID, userClaimDto.ClaimID))
                .ThrowsAsync(new Exception("Test exception"));
            var controller = new UserClaimController(mockService.Object);

            // Act
            var result = await controller.AssociateUserWithClaim(userClaimDto) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Internal server error"));
        }
    }
}
