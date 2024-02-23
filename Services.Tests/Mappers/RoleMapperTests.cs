namespace Services.Tests.Mappers
{
    using System;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Mappers;

    [TestClass]
    public class RoleMapperTests
    {
        [TestMethod]
        public void MapToRoleDto_ValidRole_ReturnsRoleDto()
        {
            // Arrange
            var role = new Domain.Model.Role
            {
                RoleID = Guid.NewGuid(),
                RoleName = "TestRole"
            };

            // Act
            var result = role.MapToRoleDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(role.RoleID, result.RoleId);
            Assert.AreEqual(role.RoleName, result.RoleName);
        }

        [TestMethod]
        public void MapToRoleDto_NullRole_ReturnsNull()
        {
            // Arrange
            Domain.Model.Role role = null;

            // Act
            var result = role.MapToRoleDto();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToRoleDomain_ValidRoleDto_ReturnsRoleDomain()
        {
            // Arrange
            var roleDto = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = "TestRole"
            };

            // Act
            var result = roleDto.MapToRoleDomain();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(roleDto.RoleId, result.RoleID);
            Assert.AreEqual(roleDto.RoleName, result.RoleName);
        }

        [TestMethod]
        public void MapToRoleDomain_NullRoleDto_ReturnsNull()
        {
            // Arrange
            Role roleDto = null;

            // Act
            var result = roleDto.MapToRoleDomain();

            // Assert
            Assert.IsNull(result);
        }
    }
}
