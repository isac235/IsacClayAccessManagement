namespace Data.Repository.Tests
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Repositories;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class DoorRepositoryTests
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
        public async Task CreateDoorAsync_ValidDoor_AddsDoor()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new DoorRepository(context, null);
                var newDoor = new Door { DoorID = Guid.NewGuid(), OfficeID = Guid.NewGuid(), DoorName = "New Door" };

                // Act
                var addedDoor = await repository.CreateDoorAsync(newDoor);

                // Assert
                Assert.IsNotNull(addedDoor);

                // Check if door is in the database
                var doorFromDb = await context.Doors.FindAsync(addedDoor.DoorID);
                Assert.IsNotNull(doorFromDb);
            }
        }

        [TestMethod]
        public async Task GetDoorByOfficeIdAndDoorIdAsync_ExistingIds_ReturnsDoor()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new DoorRepository(context, null);
                var officeId = Guid.NewGuid();
                var doorId = Guid.NewGuid();
                var existingDoor = new Door { DoorID = doorId, OfficeID = officeId, DoorName = "Existing Door" };
                await context.Doors.AddAsync(existingDoor);
                await context.SaveChangesAsync();

                // Act
                var retrievedDoor = await repository.GetDoorByOfficeIdAndDoorIdAsync(officeId, doorId);

                // Assert
                Assert.IsNotNull(retrievedDoor);
                Assert.AreEqual(existingDoor.DoorID, retrievedDoor.DoorID);
                Assert.AreEqual(existingDoor.OfficeID, retrievedDoor.OfficeID);
            }
        }

        [TestMethod]
        public async Task GetDoorByOfficeIdAndDoorIdAsync_NonExistingIds_ReturnsNull()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new DoorRepository(context, null);
                var nonExistingOfficeId = Guid.NewGuid();
                var nonExistingDoorId = Guid.NewGuid();

                // Act
                var retrievedDoor = await repository.GetDoorByOfficeIdAndDoorIdAsync(nonExistingOfficeId, nonExistingDoorId);

                // Assert
                Assert.IsNull(retrievedDoor);
            }
        }
    }
}
