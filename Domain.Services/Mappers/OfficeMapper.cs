namespace Services.Mappers
{
    using DTO;

    public static class OfficeMapper
    {
        public static Office MapToOfficeDto(this Domain.Model.Office office)
        {
            if (office == null)
            {
                return null;
            }

            var officeDTO = new Office
            {
                Location = office.Location,
                OfficeID = office.OfficeID,
                OfficeName = office.OfficeName
            };

            return officeDTO;
        }

        public static Domain.Model.Office MapToOfficeDomain(this Office office)
        {
            if (office == null)
            {
                return null;
            }

            var officeDomain = new Domain.Model.Office
            {
                Location = office.Location,
                OfficeID = office.OfficeID,
                OfficeName = office.OfficeName
            };

            return officeDomain;
        }
    }
}
