namespace Services.Tests.Mappers
{
    using System;
    using System.Collections.Generic;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Mappers;

    [TestClass]
    public class UserMapperTests
    {
        [TestMethod]
        public void MapUsersToDto_ValidUserList_ReturnsUserDtoList()
        {
            // Arrange
            var users = new List<Domain.Model.User>
            {
                new Domain.Model.User { FullName = "John Doe", UserID = Guid.NewGuid(), Username = "johndoe" },
                new Domain.Model.User { FullName = "Jane Smith", UserID = Guid.NewGuid(), Username = "janesmith" }
            };

            // Act
            var result = users.MapUsersToDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(users.Count, result.Count);
            for (int i = 0; i < users.Count; i++)
            {
                Assert.AreEqual(users[i].FullName, result[i].FullName);
                Assert.AreEqual(users[i].UserID, result[i].UserId);
                Assert.AreEqual(users[i].Username, result[i].Username);
            }
        }

        [TestMethod]
        public void MapUsersToDto_NullUserList_ReturnsNull()
        {
            // Arrange
            List<Domain.Model.User> users = null;

            // Act
            var result = users.MapUsersToDto();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToUserDto_ValidUser_ReturnsUserDto()
        {
            // Arrange
            var user = new Domain.Model.User { FullName = "John Doe", UserID = Guid.NewGuid(), Username = "johndoe" };

            // Act
            var result = user.MapToUserDto();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.FullName, result.FullName);
            Assert.AreEqual(user.UserID, result.UserId);
            Assert.AreEqual(user.Username, result.Username);
        }

        [TestMethod]
        public void MapToUserDto_NullUser_ReturnsNull()
        {
            // Arrange
            Domain.Model.User user = null;

            // Act
            var result = user.MapToUserDto();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapUserToDomain_ValidUserDto_ReturnsUserDomain()
        {
            // Arrange
            var userDto = new User { FullName = "John Doe", UserId = Guid.NewGuid(), Username = "johndoe" };

            // Act
            var result = userDto.MapUserToDomain();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userDto.FullName, result.FullName);
            Assert.AreEqual(userDto.UserId, result.UserID);
            Assert.AreEqual(userDto.Username, result.Username);
        }

        [TestMethod]
        public void MapUserToDomain_NullUserDto_ReturnsNull()
        {
            // Arrange
            User userDto = null;

            // Act
            var result = userDto.MapUserToDomain();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapUserToUserValidation_ValidUserDto_ReturnsUserValidation()
        {
            // Arrange
            var userDto = new User { FullName = "John Doe", UserId = Guid.NewGuid(), Username = "johndoe" };

            // Act
            var result = userDto.MapUserToUserValidation();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userDto.FullName, result.FullName);
            Assert.AreEqual(userDto.UserId, result.UserId);
            Assert.AreEqual(userDto.Username, result.Username);
        }
    }
}
