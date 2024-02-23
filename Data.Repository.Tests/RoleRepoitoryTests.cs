namespace Data.Repository.Tests
{
    using Data.Repository.Repositories;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class RoleRepositoryTests
    {
        private DbContextOptions<OfficesAccessDbContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<OfficesAccessDbContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;
        }

        [TestMethod]
        public async Task CreateRoleAsync_ValidRole_AddsRole()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new RoleRepository(context);
                var newRole = new Role { RoleID = Guid.NewGuid(), RoleName = "NewRole" };

                // Act
                var addedRole = await repository.CreateRoleAsync(newRole);

                // Assert
                Assert.IsNotNull(addedRole);

                // Check if role is in the database
                var roleFromDb = await context.Roles.FindAsync(addedRole.RoleID);
                Assert.IsNotNull(roleFromDb);
            }
        }
    }
}
