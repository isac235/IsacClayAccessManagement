namespace Services.Tests
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Mappers;

    [TestClass]
    public class DoorServiceTests
    {
        [TestMethod]
        public async Task CreateDoorAsync_ValidDoor_ReturnsCreatedDoor()
        {
            // Arrange
            var officeId = Guid.NewGuid();
            var doorDto = new Door();
            var doorDomain = doorDto.MapToDoorDomain();
            doorDomain.OfficeID = officeId;

            var doorRepositoryMock = new Mock<IDoorRepository>();
            doorRepositoryMock.Setup(repo => repo.CreateDoorAsync(It.IsAny<Domain.Model.Door>())).ReturnsAsync(doorDomain);

            var doorService = new DoorService(doorRepositoryMock.Object);

            // Act
            var result = await doorService.CreateDoorAsync(doorDto, officeId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateDoorAsync_NullDoor_ThrowsException()
        {
            // Arrange
            var officeId = Guid.NewGuid();
            Door nullDoorDto = null;

            var doorRepositoryMock = new Mock<IDoorRepository>();

            var doorService = new DoorService(doorRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<System.NullReferenceException>(() => doorService.CreateDoorAsync(nullDoorDto, officeId));
        }
    }
}
