namespace Data.Repository.Tests
{
    using Data.Repository.Repositories;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class UserRoleMappingRepositoryTests
    {
        private DbContextOptions<OfficesAccessDbContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<OfficesAccessDbContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            using (var context = new OfficesAccessDbContext(_options))
            {
                // Add some test data
                var user = new User { UserID = Guid.NewGuid(), Username = "user1", OfficeID = Guid.NewGuid() };
                var userRoleMapping = new UserRoleMapping { UserRoleID = Guid.NewGuid(), UserID = user.UserID};

                context.Users.Add(user);
                context.UserRoleMappings.Add(userRoleMapping);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task GetUserRoleMappingByUserIdAsync_ExistingUserId_ReturnsUserRoleMappings()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new UserRoleMappingRepository(context, new LoggerFactory().CreateLogger<UserRoleMappingRepository>());
                var userId = (await context.Users.FirstAsync()).UserID;

                // Act
                var userRoleMappings = await repository.GetUserRoleMappingByUserIdAsync(userId);

                // Assert
                Assert.AreEqual(1, userRoleMappings.Count);
            }
        }

        [TestMethod]
        public async Task CreateUserRolesAsync_ValidUserRoleMapping_CreatesUserRoleMapping()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new UserRoleMappingRepository(context, new LoggerFactory().CreateLogger<UserRoleMappingRepository>());
                var userId = (await context.Users.FirstAsync()).UserID;
                var newUserRoleMapping = new UserRoleMapping { UserRoleID = Guid.NewGuid(), UserID = userId };

                // Act
                var createdUserRoleMapping = await repository.CreateUserRolesAsync(newUserRoleMapping);

                // Assert
                Assert.IsNotNull(createdUserRoleMapping);

                // Check if user role mapping is in the database
                var userRoleMappingFromDb = await context.UserRoleMappings.FindAsync(createdUserRoleMapping.UserRoleID);
                Assert.IsNotNull(userRoleMappingFromDb);
            }
        }
    }
}
