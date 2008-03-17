using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDotNetRSS
{
    /// <summary>
    /// Represents a RSS Item in a RSS Channel.
    /// </summary>
    public class RSSItem
    {
        #region DataMembers
        const string _cdataBegin = "<![CDATA[";
        const string _cdataEnd = "]]>";

        readonly string _title = null;
        readonly string _link = null;
        readonly string _description = null;

        string _authorsEmail = null;
        string _authorsName = null;
        string _guid = null;
        bool _guidIsPermaLink = false;
        List<RSSCategory> _categorys = new List<RSSCategory>();
        string _commentsPage = null;
        RSSEnclosure _enclosure;
        DateTime? _pubDate;
        List<RSSExtensionTag> _extensions = new List<RSSExtensionTag>();
        #endregion

        #region Constructors

        /// <summary>
        /// This represents one RSS Item. 
        /// This constructor fits the base requirements for an RSS Item
        /// </summary>
        /// <param name="title">The Title of the RSS Item</param>
        /// <param name="link">The URL of the Item</param>
        /// <param name="description">A short description of the RSS Item.</param>
        public RSSItem(string title, string link, string description)
        {
            _title = title;
            _link = link;
            _description = description;
        }
        #endregion

        #region Properties

        /// <summary>
        /// The title of the item. Read Only.
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        /// <summary>
        /// The URL of the item. Read Only.
        /// </summary>
        public string Link
        {
            get { return _link; }
        }

        /// <summary>
        /// The item synopsis. Read only.
        /// </summary>
        public string Description
        {
            get { return _description; }
        }

        /// <summary>
        /// Email address of the author of the item
        /// </summary>
        public string AuthorsEmail
        {
            get { return _authorsEmail; }
            set { _authorsEmail = value; }
        }

        /// <summary>
        /// The Authors Name
        /// </summary>
        public string AuthorsName
        {
            get { return _authorsName; }
            set { _authorsName = value; }
        }

        /// <summary>
        /// Includes the item in one or more categories. 
        /// </summary>
        public List<RSSCategory> Categorys
        {
            get { return _categorys; }
            set { _categorys = value; }
        }

        /// <summary>
        /// URL of a page for comments relating to the item.
        /// </summary>
        public string CommentsPage
        {
            get { return _commentsPage; }
            set { _commentsPage = value; }
        }

        /// <summary>
        ///Describes a media object that is attached to the item.
        /// </summary>
        public RSSEnclosure ItemEnclosure
        {
            get { return _enclosure; }
            set { _enclosure = value; }
        }

        /// <summary>
        /// Indicates when the item was published.
        /// </summary>
        public DateTime? PubDate
        {
            get { return _pubDate; }
            set { _pubDate = value; }
        }

        /// <summary>
        /// A unique Identifier for this Item. If this is left as null it will use 
        /// the links property for generating one. Can be any values, just as long as 
        /// it is unique to the item and does not change.
        /// </summary>
        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        /// <summary>
        /// Wether or not the Guid is an actual link or not. Default is false.
        /// </summary>
        public bool GuidIsPermaLink
        {
            get { return _guidIsPermaLink; }
            set { _guidIsPermaLink = value; }
        }

        /// <summary>
        /// A list of custom tags. This is used to extend RSS. 
        /// You need to make sure to include the namespace for the tag in the channel.
        /// </summary>
        internal List<RSSExtensionTag> Extensions
        {
            get { return _extensions; }
            set { _extensions = value; }
        }
        #endregion

        /// <summary>
        /// Will return the items portion of the RSS feed.
        /// </summary>
        /// <returns>XML for this Item in RSS format</returns>
        public override string ToString()
        {
            StringBuilder RSSFeed = new StringBuilder();

            RSSFeed.Append("<item>");
            RSSFeed.AppendFormat("<title>{1}{0}{2}</title>", Title, _cdataBegin, _cdataEnd);
            RSSFeed.AppendFormat("<link>{0}{1}{2}</link>", _cdataBegin, Link, _cdataEnd);
            RSSFeed.AppendFormat("<description>{1}{0}{2}</description>", Description, _cdataBegin, _cdataEnd);
            if (AuthorsEmail != null)
                RSSFeed.AppendFormat("<author>{0} ({1})</author>", AuthorsEmail, ((AuthorsName != null) ? AuthorsName : string.Empty));
            foreach (RSSCategory cat in Categorys)
                RSSFeed.Append(cat.ToString());
            if (CommentsPage != null)
                RSSFeed.AppendFormat("<comments>{0}{1}{2}</comments>", _cdataBegin, CommentsPage, _cdataEnd);
            if (ItemEnclosure != null)
                RSSFeed.AppendFormat("<enclosure url=\"{0}\" length=\"{1}\" type=\"{2}\" />",
                    ItemEnclosure.EnclosureURL, ((ItemEnclosure.Length != null) ? ItemEnclosure.Length : 0), ItemEnclosure.MimeType);

            if (PubDate != null)
                RSSFeed.AppendFormat("<pubDate>{0}</pubDate>", ((DateTime)PubDate).ToString("r"));

            if (Guid == null)
                Guid = Link;
            RSSFeed.AppendFormat("<guid isPermaLink=\"{0}\">{1}{2}{3}</guid>", GuidIsPermaLink.ToString().ToLower(), _cdataBegin, Guid, _cdataEnd);
            foreach (RSSExtensionTag rssext in Extensions)
                rssext.ToString();

            RSSFeed.Append("</item>");

            return RSSFeed.ToString();
        }
    }
}




