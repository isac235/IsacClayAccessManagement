namespace Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using DTO;

    public interface IScopeService
    {
        Task<Scope> CreateScopeAsync(Scope scope, Guid doorId, Guid officeId);
    }
}
