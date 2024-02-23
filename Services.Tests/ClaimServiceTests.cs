namespace Services.Tests
{
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Mappers;

    [TestClass]
    public class ClaimServiceTests
    {
        [TestMethod]
        public async Task CreateClaimAsync_ValidClaim_ReturnsCreatedClaim()
        {
            // Arrange
            var claimDto = new ClaimDto();
            var claimDomain = claimDto.MapToClaimDomain();

            var claimRepositoryMock = new Mock<IClaimRepository>();
            claimRepositoryMock.Setup(repo => repo.AddClaimAsync(It.IsAny<Domain.Model.Claim>())).ReturnsAsync(claimDomain);

            var claimService = new ClaimService(claimRepositoryMock.Object);

            // Act
            var result = await claimService.CreateClaimAsync(claimDto);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
