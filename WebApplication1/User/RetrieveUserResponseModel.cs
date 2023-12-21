using System.ComponentModel.DataAnnotations;

namespace WebApplication1.User
{
    /// <summary>
    /// The response model for the GetUser endpoint.
    /// </summary>
    public class RetrieveUserResponseModel
    {
        /// <summary>
        /// The List of Users retrieved.
        /// </summary>
        public required List<User> Users { get; set; } 
    }
}
