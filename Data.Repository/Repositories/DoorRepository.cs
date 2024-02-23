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

    public class DoorRepository : IDoorRepository
    {
        private readonly OfficesAccessDbContext dbContext;

        public DoorRepository(OfficesAccessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Door>> GetDoorsByOfficeIdAsync(Guid officeId)
        {
            try
            {
                return await this.dbContext.Doors.Where(x => x.OfficeID == officeId).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
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
                throw new RepositoryException(nameof(CreateDoorAsync), ex.Message, ex);
            }
        }
    }
}
