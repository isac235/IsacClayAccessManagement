namespace Services
{
    using System;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Services.Interfaces;
    using Services.Mappers;

    public class ScopeService : IScopeService
    {
        private readonly IScopeRepository scopeRepository;
        private readonly IDoorRepository doorRepository;

        public ScopeService(IScopeRepository scopeRepository, IDoorRepository doorRepository)
        {
            this.scopeRepository = scopeRepository;
            this.doorRepository = doorRepository;
        }

        public async Task<Scope> CreateScopeAsync(Scope scope, Guid doorId, Guid officeId) 
        {
            var office = await this.doorRepository.GetDoorByOfficeIdAndDoorIdAsync(officeId, doorId).ConfigureAwait(false);

            if (office == null) 
            {
                throw new ValidationException("There is a mismatch between the office and the door to which you are trying to create a scope");
            }

            var scopeDomainModel = scope.MapToScopeDomain();
            scopeDomainModel.DoorID = doorId;

            var result = await this.scopeRepository.CreateScopeAsync(scopeDomainModel).ConfigureAwait(false);

            return result.MapToScopeDto();
        }
    }
}
