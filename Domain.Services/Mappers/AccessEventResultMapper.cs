namespace Services.Mappers
{
    using DTO;

    public static class AccessEventResultMapper
    {
        public static AccessEventResult MapToAccesEventResult(this Domain.Model.AccessEvent accessEvent)
        {
            if (accessEvent == null)
            {
                return null;
            }

            var accessEventResult = new AccessEventResult
            {
                AccessEventId = accessEvent.EventID,
                AccessEventAuthorization = GetAccessEventAuthorization(accessEvent.AccessResult)
            };

            return accessEventResult;
        }

        private static AccessEventAuthorization GetAccessEventAuthorization(string accessResult)
        {
            switch (accessResult)
            {
                case "Authorized": return AccessEventAuthorization.Authorized;

                default:
                    return AccessEventAuthorization.Unauthorized;
            }
        }
    }
}
