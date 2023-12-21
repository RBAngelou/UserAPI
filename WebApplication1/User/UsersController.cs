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
        public ActionResult<RetrieveUserResponseModel> retrieveUsers([FromQuery] RetrieveUserRequestModel userRequest)
        {
            return _userService.RetrieveUsers(userRequest);
        }
    }
}
