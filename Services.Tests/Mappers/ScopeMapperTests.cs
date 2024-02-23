namespace Services.Tests.Mappers
{
    using System;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Mappers;

    [TestClass]
    public class ScopeMapperTests
    {
        [TestMethod]
        public void MapToScopeDto_ValidScope_ReturnsScopeDto()
        {
            // Arrange
            var scope = new Domain.Model.Scope
            {
                ScopeID = Guid.NewGuid(),
                ScopeName = "TestScope",
                Description = "Test Description",
                RoleID = Guid.NewGuid()
            };

            // Act
            var result = scope.MapToScopeDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(scope.ScopeID, result.ScopeId);
            Assert.AreEqual(scope.ScopeName, result.ScopeName);
            Assert.AreEqual(scope.Description, result.Description);
            Assert.AreEqual(scope.RoleID, result.RoleId);
        }

        [TestMethod]
        public void MapToScopeDto_NullScope_ReturnsNull()
        {
            // Arrange
            Domain.Model.Scope scope = null;

            // Act
            var result = scope.MapToScopeDto();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToScopeDomain_ValidScopeDto_ReturnsScopeDomain()
        {
            // Arrange
            var scopeDto = new Scope
            {
                ScopeId = Guid.NewGuid(),
                ScopeName = "TestScope",
                Description = "Test Description",
                RoleId = Guid.NewGuid()
            };

            // Act
            var result = scopeDto.MapToScopeDomain();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(scopeDto.ScopeId, result.ScopeID);
            Assert.AreEqual(scopeDto.ScopeName, result.ScopeName);
            Assert.AreEqual(scopeDto.Description, result.Description);
            Assert.AreEqual(scopeDto.RoleId, result.RoleID);
        }

        [TestMethod]
        public void MapToScopeDomain_NullScopeDto_ReturnsNull()
        {
            // Arrange
            Scope scopeDto = null;

            // Act
            var result = scopeDto.MapToScopeDomain();

            // Assert
            Assert.IsNull(result);
        }
    }
}
