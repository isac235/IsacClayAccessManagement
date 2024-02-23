namespace Data.Repository.Tests
{
    using Data.Repository.Repositories;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class UserClaimRepositoryTests
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
                var userClaim = new UserClaim { UserClaimID = Guid.NewGuid(), UserID = user.UserID};

                context.Users.Add(user);
                context.UserClaims.Add(userClaim);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task GetUserClaimByUserIdAsync_ExistingUserId_ReturnsUserClaims()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new UserClaimRepository(context, null);
                var userId = (await context.Users.FirstAsync()).UserID;

                // Act
                var userClaims = await repository.GetUserClaimByUserIdAsync(userId);

                // Assert
                Assert.IsNotNull(userClaims);
            }
        }

        [TestMethod]
        public async Task AddUserClaimAsync_ValidUserClaim_AddsUserClaim()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new UserClaimRepository(context, null);
                var userId = (await context.Users.FirstAsync()).UserID;
                var newUserClaim = new UserClaim { UserClaimID = Guid.NewGuid(), UserID = userId };

                // Act
                var addedUserClaim = await repository.AddUserClaimAsync(newUserClaim);

                // Assert
                Assert.IsNotNull(addedUserClaim);

                // Check if user claim is in the database
                var userClaimFromDb = await context.UserClaims.FindAsync(addedUserClaim.UserClaimID);
                Assert.IsNotNull(userClaimFromDb);
            }
        }
    }
}
