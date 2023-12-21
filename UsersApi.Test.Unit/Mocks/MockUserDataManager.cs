using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.User;

namespace UsersApi.Test.Unit.Mocks
{
    public class MockUserDataManager : IUserDataManager
    {
        public bool TryGetUser(string userName, string token, out User user)
        {
            user = new User() { 
                name = userName,
                login = $"test-{userName}",
                numOfFollowers = 1,
                numOfPubRepo = 7,
                originInfo = "User retrieved from mock" };
            return true;
        }
    }
}
