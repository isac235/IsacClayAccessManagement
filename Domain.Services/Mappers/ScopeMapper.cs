namespace Services.Mappers
{
    using DTO;

    public static class ScopeMapper
    {
        public static Scope MapToScopeDto(this Domain.Model.Scope scope)
        {
            if (scope == null)
            {
                return null;
            }

            var scopeDto = new Scope
            {
                Description = scope.Description,
                ScopeId = scope.ScopeID,
                ScopeName = scope.ScopeName,
                RoleId = scope.RoleID
            };

            return scopeDto;
        }

        public static Domain.Model.Scope MapToScopeDomain(this Scope scope)
        {
            if (scope == null)
            {
                return null;
            }

            var scopeDto = new Domain.Model.Scope
            {
                ScopeName = scope.ScopeName,
                Description = scope.Description,
                ScopeID = scope.ScopeId,
                RoleID = scope.RoleId
            };

            return scopeDto;
        }
    }
}
