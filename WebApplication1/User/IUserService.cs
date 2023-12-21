namespace WebApplication1.User
{
    public interface IUserService
    {
        public RetrieveUserResponseModel RetrieveUsers(RetrieveUserRequestModel userNames);
    }
}
