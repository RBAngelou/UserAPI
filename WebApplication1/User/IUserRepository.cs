namespace WebApplication1.User
{
    public interface IUserRepository
    {
        public RetrieveUserResponseModel RetrieveUser(List<string> userName, string token);
    }
}
