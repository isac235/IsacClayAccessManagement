namespace DTO
{
    using System;

    public class AccessEvent
    {
        public Guid EventId { get; set; }

        public Guid UserId { get; set; }

        public Guid DoorId { get; set; }

        public DateTime EventTime { get; set; }

        public AccessMethod AccessMethod { get; set; }

        public string AccessResult { get; set; }
    }
}
