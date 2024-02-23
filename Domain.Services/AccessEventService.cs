namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using DTO;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Services.Interfaces;
    using Services.Mappers;

    public class AccessEventService : IAccessEventService
    {
        private readonly IAccessEventRepository accessEventRepository;
        private readonly IDoorRepository doorRepository;
        private readonly IUserRoleMappingRepository userRoleMappingRepository;
        private readonly IScopeRepository scopeRepository;

        public AccessEventService(
            IAccessEventRepository accessEventRepository,
            IDoorRepository doorRepository,
            IUserRoleMappingRepository userRoleMappingRepository,
            IScopeRepository scopeRepository)
        {
            this.accessEventRepository = accessEventRepository;
            this.doorRepository = doorRepository;
            this.userRoleMappingRepository = userRoleMappingRepository;
            this.scopeRepository = scopeRepository;
        }

        public async Task<List<AccessEvent>> GetAccessEventsByOfficeIdAsync(Guid officeId) 
        {
            var doors = await this.doorRepository.GetDoorsByOfficeIdAsync(officeId).ConfigureAwait(false);
            var result = new List<Domain.Model.AccessEvent>();

            foreach (var door in doors)
            {
                var events =  await this.accessEventRepository.GetAllAccessEventsByDoorId(door.DoorID).ConfigureAwait(false);
                result.AddRange(events);
            }

            return result.MapToAccessEventsDto();
        }

        public async Task<AccessEventResult> AccessEventAsync(AccessEvent accessEvent, Guid officeId)
        {
            var door = await this.doorRepository.GetDoorByOfficeIdAndDoorIdAsync(officeId, accessEvent.DoorId).ConfigureAwait(false);

            if (door == null)
            {
                throw new ValidationException("The office door you are trying to access does not exist!");
            }

            var userRoles = await this.userRoleMappingRepository.GetUserRoleMappingByUserIdAsync(accessEvent.UserId).ConfigureAwait(false);

            if (userRoles == null || !userRoles.Any())
            {
                throw new ValidationException("Your user does not have roles associated!");
            }

            var scopesByDoor = await this.scopeRepository.GetScopesByDoorIdAsync(accessEvent.DoorId).ConfigureAwait(false);

            if (scopesByDoor == null || !scopesByDoor.Any())
            {
                throw new ValidationException("The door you are trying to access does not have scopes associated!");
            }

            var rolesIntersection = userRoles.Select(x => x.RoleID).ToList().Intersect(scopesByDoor.Select(x => x.RoleID).ToList());
            
            var accesEventDomain = accessEvent.MapToAccessEventDomain();

            accesEventDomain.AccessResult = "Authorized";

            if (rolesIntersection == null || rolesIntersection.Count() == 0)
            {
                accesEventDomain.AccessResult = "Unauthorized";
            }

            var result = await this.accessEventRepository.CreateAccessEventAsync(accesEventDomain).ConfigureAwait(false);
            
            return result.MapToAccesEventResult();
        }
    }
}
