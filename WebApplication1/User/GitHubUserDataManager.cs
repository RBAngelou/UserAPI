using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebApplication1.User
{
    public class GitHubUserDataManager : IUserDataManager
    {
        /// <summary>
        /// the url for the github api
        /// </summary>
        private string _githubApiUrl = "https://api.github.com/users/";

        /// <summary>
        /// Retrieves the user from github, calls the github api
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool TryGetUser(string userName, string token, out User newUser)
        {
            //Send a GET request to the github api with the username and access token
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
            _githubApiUrl += userName;
            client.Timeout = TimeSpan.FromSeconds(3); //time out after 3 seconds
            client.BaseAddress = new System.Uri(_githubApiUrl);

            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact;

            HttpResponseMessage result = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, _githubApiUrl)).Result;
            //HttpResponseMessage result = client.GetAsync(_githubApiUrl).Result;
            if (result.IsSuccessStatusCode)
            {
                //Parse the response
                string json = result.Content.ReadAsStringAsync().Result;
                newUser = JsonConvert.DeserializeObject<User>(json);
                newUser.originInfo = "User retrieved from github";
                return true;
            }
            newUser = null;
            return false;
        }
    }
}
