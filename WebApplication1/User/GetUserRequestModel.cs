using System.ComponentModel.DataAnnotations;

namespace WebApplication1.User
{
    /// <summary>
    /// The request model for the GetUser endpoint.
    /// </summary>
    public class GetUserRequestModel
    {
        /// <summary>
        /// The List of Usernames to use as filter for retrieving users.
        /// </summary>
        [Required]
        public required List<string> Usernames { get; set; }
    }
}
