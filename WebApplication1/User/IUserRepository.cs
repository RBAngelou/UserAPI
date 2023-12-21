namespace WebApplication1.User
{
    public interface IUserRepository
    {
        public RetrieveUserResponseModel RetrieveUser(string userName, string token);
    }
}
