namespace Services.Tests.Mappers
{
    using System;

    using Domain.Model;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Mappers;

    [TestClass]
    public class ClaimMapperTests
    {
        [TestMethod]
        public void MapToClaimDto_ValidClaim_ReturnsClaimDto()
        {
            // Arrange
            var claim = new Claim
            {
                ClaimID = Guid.NewGuid(),
                ClaimName = "TestClaim"
            };

            // Act
            var result = claim.MapToClaimDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(claim.ClaimID, result.ClaimID);
            Assert.AreEqual(claim.ClaimName, result.ClaimName);
        }

        [TestMethod]
        public void MapToClaimDto_NullClaim_ReturnsNull()
        {
            // Arrange
            Claim claim = null;

            // Act
            var result = claim.MapToClaimDto();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToClaimDomain_ValidClaimDto_ReturnsClaimDomain()
        {
            // Arrange
            var claimDto = new ClaimDto
            {
                ClaimID = Guid.NewGuid(),
                ClaimName = "TestClaim"
            };

            // Act
            var result = claimDto.MapToClaimDomain();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(claimDto.ClaimID, result.ClaimID);
            Assert.AreEqual(claimDto.ClaimName, result.ClaimName);
        }

        [TestMethod]
        public void MapToClaimDomain_NullClaimDto_ReturnsNull()
        {
            // Arrange
            ClaimDto claimDto = null;

            // Act
            var result = claimDto.MapToClaimDomain();

            // Assert
            Assert.IsNull(result);
        }
    }
}
