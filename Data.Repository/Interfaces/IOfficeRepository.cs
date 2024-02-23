namespace Data.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Domain.Model;

    public interface IOfficeRepository
    {
        Task<Office> GetOfficeByIdAsync(Guid officeId);

        Task<Office> CreateOfficeAsync(Office newOffice);
    }
}