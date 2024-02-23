namespace Data.Repository.Tests
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Repositories;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserRepositoryTests
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
                context.Users.Add(new User { UserID = Guid.NewGuid(), Username = "user1", OfficeID = Guid.NewGuid() });
                context.Users.Add(new User { UserID = Guid.NewGuid(), Username = "user2", OfficeID = Guid.NewGuid() });
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new UserRepository(context, null);

                // Act
                var users = await repository.GetAllUsersAsync();

                // Assert
                Assert.IsNotNull(users);
            }
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ExistingUserId_ReturnsUser()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new UserRepository(context, null);
                var userId = (await context.Users.FirstAsync()).UserID;

                // Act
                var user = await repository.GetUserByIdAsync(userId);

                // Assert
                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        public async Task GetUserByUserNameAndOffice_ExistingUser_ReturnsUser()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new UserRepository(context, null);
                var user = await context.Users.FirstAsync();

                // Act
                var result = await repository.GetUserByUserNameAndOffice(user.Username, user.OfficeID);

                // Assert
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public async Task CreateUserAsync_ValidUser_CreatesUser()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new UserRepository(context, null);
                var newUser = new User { UserID = Guid.NewGuid(), Username = "newuser", OfficeID = Guid.NewGuid() };

                // Act
                var createdUser = await repository.CreateUserAsync(newUser);

                // Assert
                Assert.IsNotNull(createdUser);

                // Check if user is in the database
                var userFromDb = await context.Users.FindAsync(createdUser.UserID);
                Assert.IsNotNull(userFromDb);
            }
        }
    }
}
