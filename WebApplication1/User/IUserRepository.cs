namespace WebApplication1.User
{
    public interface IUserRepository
    {
        public Task<RetrieveUserResponseModel> RetrieveUser(List<string> userName, string token);
    }
}
