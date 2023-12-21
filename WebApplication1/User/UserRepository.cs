using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebApplication1.User
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// the url for the github api
        /// </summary>
        private string _githubApiUrl = "https://api.github.com/users/";
        private Dictionary<string, User> _users = new Dictionary<string, User>();
        private string _token;

        public User RetrieveUser(string userName, string token)
        {
            _token = token;
            return TryGetUserFromCache(userName, out User user) ? user : null;
        }

        /// <summary>
        /// Gets the user from the cache or from github
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool TryGetUserFromCache(string userName, out User user)
        {
            if (_users.TryGetValue(userName, out user))
            {
                return true;
            } else if (GetUserFromGithub(userName, out user))
            {
                _users.Add(userName, user);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retrieves the user from github, calls the github api
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool GetUserFromGithub(string userName, out User user)
        {
            //Send a GET request to the github api with the username and access token
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            _githubApiUrl += userName;
            HttpResponseMessage result = client.GetAsync(_githubApiUrl).Result;

            if (result.IsSuccessStatusCode)
            {
                //Parse the response
                string json = result.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(json);
                return true;
            }
            else
            {
                user = null;
            }

            return false;
        }
    }
}
