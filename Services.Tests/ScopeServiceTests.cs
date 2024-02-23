namespace Services.Tests
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Mappers;

    [TestClass]
    public class ScopeServiceTests
    {
        [TestMethod]
        public async Task CreateScopeAsync_ValidScopeAndDoor_ReturnsCreatedScope()
        {
            // Arrange
            var doorId = Guid.NewGuid();
            var officeId = Guid.NewGuid();
            var scopeDto = new Scope();
            var scopeDomain = scopeDto.MapToScopeDomain();

            var doorRepositoryMock = new Mock<IDoorRepository>();
            doorRepositoryMock.Setup(repo => repo.GetDoorByOfficeIdAndDoorIdAsync(officeId, doorId)).ReturnsAsync(new Domain.Model.Door { /* Initialize with required properties */ });

            var scopeRepositoryMock = new Mock<IScopeRepository>();
            scopeRepositoryMock.Setup(repo => repo.CreateScopeAsync(It.IsAny<Domain.Model.Scope>())).ReturnsAsync(scopeDomain);

            var scopeService = new ScopeService(scopeRepositoryMock.Object, doorRepositoryMock.Object);

            // Act
            var result = await scopeService.CreateScopeAsync(scopeDto, doorId, officeId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateScopeAsync_InvalidOfficeIdAndDoorId_ThrowsValidationException()
        {
            // Arrange
            var doorId = Guid.NewGuid();
            var officeId = Guid.NewGuid();
            var scopeDto = new Scope();

            var doorRepositoryMock = new Mock<IDoorRepository>();
            doorRepositoryMock.Setup(repo => repo.GetDoorByOfficeIdAndDoorIdAsync(officeId, doorId)).ReturnsAsync((Domain.Model.Door)null);

            var scopeRepositoryMock = new Mock<IScopeRepository>();

            var scopeService = new ScopeService(scopeRepositoryMock.Object, doorRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ValidationException>(() => scopeService.CreateScopeAsync(scopeDto, doorId, officeId));
        }
    }
}
