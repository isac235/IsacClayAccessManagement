namespace Services.Interfaces
{
    using System.Threading.Tasks;
    using DTO;

    public interface IRoleService
    {
        Task<Role> CreateRoleAsync(Role role);
    }
}
