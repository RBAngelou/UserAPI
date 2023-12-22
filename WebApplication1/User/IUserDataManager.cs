namespace WebApplication1.User
{
    public interface IUserDataManager
    {
        public Task<User> TryGetUser(string userName, string token);
    }
}
