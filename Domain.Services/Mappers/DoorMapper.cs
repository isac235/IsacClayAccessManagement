namespace Services.Mappers
{
    using DTO;

    public static class DoorMapper
    {
        public static Door MapToDoorDto(this Domain.Model.Door door)
        {
            if (door == null)
            {
                return null;
            }

            var doorDto = new Door
            {
                DoorId = door.DoorID,
                DoorName = door.DoorName,
                Location = door.Location,
            };

            return doorDto;
        }

        public static Domain.Model.Door MapToDoorDomain(this Door door)
        {
            if (door == null)
            {
                return null;
            }

            var doorDto = new Domain.Model.Door
            {
                DoorID = door.DoorId,
                Location = door.Location,
                DoorName = door.DoorName
            };

            return doorDto;
        }
    }
}
