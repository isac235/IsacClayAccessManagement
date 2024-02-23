namespace Data.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Domain.Model;
    using Infrastructure.CrossCutting.CustomExceptions;

    public class OfficeRepository : IOfficeRepository
    {
        private readonly OfficesAccessDbContext dbContext;

        public OfficeRepository(OfficesAccessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Office> GetOfficeByIdAsync(Guid officeId)
        {
            try
            {
                return await this.dbContext.Offices.FindAsync(officeId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
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
                throw new RepositoryException(nameof(CreateOfficeAsync), ex.Message, ex);
            }
        }
    }
}