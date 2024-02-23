namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Model;

    public interface IAccessEventRepository
    {
        Task<List<AccessEvent>> GetAllAccessEventsAsync();

        Task<List<AccessEvent>> GetAllAccessEventsByDoorId(Guid doorId);

        Task<AccessEvent> CreateAccessEventAsync(AccessEvent newAccessEvent);
    }
}
