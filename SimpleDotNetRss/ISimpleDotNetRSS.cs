
namespace SimpleDotNetRSS
{
    /// <summary>
    /// Interface for retreiving a RSSChannel object.
    /// </summary>
    public interface ISimpleDotNetRSS
    {
        /// <summary>
        /// Outputs an RSS Channel feed for that object
        /// </summary>
        /// <returns>An RSSChannel Object</returns>
        bool HasRss { get; set; }

    }
}
