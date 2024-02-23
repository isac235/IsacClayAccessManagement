namespace Services.Tests.Mappers
{
    using System;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Mappers;

    [TestClass]
    public class DoorMapperTests
    {
        [TestMethod]
        public void MapToDoorDto_ValidDoor_ReturnsDoorDto()
        {
            // Arrange
            var door = new Domain.Model.Door
            {
                DoorID = Guid.NewGuid(),
                DoorName = "TestDoor",
                Location = "TestLocation"
            };

            // Act
            var result = door.MapToDoorDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(door.DoorID, result.DoorId);
            Assert.AreEqual(door.DoorName, result.DoorName);
            Assert.AreEqual(door.Location, result.Location);
        }

        [TestMethod]
        public void MapToDoorDto_NullDoor_ReturnsNull()
        {
            // Arrange
            Domain.Model.Door door = null;

            // Act
            var result = door.MapToDoorDto();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToDoorDomain_ValidDoorDto_ReturnsDoorDomain()
        {
            // Arrange
            var doorDto = new Door
            {
                DoorId = Guid.NewGuid(),
                DoorName = "TestDoor",
                Location = "TestLocation"
            };

            // Act
            var result = doorDto.MapToDoorDomain();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(doorDto.DoorId, result.DoorID);
            Assert.AreEqual(doorDto.DoorName, result.DoorName);
            Assert.AreEqual(doorDto.Location, result.Location);
        }

        [TestMethod]
        public void MapToDoorDomain_NullDoorDto_ReturnsNull()
        {
            // Arrange
            Door doorDto = null;

            // Act
            var result = doorDto.MapToDoorDomain();

            // Assert
            Assert.IsNull(result);
        }
    }
}
