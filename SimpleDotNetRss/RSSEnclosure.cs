using System;
using System.Web.UI.HtmlControls;

namespace SimpleDotNetRSS
{
    /// <summary>
    /// This is applyed to Items that should have files associated to them. All feilds are required.
    /// </summary>
    public class RSSEnclosure
    {
        readonly string _enclosureURL = null;
        readonly ulong? _length = null;
        readonly string _mimeType = null;

        /// <summary>
        /// This is used for attaching files to an RSS feed. All fields are required 
        /// for campatability of the standard.
        /// </summary>
        /// <param name="url">The URL of the media file.</param>
        /// <param name="length">The size in Bytes of the file</param>
        /// <param name="mimeType">The Mime Type for that file</param>
        public RSSEnclosure(string url, ulong length, string mimeType)
        {
            url = ConvertToXMLSafeURL(url);
            _enclosureURL = url; 
            _length = length;
            _mimeType = mimeType;
        }

        private static string ConvertToXMLSafeURL(string url)
        {
            HtmlGenericControl temp = new HtmlGenericControl();
            System.Uri u = new Uri(url);
            //System.Xml.XmlCharacterData xd; xd.
            temp.InnerText = url;
            url = temp.InnerHtml;
            return url;
        }

        /// <summary>
        /// The URL of the media file.
        /// </summary>
        public string EnclosureURL
        {
            get { return _enclosureURL; }
        }
        /// <summary>
        /// The size in Bytes of the file
        /// </summary>
        public ulong? Length
        {
            get { return _length; }
        }
        /// <summary>
        /// The Mime Type for that file
        /// </summary>
        public string MimeType
        {
            get { return _mimeType; }
        }

    }
}
