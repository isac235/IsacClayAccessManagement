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

    public class AccessEventRepository : IAccessEventRepository
    {
        private readonly OfficesAccessDbContext dbContext;

        public AccessEventRepository(OfficesAccessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<AccessEvent>> GetAllAccessEventsAsync()
        {
            try
            {
                return await this.dbContext.AccessEvents.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
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
                throw new RepositoryException(nameof(CreateAccessEventAsync), ex.Message, ex);
            }
        }
    }
}