namespace WebApplication1.User
{
    public interface IUserDataManager
    {
        public bool TryGetUser(string userName, string token, out User user);
    }
}
