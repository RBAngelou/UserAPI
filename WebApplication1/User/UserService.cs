﻿
namespace WebApplication1.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public RetrieveUserResponseModel RetrieveUsers(RetrieveUserRequestModel userNames, string token)
        {
            User retrievedUser = _userRepository.RetrieveUser(userNames.Usernames.FirstOrDefault(), token);

            List<User> users = [retrievedUser];

            RetrieveUserResponseModel getUserResponseModel = new RetrieveUserResponseModel() { Users = users };

            return getUserResponseModel;
        }
    }
}
