
namespace WebApplication1.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public RetrieveUserResponseModel RetrieveUsers(RetrieveUserRequestModel userNames)
        {
            RetrieveUserResponseModel getUserResponseModel = _userRepository.RetrieveUser(userNames.usernames, userNames.bearerToken);
            return getUserResponseModel;
        }
    }
}
