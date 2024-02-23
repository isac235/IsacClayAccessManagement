namespace Services.Mappers
{
    using System.Collections.Generic;
    using DTO;

    public static class AccessEventMapper
    {
        public static List<AccessEvent> MapToAccessEventsDto(this List<Domain.Model.AccessEvent> accessEvents)
        {
            if (accessEvents == null)
            {
                return null;
            }

            if (accessEvents.Count == 0)
            {
                return new List<AccessEvent>();
            }

            var accessEventsDto = new List<AccessEvent>();

            foreach (var accessEvent in accessEvents)
            {
                accessEventsDto.Add(accessEvent.MapToAccessEventDto());
            }

            return accessEventsDto;
        }

        public static AccessEvent MapToAccessEventDto(this Domain.Model.AccessEvent accessEvent)
        {
            if (accessEvent == null)
            {
                return null;
            }

            var accessEventDto = new AccessEvent
            {
                DoorId = accessEvent.DoorID,
                EventId = accessEvent.EventID,
                EventTime = accessEvent.EventTime,
                UserId = accessEvent.UserID,
                AccessMethod = GetAccessMethod(accessEvent.AccessMethod),
                AccessResult = accessEvent.AccessResult
            };

            return accessEventDto;
        }

        public static Domain.Model.AccessEvent MapToAccessEventDomain(this AccessEvent accessEvent)
        {
            if (accessEvent == null)
            {
                return null;
            }

            var accessEventDomain = new Domain.Model.AccessEvent
            {
                AccessMethod = accessEvent.AccessMethod.ToString(),
                DoorID = accessEvent.DoorId,
                EventID = accessEvent.EventId,
                UserID = accessEvent.UserId,
                EventTime = accessEvent.EventTime,
            };

            return accessEventDomain;
        }

        private static AccessMethod GetAccessMethod(string accessMethod)
        {
            switch (accessMethod)
            {
                case "Tag": return AccessMethod.Tag;

                default:
                    return AccessMethod.Remote;
            }
        }
    }
}
