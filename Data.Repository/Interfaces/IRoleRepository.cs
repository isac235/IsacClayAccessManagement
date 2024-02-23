namespace Data.Repository.Interfaces
{
    using System.Threading.Tasks;
    using Domain.Model;

    public interface IRoleRepository
    {
        Task<Role> CreateRoleAsync(Role newRole);
    }
}
