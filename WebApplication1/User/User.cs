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
        /// The company associated with the user.
        /// </summary>
        public  string company { get; set; }

        /// <summary>
        /// The number of followers of the user.
        /// </summary>
        public int numOfFollowers { get; set; }

        /// <summary>
        /// The number of public repositories of the user.
        /// </summary>
        public int numOfPubRepo { get; set; }

        /// <summary>
        /// Computes the average number of followers per public repository. If numOfPubRepo is 0, then the average is 0.
        /// </summary>
        /// <returns>average number of followers</returns>
        public double avgNumFollowersPerPubRepo { get { return (numOfPubRepo > 0) ? numOfFollowers / numOfPubRepo : 0; } set { } }

        /// <summary>
        /// Origin info of the user.
        /// </summary>
        public string originInfo { get; set; }
    }
}
