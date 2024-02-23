namespace Services.Tests
{
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Mappers;

    [TestClass]
    public class RoleServiceTests
    {
        [TestMethod]
        public async Task CreateRoleAsync_ValidRole_ReturnsCreatedRole()
        {
            // Arrange
            var roleDto = new Role();
            var roleDomain = roleDto.MapToRoleDomain();

            var roleRepositoryMock = new Mock<IRoleRepository>();
            roleRepositoryMock.Setup(repo => repo.CreateRoleAsync(It.IsAny<Domain.Model.Role>())).ReturnsAsync(roleDomain);

            var roleService = new RoleService(roleRepositoryMock.Object);

            // Act
            var result = await roleService.CreateRoleAsync(roleDto);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
