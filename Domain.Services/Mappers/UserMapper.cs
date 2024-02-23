namespace Services.Mappers
{
    using System.Collections.Generic;
    using DTO;

    public static class UserMapper
    {
        public static List<User> MapUsersToDto(this List<Domain.Model.User> users) 
        {
            if (users == null)
            {
                return null;
            }

            var usersDto = new List<User>();

            foreach (var user in users)
            {
                usersDto.Add(user.MapToUserDto());
            }

            return usersDto;
        }

        public static User MapToUserDto(this Domain.Model.User user)
        {
            if (user == null)
            {
                return null;
            }

            var userDto = new User
            {
                FullName = user.FullName,
                UserId = user.UserID,
                Username = user.Username
            };

            return userDto;
        }

        public static Domain.Model.User MapUserToDomain(this User user)
        {
            if (user == null)
            {
                return null;
            }

            var userDomain = new Domain.Model.User
            {
                FullName = user.FullName,
                UserID = user.UserId,
                Username = user.Username,
            };

            return userDomain;
        }

        public static UserValidation MapUserToUserValidation(this User user)
        {
            var userValidtion = new UserValidation
            {
                FullName = user.FullName,
                Username = user.Username,
                UserId = user.UserId
                
            };

            return userValidtion;
        }
    }
}
