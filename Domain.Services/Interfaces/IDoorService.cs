namespace Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using DTO;
    public interface IDoorService
    {
        Task<Door> CreateDoorAsync(Door door, Guid officeId);
    }
}
