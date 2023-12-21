namespace WebApplication1.User
{
    /// <summary>
    /// The User model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        public required string name { get; set; }

        /// <summary>
        /// The login name of the user.
        /// </summary>
        public required string login { get; set; }

        /// <summary>
        /// The number of followers of the user.
        /// </summary>
        public int numOfFollowers { get; set; }

        /// <summary>
        /// The number of public repositories of the user.
        /// </summary>
        public int numOfPubRepo { get; set; }

        /// <summary>
        /// Computes the average number of followers per public repository.
        /// </summary>
        /// <returns>average number of followers</returns>
        public double avgNumFollowersPerPubRepo { get { return numOfFollowers / numOfPubRepo; } }

        /// <summary>
        /// Origin info of the user.
        /// </summary>
        public string originInfo { get; set; }
    }
}
