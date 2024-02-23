namespace Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class UserClaimServiceTests
    {
        [TestMethod]
        public async Task GetUserClaimsByUserIdAsync_ValidUserId_ReturnsUserClaims()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userClaims = new List<Domain.Model.UserClaim>
            {
                new Domain.Model.UserClaim { ClaimID = Guid.NewGuid(), UserID = userId },
                new Domain.Model.UserClaim { ClaimID = Guid.NewGuid(), UserID = userId }
            };

            var claimRepositoryMock = new Mock<IClaimRepository>();
            claimRepositoryMock.Setup(repo => repo.GetClaimByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Domain.Model.Claim { ClaimName = "TestClaim" });

            var userClaimRepositoryMock = new Mock<IUserClaimRepository>();
            userClaimRepositoryMock.Setup(repo => repo.GetUserClaimByUserIdAsync(userId)).ReturnsAsync(userClaims);

            var userClaimService = new UserClaimService(userClaimRepositoryMock.Object, claimRepositoryMock.Object);

            // Act
            var result = await userClaimService.GetUserClaimsByUserIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userClaims.Count, result.Count);
            foreach (var claimName in result)
            {
                Assert.AreEqual("TestClaim", claimName);
            }
        }

        [TestMethod]
        public async Task AssociateUserWithClaimAsync_ValidUserIdAndClaimId_AssociatesUserWithClaimAndReturnsDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var claimId = Guid.NewGuid();
            var userClaimRepositoryMock = new Mock<IUserClaimRepository>();
            var claimRepositoryMock = new Mock<IClaimRepository>();

            var userClaimService = new UserClaimService(userClaimRepositoryMock.Object, claimRepositoryMock.Object);
            userClaimRepositoryMock.Setup(repo => repo.AddUserClaimAsync(It.IsAny<Domain.Model.UserClaim>()))
                                    .ReturnsAsync(new Domain.Model.UserClaim { UserID = userId, ClaimID = claimId });

            // Act
            var result = await userClaimService.AssociateUserWithClaimAsync(userId, claimId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserID);
            Assert.AreEqual(claimId, result.ClaimID);
        }
    }
}
