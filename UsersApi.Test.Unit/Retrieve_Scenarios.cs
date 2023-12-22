using UsersApi.Test.Unit.Mocks;
using WebApplication1.User;

namespace UsersApi.Test.Unit
{
    public class Tests
    {
        List<User> _expectedUsers_set1;
        List<User> _expectedUsers_set2;
        List<User> _expectedUsers_set3;
        List<User> _expectedUsers_set4;
        MockUserDataManager _mockUserDataManager;
        UserRepository _userRepository;
        [SetUp]
        public void Setup()
        {
            PrepareExpectedData();
            _mockUserDataManager = new MockUserDataManager();
            _userRepository = new UserRepository(_mockUserDataManager);
        }

        private void PrepareExpectedData()
        {
            _expectedUsers_set1 = new List<User>() {
                new User() {
                    name = "test",
                    login = "test-test",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from mock" }
            };

            _expectedUsers_set2 = new List<User>() {
                new User() {
                    name = "test",
                    login = "test-test",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from mock" },
                new User() {
                    name = "testUser2",
                    login = "test-testUser2",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from mock" },
                new User() {
                    name = "testUser3",
                    login = "test-testUser3",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from mock" }
            };

            _expectedUsers_set3 = new List<User>() {
                new User() {
                    name = "test",
                    login = "test-test",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from cache" },
                new User() {
                    name = "testUser2",
                    login = "test-testUser2",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from cache" },
                new User() {
                    name = "testUser3",
                    login = "test-testUser3",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from cache" }
            };

            _expectedUsers_set4 = new List<User>() {
                new User() {
                    name = "test",
                    login = "test-test",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from cache" },
                new User() {
                    name = "testUser2",
                    login = "test-testUser2",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from cache" },
                new User() {
                    name = "testUser3",
                    login = "test-testUser3",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from cache" },
                new User() {
                    name = "testUser4",
                    login = "test-testUser4",
                    numOfFollowers = 1,
                    numOfPubRepo = 7,
                    originInfo = "User retrieved from mock" }
            };
        }

        /// <summary>
        /// Step 1: Send a request to retrieve a single user from Data Manager
        /// Step 2: Assert that the user is retrieved from Data Manager
        /// </summary>
        [Test]
        public async Task RetrieveOneUserAsync()
        {
            RetrieveUserResponseModel result = await _userRepository.RetrieveUser(new List<string>() { "test" }, "test").ConfigureAwait(false);

            AssertUsers(_expectedUsers_set1, result.Users);
            Assert.Pass();
        }

        /// <summary>
        /// Step 1: Send request to Retrieve users from Data Manager
        /// Step 2: All users should be retrieved from Data Manager
        /// </summary>
        [Test]
        public async Task RetrieveMultipleUsers()
        {
            RetrieveUserResponseModel result = await _userRepository.RetrieveUser(new List<string>() { "test", "testUser2", "testUser3" }, "test").ConfigureAwait(false);

            AssertUsers(_expectedUsers_set2, result.Users);
            Assert.Pass();
        }


        /// <summary>
        /// Step 1: Send request to Retrieve users from Data Manager
        /// Step 2: Assert users are retrieved from Data Manager
        /// Step 3: Send request to Retrieve users from cache
        /// Step 4: Assert that users are retrieved from cache
        /// </summary>
        [Test]
        public async Task RetrieveUsersFromCache()
        {
            RetrieveUserResponseModel result = await _userRepository.RetrieveUser(new List<string>() { "test", "testUser2", "testUser3" }, "test").ConfigureAwait(false);
            AssertUsers(_expectedUsers_set2, result.Users);

            //All Users should have been retrieved from cache at this point
            RetrieveUserResponseModel result2 = await _userRepository.RetrieveUser(new List<string>() { "test", "testUser2", "testUser3" }, "test").ConfigureAwait(false);
            AssertUsers(_expectedUsers_set3, result2.Users);
            
        }

        /// <summary>
        /// Step 1: Call RetrieveUsersFromCache() Test Method to achieve retriving users from cache
        /// Step 2: Send request to all users (including a new user that needs to be retrived from Data Manager) 
        /// Step 3: Assert that all users are retrieved (total 4)
        /// </summary>
        [Test]
        public async Task RetrieveUsersFromDataManagerAndCache()
        {
            RetrieveUsersFromCache();

            ///The new user "testUser4" should be retrieved from Data Manager
            RetrieveUserResponseModel result = await _userRepository.RetrieveUser(new List<string>() { "test", "testUser2", "testUser3", "testUser4" }, "test").ConfigureAwait(false);
            ///Use set 4 to assert this test case
            AssertUsers(_expectedUsers_set4, result.Users);
        }

        /// <summary>
        /// Step 1: Create a user with 0 Pub repo
        /// Step 2: Assert that the user is created with 0 average number of followers per public repo and no exception is raised/thrown
        /// </summary>
        [Test]
        public void UserObject_Creation()
        {
            List<User> usersCreated = new List<User>()
            {
                new User()
                {
                    name = "test",
                    login = "test-test",
                    numOfFollowers = 1,
                    numOfPubRepo = 0,
                    originInfo = "User retrieved from mock" 
                }
            };

            List<User> expectedUser = new List<User>()
            {
                new User()
                {
                    name = "test",
                    login = "test-test",
                    numOfFollowers = 1,
                    numOfPubRepo = 0,
                    avgNumFollowersPerPubRepo = 0,
                    originInfo = "User retrieved from mock"
                }
            };

            AssertUsers(_expectedUsers_set1, usersCreated);
        }

        [Test]
        public async Task RetrieveWithNonExistentUsers()
        {
            RetrieveUserResponseModel result = await _userRepository.RetrieveUser(new List<string>() { "test", "testUser2", "testUser3", "nonExistentUser" }, "test").ConfigureAwait(false);
            AssertUsers(_expectedUsers_set2, result.Users);
            Assert.That(_userRepository.GetMissedUsersCount() == 1);
        }

        private void AssertUsers(List<User> expected, List<User> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual[i].name, Is.EqualTo(expected[i].name));
                Assert.That(actual[i].login, Is.EqualTo(expected[i].login));
                Assert.That(actual[i].numOfFollowers, Is.EqualTo(expected[i].numOfFollowers));
                Assert.That(expected[i].avgNumFollowersPerPubRepo, Is.EqualTo(actual[i].avgNumFollowersPerPubRepo).Within(0.001));
                Assert.That(actual[i].originInfo, Is.EqualTo(expected[i].originInfo));
            }
        }   
    }
}