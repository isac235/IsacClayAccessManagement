namespace Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTO;

    public interface IAccessEventService
    {
        Task<List<AccessEvent>> GetAccessEventsByOfficeIdAsync(Guid officeId);

        Task<AccessEventResult> AccessEventAsync(AccessEvent accessEvent, Guid officeId);
    }
}
