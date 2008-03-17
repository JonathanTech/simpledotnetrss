
namespace SimpleDotNetRSS
{
    /// <summary>
    /// Used to add a single Image to your RSS feed. This is usually an image that represents the general content
    /// of the RSS feed or the company logo for the owner of the feed.
    /// </summary>
    public class RSSImage
    {
        readonly string _imageURL = null;
        string _imageTitle = null;
        string _imageLink = null;
        uint _width = 88;
        uint _height = 88;

        #region Constructors
        /// <summary>
        /// Attaches and Image to an RSS Feed. You must use the full URL for thie ImageURL, not a Relative one.
        /// </summary>
        /// <param name="imageURL"> is the URL of a GIF, JPEG or PNG image that represents the channel. Must be a Full URL.</param>
        public RSSImage(string imageURL) : this(imageURL, null, null) { }
        /// <summary>
        /// Attaches and Image to an RSS Feed. You must use the full URL for thie ImageURL, not a Relative one.
        /// </summary>
        /// <param name="imageURL"> is the URL of a GIF, JPEG or PNG image that represents the channel. Must be a Full URL.</param>
        /// <param name="imageTitle">describes the image, it's used in the ALT attribute of the HTML img tag when the channel is rendered in HTML.</param>
        /// <param name="imageLink">is the URL of the site, when the channel is rendered, the image is a link to the site.
        /// (Note, in practice the image title and link should have the same value as the channel's title
        /// and </param>
        public RSSImage(string imageURL, string imageTitle, string imageLink)
            : this(imageURL, imageTitle, imageLink, 88, 88) { }
        /// <summary>
        /// Attaches and Image to an RSS Feed. You must use the full URL for thie ImageURL, not a Relative one.
        /// </summary>
        /// <param name="imageURL"> is the URL of a GIF, JPEG or PNG image that represents the channel. Must be a Full URL.</param>
        /// <param name="imageTitle">describes the image, it's used in the ALT attribute of the HTML img tag when the channel is rendered in HTML.</param>
        /// <param name="imageLink">is the URL of the site, when the channel is rendered, the image is a link to the site.
        /// (Note, in practice the image title and link should have the same value as the channel's title
        /// and link)</param>
        /// <param name="width">(Optional) Default is 88, max is 144(pixels)</param>
        /// <param name="height">(Optional) Default is 88, max is 144(pixels)</param>
        public RSSImage(string imageURL, string imageTitle, string imageLink, uint width, uint height)
        {
            _imageURL = imageURL;
            ImageTitle = imageTitle;
            ImageLink = imageLink;
            Width = width;
            Height = height;
        }
        #endregion

        #region Properties
        /// <summary>
        /// the URL of a GIF, JPEG or PNG image that represents the channel.
        /// </summary>
        public string ImageURL
        {
            get { return _imageURL; }
        }

        /// <summary>
        /// describes the image, it's used in the ALT attribute 
        /// of the HTML img tag when the channel is rendered in HTML.<
        /// </summary>
        public string ImageTitle
        {
            get { return _imageTitle; }
            set { _imageTitle = value; }
        }

        /// <summary>
        /// is the URL of the site, when the channel is rendered, the image is a link to the site.
        /// (Note, in practice the image title and link should have the same value as the channel's title
        /// and link)
        /// </summary>
        public string ImageLink
        {
            get { return _imageLink; }
            set { _imageLink = value; }
        }

        /// <summary>
        /// (Optional) Default is 88, max is 144(pixels)
        /// </summary>
        public uint Width
        {
            get { return _width; }
            set
            {
                if (value > 144)
                {
                    value = 144;
                }
                _width = value;
            }
        }

        /// <summary>
        /// (Optional) Default is 88, max is 144(pixels)
        /// </summary>
        public uint Height
        {
            get { return _height; }
            set
            {
                if (value > 144)
                {
                    value = 144;
                }
                _height = value;
            }
        }
        #endregion

    }
}
