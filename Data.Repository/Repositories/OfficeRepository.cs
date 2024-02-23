namespace Data.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Domain.Model;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.Extensions.Logging;

    public class OfficeRepository : IOfficeRepository
    {
        private readonly OfficesAccessDbContext dbContext;
        private readonly ILogger<OfficeRepository> logger;

        public OfficeRepository(OfficesAccessDbContext dbContext, ILogger<OfficeRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<Office> GetOfficeByIdAsync(Guid officeId)
        {
            try
            {
                return await this.dbContext.Offices.FindAsync(officeId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while getting office by id.");
                throw new RepositoryException(nameof(GetOfficeByIdAsync), ex.Message, ex);
            }
        }

        public async Task<Office> CreateOfficeAsync(Office newOffice)
        {
            try
            {
                await this .dbContext.Offices.AddAsync(newOffice).ConfigureAwait(false);
                await this.dbContext.SaveChangesAsync().ConfigureAwait(false);

                return newOffice;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while creating office");
                throw new RepositoryException(nameof(CreateOfficeAsync), ex.Message, ex);
            }
        }
    }
}