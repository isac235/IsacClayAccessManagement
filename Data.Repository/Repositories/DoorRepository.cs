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

    public class DoorRepository : IDoorRepository
    {
        private readonly OfficesAccessDbContext dbContext;
        private readonly ILogger<DoorRepository> logger;

        public DoorRepository(OfficesAccessDbContext dbContext, ILogger<DoorRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<List<Door>> GetDoorsByOfficeIdAsync(Guid officeId)
        {
            try
            {
                return await this.dbContext.Doors.Where(x => x.OfficeID == officeId).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting doors by officeId");
                throw new RepositoryException(nameof(GetDoorsByOfficeIdAsync), ex.Message, ex);
            }
        }

        public async Task<Door> GetDoorByOfficeIdAndDoorIdAsync(Guid officeId, Guid doorId)
        {
            try
            {
               return await this.dbContext.Doors.FirstOrDefaultAsync(x => x.OfficeID == officeId && x.DoorID == doorId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting door by office and door id.");
                throw new RepositoryException(nameof(GetDoorByOfficeIdAndDoorIdAsync), ex.Message, ex);
            }
        }

        public async Task<Door> CreateDoorAsync(Door newDoor)
        {
            try
            {
                await this.dbContext.Doors.AddAsync(newDoor).ConfigureAwait(false);
                await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

                return newDoor;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while creating door.");
                throw new RepositoryException(nameof(CreateDoorAsync), ex.Message, ex);
            }
        }
    }
}
