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

        public RetrieveUserResponseModel RetrieveUser(string userName, string token)
        {
            _token = token;
            bool isSuccess = TryGetUserFromCache(userName, out RetrieveUserResponseModel user);

            return user;
        }

        /// <summary>
        /// Gets the user from the cache or from github
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool TryGetUserFromCache(string userName, out RetrieveUserResponseModel userResponseModel)
        {
            if (_users.TryGetValue(userName, out User retrieveUser))
            {
                userResponseModel = new RetrieveUserResponseModel() { 
                    Users = new List<User>() { retrieveUser },
                    Status = 200,
                    Message = "Successfully retrieved user from cache"
                };
                return true;
            } else if (GetUserFromGithub(userName, out userResponseModel))
            {
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
        private bool GetUserFromGithub(string userName, out RetrieveUserResponseModel userResponseModel)
        {
            //Send a GET request to the github api with the username and access token
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            _githubApiUrl += userName;
            HttpResponseMessage result = client.GetAsync(_githubApiUrl).Result;

            userResponseModel = new RetrieveUserResponseModel() { Users = new List<User>() };
            userResponseModel.Status = (int)result.StatusCode;
            userResponseModel.Message = result.ReasonPhrase;
            if (result.IsSuccessStatusCode)
            {
                //Parse the response
                string json = result.Content.ReadAsStringAsync().Result;
                User newUser = JsonConvert.DeserializeObject<User>(json);
                _users.Add(userName, newUser);
                userResponseModel.Users.Add(newUser);
                return true;
            }
            return false;
        }
    }
}
