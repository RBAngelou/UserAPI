using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebApplication1.User
{
    public class UserRepository : IUserRepository
    {

        private Dictionary<string, User> _users = new Dictionary<string, User>();

        private IUserDataManager _userDataManager;

        

        public UserRepository(IUserDataManager userDataManager)
        {
            _userDataManager = userDataManager;
        }

        public RetrieveUserResponseModel RetrieveUser(List<string> userName, string token)
        {
            RetrieveUserResponseModel retrieveUserResponseModel = new RetrieveUserResponseModel() { Users = new List<User>() }; 
            //Try to get the user from the cache or from github
            if (GetUsers(userName, token, out List<User> retrievedUsers)) 
            {
                retrieveUserResponseModel.Users = retrievedUsers;
                retrieveUserResponseModel.Status = (StatusCodes.Status200OK);
            }
            else
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
        private bool GetUsers(List<string> userNamesList, string token, out List<User> _retrievedUsers)
        {
            _retrievedUsers = new List<User>();
            foreach (string name in userNamesList)
            {
                if (_users.TryGetValue(name, out User retrievedUserFromCache))
                {
                    retrievedUserFromCache.originInfo = "User retrieved from cache";
                    _retrievedUsers.Add(retrievedUserFromCache);
                }else if (_userDataManager.TryGetUser(name, token, out User retrievedUserFromManager))
                {
                    //Add the user to the cache
                    _users.Add(name, retrievedUserFromManager);
                    //Add the user to the list of retrieved users
                    _retrievedUsers.Add(retrievedUserFromManager);
                }
            }
            return true;
        }


    }
}
