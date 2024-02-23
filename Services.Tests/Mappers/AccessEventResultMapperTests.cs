namespace Services.Tests.Mappers
{
    using System;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Mappers;

    [TestClass]
    public class AccessEventResultMapperTests
    {
        [TestMethod]
        public void MapToAccesEventResult_ValidAccessEvent_ReturnsAccessEventResult()
        {
            // Arrange
            var accessEvent = new Domain.Model.AccessEvent
            {
                EventID = Guid.NewGuid(),
                AccessResult = "Authorized"
            };

            // Act
            var result = accessEvent.MapToAccesEventResult();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(accessEvent.EventID, result.AccessEventId);
            Assert.AreEqual(AccessEventAuthorization.Authorized, result.AccessEventAuthorization);
        }

        [TestMethod]
        public void MapToAccesEventResult_NullAccessEvent_ReturnsNull()
        {
            // Arrange
            Domain.Model.AccessEvent accessEvent = null;

            // Act
            var result = accessEvent.MapToAccesEventResult();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToAccesEventResult_UnauthorizedAccessEvent_ReturnsAccessEventResult()
        {
            // Arrange
            var accessEvent = new Domain.Model.AccessEvent
            {
                EventID = Guid.NewGuid(),
                AccessResult = "Unauthorized"
            };

            // Act
            var result = accessEvent.MapToAccesEventResult();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(accessEvent.EventID, result.AccessEventId);
            Assert.AreEqual(AccessEventAuthorization.Unauthorized, result.AccessEventAuthorization);
        }

        [TestMethod]
        public void MapToAccesEventResult_UnknownAccessResult_ReturnsUnauthorized()
        {
            // Arrange
            var accessEvent = new Domain.Model.AccessEvent
            {
                EventID = Guid.NewGuid(),
                AccessResult = "UnknownResult"
            };

            // Act
            var result = accessEvent.MapToAccesEventResult();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(accessEvent.EventID, result.AccessEventId);
            Assert.AreEqual(AccessEventAuthorization.Unauthorized, result.AccessEventAuthorization);
        }
    }
}
