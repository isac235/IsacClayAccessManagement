namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Model;

    public interface IDoorRepository
    {
        Task<List<Door>> GetDoorsByOfficeIdAsync(Guid officeId);

        Task<Door> GetDoorByOfficeIdAndDoorIdAsync(Guid officeId, Guid doorId);

        Task<Door> CreateDoorAsync(Door newDoor);
    }
}