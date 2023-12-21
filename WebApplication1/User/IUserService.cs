namespace WebApplication1.User
{
    public interface IUserService
    {
        public GetUserResponseModel GetUsers(GetUserRequestModel userNames);
    }
}
