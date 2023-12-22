using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.User
{
    [ApiController]
    public class UsersController : Controller
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
        [HttpGet]
        [Route("/api/retrieveUsers")]
        public async Task<ActionResult<RetrieveUserResponseModel>> retrieveUsersAsync([FromQuery] RetrieveUserRequestModel userRequest)
        {
            return await _userService.RetrieveUsers(userRequest).ConfigureAwait(false);
        }
    }
}
