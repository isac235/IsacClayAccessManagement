namespace Data.Repository.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Repository.Repositories;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AccessEventRepositoryTests
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
        public async Task CreateAccessEventAsync_ValidEvent_AddsEvent()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new AccessEventRepository(context);
                var newEvent = new AccessEvent { EventID = Guid.NewGuid(), EventTime = DateTime.Now };

                // Act
                var addedEvent = await repository.CreateAccessEventAsync(newEvent);

                // Assert
                Assert.IsNotNull(addedEvent);

                // Check if event is in the database
                var eventFromDb = await context.AccessEvents.FindAsync(addedEvent.EventID);
                Assert.IsNotNull(eventFromDb);
            }
        }

        [TestMethod]
        public async Task GetAllAccessEventsAsync_ReturnsAllEvents()
        {
            using (var context = new OfficesAccessDbContext(_options))
            {
                // Arrange
                var repository = new AccessEventRepository(context);
                var eventsToAdd = new List<AccessEvent>
                {
                    new AccessEvent { EventID = Guid.NewGuid(), EventTime = DateTime.Now },
                    new AccessEvent { EventID = Guid.NewGuid(), EventTime = DateTime.Now }
                };
                await context.AccessEvents.AddRangeAsync(eventsToAdd);
                await context.SaveChangesAsync();

                // Act
                var retrievedEvents = await repository.GetAllAccessEventsAsync();

                // Assert
                Assert.IsNotNull(retrievedEvents);
            }
        }
    }
}
