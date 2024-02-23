namespace Services.Mappers
{
    using DTO;

    public static class UserRoleMappingMapper
    {
        public static UserRoleMapping MapToUserRoleMappingDto(this Domain.Model.UserRoleMapping userRoleMapping)
        {
            if (userRoleMapping == null)
            {
                return null;
            }

            var userRoleMappingDto = new UserRoleMapping
            {
                RoleId = userRoleMapping.RoleID,
                UserId = userRoleMapping.UserID,
                UserRoleId = userRoleMapping.UserRoleID
            };

            return userRoleMappingDto;
        }

        public static Domain.Model.UserRoleMapping MapToUserRoleMappingDomain(this UserRoleMapping userRoleMapping)
        {
            if (userRoleMapping == null)
            {
                return null;
            }

            var userRoleMappingDomain = new Domain.Model.UserRoleMapping
            {
                RoleID = userRoleMapping.RoleId,
                UserID = userRoleMapping.UserId,
                UserRoleID = userRoleMapping.UserRoleId
            };

            return userRoleMappingDomain;
        }
    }
}
