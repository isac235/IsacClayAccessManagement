namespace Services.Tests
{
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Mappers;

    [TestClass]
    public class OfficeServiceTests
    {
        [TestMethod]
        public async Task CreateOfficeAsync_ValidOffice_ReturnsCreatedOffice()
        {
            // Arrange
            var newOfficeDto = new Office();
            var newOfficeDomain = newOfficeDto.MapToOfficeDomain();

            var officeRepositoryMock = new Mock<IOfficeRepository>();
            officeRepositoryMock.Setup(repo => repo.CreateOfficeAsync(It.IsAny<Domain.Model.Office>())).ReturnsAsync(newOfficeDomain);

            var officeService = new OfficeService(officeRepositoryMock.Object);

            // Act
            var result = await officeService.CreateOfficeAsync(newOfficeDto);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
