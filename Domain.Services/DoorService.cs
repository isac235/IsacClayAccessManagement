namespace Services
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Services.Interfaces;
    using Services.Mappers;

    public class DoorService : IDoorService
    {
        private readonly IDoorRepository doorRepository;

        public DoorService(IDoorRepository doorRepository)
        {
            this.doorRepository = doorRepository;
        }

        public async Task<Door> CreateDoorAsync(Door door, Guid OfficeId) 
        {
            var doorDomain = door.MapToDoorDomain();
            doorDomain.OfficeID = OfficeId;
            
            var result  = await this.doorRepository.CreateDoorAsync(doorDomain).ConfigureAwait(false);

            return result.MapToDoorDto();
        }
    }
}
