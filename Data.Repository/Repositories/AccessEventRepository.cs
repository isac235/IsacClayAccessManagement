namespace Data.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Domain.Model;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class AccessEventRepository : IAccessEventRepository
    {
        private readonly OfficesAccessDbContext dbContext;
        private readonly ILogger<AccessEventRepository> logger;

        public AccessEventRepository(OfficesAccessDbContext dbContext, ILogger<AccessEventRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<List<AccessEvent>> GetAllAccessEventsAsync()
        {
            try
            {
                return await this.dbContext.AccessEvents.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting all access events.");
                throw new RepositoryException(nameof(GetAllAccessEventsAsync), ex.Message, ex);
            }
        }

        public async Task<List<AccessEvent>> GetAllAccessEventsByDoorId(Guid doorId)
        {
            try
            {
                return await this.dbContext.AccessEvents.Where(x => x.DoorID == doorId).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting all access events by doorId.");
                throw new RepositoryException(nameof(GetAllAccessEventsByDoorId), ex.Message, ex);
            }
        }

        public async Task<AccessEvent> CreateAccessEventAsync(AccessEvent newAccessEvent)
        {
            try
            {
                await this.dbContext.AccessEvents.AddAsync(newAccessEvent).ConfigureAwait(false);
                await this.dbContext.SaveChangesAsync().ConfigureAwait(false) ;

                return newAccessEvent;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while creating access event");
                throw new RepositoryException(nameof(CreateAccessEventAsync), ex.Message, ex);
            }
        }
    }
}