namespace Data.Repository.Tests
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Repositories;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OfficeRepositoryTests
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
        public async Task CreateOfficeAsync_ValidOffice_AddsOffice()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new OfficeRepository(context);
                var newOffice = new Office { OfficeID = Guid.NewGuid(), OfficeName = "New Office" };

                // Act
                var addedOffice = await repository.CreateOfficeAsync(newOffice);

                // Assert
                Assert.IsNotNull(addedOffice);

                // Check if office is in the database
                var officeFromDb = await context.Offices.FindAsync(addedOffice.OfficeID);
                Assert.IsNotNull(officeFromDb);
            }
        }

        [TestMethod]
        public async Task GetOfficeByIdAsync_ExistingId_ReturnsOffice()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new OfficeRepository(context);
                var existingOffice = new Office { OfficeID = Guid.NewGuid(), OfficeName = "Existing Office" };
                await context.Offices.AddAsync(existingOffice);
                await context.SaveChangesAsync();

                // Act
                var retrievedOffice = await repository.GetOfficeByIdAsync(existingOffice.OfficeID);

                // Assert
                Assert.IsNotNull(retrievedOffice);
                Assert.AreEqual(existingOffice.OfficeID, retrievedOffice.OfficeID);
            }
        }

        [TestMethod]
        public async Task GetOfficeByIdAsync_NonExistingId_ReturnsNull()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new OfficeRepository(context);
                var nonExistingId = Guid.NewGuid();

                // Act
                var retrievedOffice = await repository.GetOfficeByIdAsync(nonExistingId);

                // Assert
                Assert.IsNull(retrievedOffice);
            }
        }
    }
}
