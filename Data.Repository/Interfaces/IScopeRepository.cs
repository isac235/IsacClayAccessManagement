namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Model;

    public interface IScopeRepository
    {
        Task<Scope> CreateScopeAsync(Scope scope);

        Task<List<Scope>> GetScopesByDoorIdAsync(Guid doorId);
    }
}
