namespace Services.Tests.Mappers
{
    using System;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Mappers;

    [TestClass]
    public class OfficeMapperTests
    {
        [TestMethod]
        public void MapToOfficeDto_ValidOffice_ReturnsOfficeDto()
        {
            // Arrange
            var office = new Domain.Model.Office
            {
                OfficeID = Guid.NewGuid(),
                OfficeName = "TestOffice",
                Location = "TestLocation"
            };

            // Act
            var result = office.MapToOfficeDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(office.OfficeID, result.OfficeID);
            Assert.AreEqual(office.OfficeName, result.OfficeName);
            Assert.AreEqual(office.Location, result.Location);
        }

        [TestMethod]
        public void MapToOfficeDto_NullOffice_ReturnsNull()
        {
            // Arrange
            Domain.Model.Office office = null;

            // Act
            var result = office.MapToOfficeDto();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToOfficeDomain_ValidOfficeDto_ReturnsOfficeDomain()
        {
            // Arrange
            var officeDto = new Office
            {
                OfficeID = Guid.NewGuid(),
                OfficeName = "TestOffice",
                Location = "TestLocation"
            };

            // Act
            var result = officeDto.MapToOfficeDomain();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(officeDto.OfficeID, result.OfficeID);
            Assert.AreEqual(officeDto.OfficeName, result.OfficeName);
            Assert.AreEqual(officeDto.Location, result.Location);
        }

        [TestMethod]
        public void MapToOfficeDomain_NullOfficeDto_ReturnsNull()
        {
            // Arrange
            Office officeDto = null;

            // Act
            var result = officeDto.MapToOfficeDomain();

            // Assert
            Assert.IsNull(result);
        }
    }
}
