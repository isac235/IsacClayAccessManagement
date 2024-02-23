namespace Services.Mappers
{
    using DTO;

    public static class RoleMapper
    {
        public static Role MapToRoleDto(this Domain.Model.Role role)
        {
            if (role == null)
            {
                return null;
            }

            var roleDto = new Role
            {
                RoleId = role.RoleID,
                RoleName = role.RoleName
            };

            return roleDto;
        }

        public static Domain.Model.Role MapToRoleDomain(this Role role)
        {
            if (role == null)
            {
                return null;
            }

            var roleDto = new Domain.Model.Role
            {
                RoleID = role.RoleId,
                RoleName = role.RoleName
            };

            return roleDto;
        }
    }
}
