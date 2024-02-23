namespace Services.Tests.Mappers
{
    using System;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Mappers;

    [TestClass]
    public class AccessEventMapperTests
    {
        [TestMethod]
        public void MapToAccessEventDto_ValidAccessEvent_ReturnsAccessEventDto()
        {
            // Arrange
            var accessEvent = new Domain.Model.AccessEvent
            {
                EventID = Guid.NewGuid(),
                DoorID = Guid.NewGuid(),
                UserID = Guid.NewGuid(),
                EventTime = DateTime.Now,
                AccessMethod = "Remote"
            };

            // Act
            var result = accessEvent.MapToAccessEventDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(accessEvent.EventID, result.EventId);
            Assert.AreEqual(accessEvent.DoorID, result.DoorId);
            Assert.AreEqual(accessEvent.UserID, result.UserId);
            Assert.AreEqual(accessEvent.EventTime, result.EventTime);
            Assert.AreEqual("Remote", result.AccessMethod.ToString());
        }

        [TestMethod]
        public void MapToAccessEventDto_NullAccessEvent_ReturnsNull()
        {
            // Arrange
            Domain.Model.AccessEvent accessEvent = null;

            // Act
            var result = accessEvent.MapToAccessEventDto();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToAccessEventDomain_ValidAccessEventDto_ReturnsAccessEventDomain()
        {
            // Arrange
            var accessEventDto = new DTO.AccessEvent
            {
                EventId = Guid.NewGuid(),
                DoorId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                EventTime = DateTime.Now,
                AccessMethod = AccessMethod.Tag
            };

            // Act
            var result = accessEventDto.MapToAccessEventDomain();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(accessEventDto.EventId, result.EventID);
            Assert.AreEqual(accessEventDto.DoorId, result.DoorID);
            Assert.AreEqual(accessEventDto.UserId, result.UserID);
            Assert.AreEqual(accessEventDto.EventTime, result.EventTime);
            Assert.AreEqual("Tag", result.AccessMethod);
        }

        [TestMethod]
        public void MapToAccessEventDomain_NullAccessEventDto_ReturnsNull()
        {
            // Arrange
            DTO.AccessEvent accessEventDto = null;

            // Act
            var result = accessEventDto.MapToAccessEventDomain();

            // Assert
            Assert.IsNull(result);
        }
    }
}
