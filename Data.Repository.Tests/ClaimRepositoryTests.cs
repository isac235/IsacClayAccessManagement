namespace Data.Repository.Tests
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Repositories;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ClaimRepositoryTests
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
        public async Task AddClaimAsync_ValidClaim_AddsClaim()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new ClaimRepository(context, null);
                var newClaim = new Claim { ClaimID = Guid.NewGuid(), ClaimName = "New Claim" };

                // Act
                var addedClaim = await repository.AddClaimAsync(newClaim);

                // Assert
                Assert.IsNotNull(addedClaim);

                // Check if claim is in the database
                var claimFromDb = await context.Claims.FindAsync(addedClaim.ClaimID);
                Assert.IsNotNull(claimFromDb);
            }
        }

        [TestMethod]
        public async Task GetClaimByIdAsync_ExistingId_ReturnsClaim()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new ClaimRepository(context, null);
                var existingClaimId = Guid.NewGuid();
                var existingClaim = new Claim { ClaimID = existingClaimId, ClaimName = "Existing Claim" };
                await context.Claims.AddAsync(existingClaim);
                await context.SaveChangesAsync();

                // Act
                var retrievedClaim = await repository.GetClaimByIdAsync(existingClaimId);

                // Assert
                Assert.IsNotNull(retrievedClaim);
                Assert.AreEqual(existingClaim.ClaimID, retrievedClaim.ClaimID);
            }
        }

        [TestMethod]
        public async Task GetClaimByIdAsync_NonExistingId_ReturnsNull()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new ClaimRepository(context, null);
                var nonExistingClaimId = Guid.NewGuid();

                // Act
                var retrievedClaim = await repository.GetClaimByIdAsync(nonExistingClaimId);

                // Assert
                Assert.IsNull(retrievedClaim);
            }
        }
    }
}
