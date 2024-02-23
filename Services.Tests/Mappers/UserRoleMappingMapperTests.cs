namespace Services.Tests.Mappers
{
    using System;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Mappers;

    [TestClass]
    public class UserRoleMappingMapperTests
    {
        [TestMethod]
        public void MapToUserRoleMappingDto_ValidUserRoleMapping_ReturnsUserRoleMappingDto()
        {
            // Arrange
            var userRoleMapping = new Domain.Model.UserRoleMapping
            {
                RoleID = Guid.NewGuid(),
                UserID = Guid.NewGuid(),
                UserRoleID = Guid.NewGuid()
            };

            // Act
            var result = userRoleMapping.MapToUserRoleMappingDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userRoleMapping.RoleID, result.RoleId);
            Assert.AreEqual(userRoleMapping.UserID, result.UserId);
            Assert.AreEqual(userRoleMapping.UserRoleID, result.UserRoleId);
        }

        [TestMethod]
        public void MapToUserRoleMappingDto_NullUserRoleMapping_ReturnsNull()
        {
            // Arrange
            Domain.Model.UserRoleMapping userRoleMapping = null;

            // Act
            var result = userRoleMapping.MapToUserRoleMappingDto();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToUserRoleMappingDomain_ValidUserRoleMappingDto_ReturnsUserRoleMappingDomain()
        {
            // Arrange
            var userRoleMappingDto = new UserRoleMapping
            {
                RoleId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserRoleId = Guid.NewGuid()
            };

            // Act
            var result = userRoleMappingDto.MapToUserRoleMappingDomain();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userRoleMappingDto.RoleId, result.RoleID);
            Assert.AreEqual(userRoleMappingDto.UserId, result.UserID);
            Assert.AreEqual(userRoleMappingDto.UserRoleId, result.UserRoleID);
        }

        [TestMethod]
        public void MapToUserRoleMappingDomain_NullUserRoleMappingDto_ReturnsNull()
        {
            // Arrange
            UserRoleMapping userRoleMappingDto = null;

            // Act
            var result = userRoleMappingDto.MapToUserRoleMappingDomain();

            // Assert
            Assert.IsNull(result);
        }
    }
}
