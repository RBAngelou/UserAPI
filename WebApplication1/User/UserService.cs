
namespace WebApplication1.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RetrieveUserResponseModel> RetrieveUsers(RetrieveUserRequestModel userNames)
        {
            RetrieveUserResponseModel getUserResponseModel = await _userRepository.RetrieveUser(userNames.usernames, userNames.bearerToken).ConfigureAwait(false);
            return getUserResponseModel;
        }
    }
}
