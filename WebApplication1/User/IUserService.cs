namespace WebApplication1.User
{
    public interface IUserService
    {
        public Task<RetrieveUserResponseModel> RetrieveUsers(RetrieveUserRequestModel userNames);
    }
}
