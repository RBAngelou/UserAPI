using System.ComponentModel.DataAnnotations;

namespace WebApplication1.User
{
    /// <summary>
    /// The response model for the GetUser endpoint.
    /// </summary>
    public class RetrieveUserResponseModel
    {
        /// <summary>
        /// Success or Failure.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Returns OK if request is successful, otherwise a message will specify why it's rejected or unsuccessful.
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// The List of Users retrieved.
        /// </summary>
        public required List<User> Users { get; set; } 
    }
}
