namespace Services.Tests.Mappers
{
    using System;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Mappers;

    [TestClass]
    public class UserClaimMapperTests
    {
        [TestMethod]
        public void MapUserClaimToDto_ValidUserClaim_ReturnsUserClaimDto()
        {
            // Arrange
            var userClaim = new Domain.Model.UserClaim { UserClaimID = Guid.NewGuid(), UserID = Guid.NewGuid(), ClaimID = Guid.NewGuid() };

            // Act
            var result = userClaim.MapUserClaimToDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userClaim.UserClaimID, result.UserClaimID);
            Assert.AreEqual(userClaim.UserID, result.UserID);
            Assert.AreEqual(userClaim.ClaimID, result.ClaimID);
        }

        [TestMethod]
        public void MapUserClaimToDto_NullUserClaim_ReturnsNull()
        {
            // Arrange
            Domain.Model.UserClaim userClaim = null;

            // Act
            var result = userClaim.MapUserClaimToDto();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapUserClaimToDomain_ValidUserClaimDto_ReturnsUserClaimDomain()
        {
            // Arrange
            var userClaimDto = new UserClaim { UserClaimID = Guid.NewGuid(), UserID = Guid.NewGuid(), ClaimID = Guid.NewGuid() };

            // Act
            var result = userClaimDto.MapUserClaimToDomain();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userClaimDto.UserClaimID, result.UserClaimID);
            Assert.AreEqual(userClaimDto.UserID, result.UserID);
            Assert.AreEqual(userClaimDto.ClaimID, result.ClaimID);
        }

        [TestMethod]
        public void MapUserClaimToDomain_NullUserClaimDto_ReturnsNull()
        {
            // Arrange
            UserClaim userClaimDto = null;

            // Act
            var result = userClaimDto.MapUserClaimToDomain();

            // Assert
            Assert.IsNull(result);
        }
    }
}
