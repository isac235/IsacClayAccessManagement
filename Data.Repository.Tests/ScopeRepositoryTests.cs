namespace Data.Repository.Tests
{
    using Data.Repository.Repositories;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class ScopeRepositoryTests
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
                var doorId = Guid.NewGuid();
                var scope = new Scope { ScopeID = Guid.NewGuid(), DoorID = doorId, ScopeName = "Scope1" };

                context.Scopes.Add(scope);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task CreateScopeAsync_ValidScope_AddsScope()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new ScopeRepository(context, null);
                var newScope = new Scope { ScopeID = Guid.NewGuid(), DoorID = Guid.NewGuid(), ScopeName = "NewScope" };

                // Act
                var addedScope = await repository.CreateScopeAsync(newScope);

                // Assert
                Assert.IsNotNull(addedScope);

                // Check if scope is in the database
                var scopeFromDb = await context.Scopes.FindAsync(addedScope.ScopeID);
                Assert.IsNotNull(scopeFromDb);
            }
        }

        [TestMethod]
        public async Task GetScopesByDoorIdAsync_ExistingDoorId_ReturnsScopes()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new ScopeRepository(context, null);
                var doorId = (await context.Scopes.FirstAsync()).DoorID;

                // Act
                var scopes = await repository.GetScopesByDoorIdAsync(doorId);

                // Assert
                Assert.AreEqual(1, scopes.Count);
            }
        }
    }
}
