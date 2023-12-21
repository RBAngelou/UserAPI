using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.User
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UsersController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// The GetUser endpoint.
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetUsers")]
        public ActionResult<GetUserResponseModel> GetUsers([FromQuery] GetUserRequestModel userNames)
        {
            return _userService.GetUsers(userNames);
        }
    }
}
