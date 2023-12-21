using System.ComponentModel.DataAnnotations;

namespace WebApplication1.User
{
    /// <summary>
    /// The request model for the GetUser endpoint.
    /// </summary>
    public class RetrieveUserRequestModel
    {
        /// <summary>
        /// The List of Usernames to use as filter for retrieving users.
        /// </summary>
        public string[] usernames { get; set; }

        public string bearerToken { get; set; }
    }
}
