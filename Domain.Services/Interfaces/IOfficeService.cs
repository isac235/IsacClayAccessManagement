namespace Services.Interfaces
{
    using System.Threading.Tasks;
    using DTO;

    public interface IOfficeService
    {
        Task<Office> CreateOfficeAsync(Office newOfficeDTO);
    }
}
