using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebApplication1.User
{
    public class UserRepository : IUserRepository
    {

        /// <summary>
        /// replace with memory cache
        /// </summary>
        private Dictionary<string, User> _missedUsers = new Dictionary<string, User>();

        private IUserDataManager _userDataManager;
        private IMemoryCache _cache;

        

        public UserRepository(IUserDataManager userDataManager)
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _userDataManager = userDataManager;
        }

        public async Task<RetrieveUserResponseModel> RetrieveUser(List<string> userName, string token)
        {
            RetrieveUserResponseModel retrieveUserResponseModel = new RetrieveUserResponseModel() { Users = new List<User>() }; 
            //Try to get the user from the cache or from github

            retrieveUserResponseModel.Users = await GetUsersAsync(userName, token).ConfigureAwait(false);

            retrieveUserResponseModel.Status = (StatusCodes.Status200OK);
          
            
            if (retrieveUserResponseModel.Users.Count == 0)
            {
                retrieveUserResponseModel.Status = (StatusCodes.Status500InternalServerError);
                retrieveUserResponseModel.Message = "Please contact admin support";
            }
            
            return retrieveUserResponseModel;
        }

        /// <summary>
        /// Gets the user from the cache or from github
        /// </summary>
        /// <param name="userNamesList"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<List<User>> GetUsersAsync(List<string> userNamesList, string token)
        {
            List<User> _retrievedUsers = new List<User>();
            foreach (string name in userNamesList)
            {
                if (_cache.TryGetValue(name, out User retrievedUserFromCache))
                {
                    retrievedUserFromCache.originInfo = "User retrieved from cache";
                    _retrievedUsers.Add(retrievedUserFromCache);
                }else
                {
                    User retrievedUserFromManager = await _userDataManager.TryGetUser(name, token);

                    if (retrievedUserFromManager is null)
                    {
                        _missedUsers.Add(name, retrievedUserFromManager);
                    }
                    else
                    {
                        //Add the user to the cache
                        _cache.Set(name, retrievedUserFromManager);

                        //Add the user to the list of retrieved users
                        _retrievedUsers.Add(retrievedUserFromManager);
                    }
                    
                }
            }
            return _retrievedUsers;
        }

        public int GetMissedUsersCount()
        {
            return _missedUsers.Count;  
        }

    }
}
