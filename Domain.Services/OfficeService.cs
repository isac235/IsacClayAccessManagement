namespace Services
{
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Services.Interfaces;
    using Services.Mappers;
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository officeRepository;

        public OfficeService(IOfficeRepository officeRepository) 
        {
            this.officeRepository = officeRepository;
        }
        public async Task<Office> CreateOfficeAsync(Office newOfficeDTO)
        {
            var newOffice = newOfficeDTO.MapToOfficeDomain();

            var office = await this.officeRepository.CreateOfficeAsync(newOffice).ConfigureAwait(false);

            return office.MapToOfficeDto();
        }
    }
}
