using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.User
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// The RetrieveUser endpoint.
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        [HttpGet(Name = "RetrieveUsers")]
        public ActionResult<RetrieveUserResponseModel> RetrieveUsers([FromHeader] RetrieveUserRequestModel userNames)
        {
            return _userService.RetrieveUsers(userNames);
        }
    }
}
