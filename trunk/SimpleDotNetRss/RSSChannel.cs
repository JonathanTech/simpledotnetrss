using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SimpleDotNetRSS
{
    /// <summary>
    /// The Main object for creating an RSS feed. After one of these objects is initialized through a 
    /// constructor you will be able to output the RSS as a formatted string using CreateRSS Method or 
    /// the ToString method.
    /// </summary>
    public class RSSChannel
    {
        #region DataMembers
        const string _cdataBegin = "<![CDATA[";
        const string _cdataEnd = "]]>";
        const string _docs = "http://cyber.law.harvard.edu/rss/rss.html";   //Where I got my information from 
        readonly Assembly _assemblyReference;                                //used to auto populate the version information.   
        readonly string _generator;                                         //"Simple Dot Net RSS System xxxx";

        Dictionary<string, string> _rssNamespaces = new Dictionary<string, string>();

        // Required:
        readonly string _title = null;
        readonly string _link = null;
        readonly string _description = null;
        List<RSSCategory> _categorys = new List<RSSCategory>();

        // Optional:
        string _language = null;
        string _copyright = null;
        string _managingEditorEmail = null;
        string _managingEditorName = null;
        string _webMasterEmail = null;
        string _webMasterName = null;
        DateTime? _pubDate;
        DateTime? _lastBuildDate;
        uint? _ttl; //time to live
        RSSImage _image = null;
        List<UInt16> _skipHours = new List<ushort>();
        List<DayOfWeek> _skipDays = new List<DayOfWeek>();
        List<RSSItem> _items = new List<RSSItem>();
        List<RSSExtensionTag> _channelExtensions = new List<RSSExtensionTag>();

        #endregion

        #region Constructors
        /// <summary>
        /// This creates the single
        /// channel element, which contains information
        /// about the channel (metadata) and its contents.
        /// This constructor provides only the required feilds
        /// </summary>
        /// <param name="title">
        /// The name of the channel. It's how people refer
        /// to your service. If you have an HTML website 
        /// that contains the same information as your RSS 
        /// file, the title of your channel should be the 
        /// same as the title of your website.
        /// </param>
        /// <param name="link">The URL to the HTML website corresponding to the channel.</param>
        /// <param name="description">Phrase or sentence describing the channel. Limit 200 Characters</param>
        /// <param name="items">The Array of Items that the Channel should include</param>
        public RSSChannel(string title, string link, string description, params RSSItem[] items)
        {
            _title = title;
            _link = link;
            _description = description;
            
            Items.AddRange(items);
            _assemblyReference = Assembly.GetExecutingAssembly();
            _generator = "Simple Dot Net RSS System Version " + _assemblyReference.GetName().Version.ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The first value is the namespace name, the second is the url identifier.
        /// </summary>
        public Dictionary<string, string> RssNamespaces
        {
            get { return _rssNamespaces; }
            set { _rssNamespaces = value; }
        }

        /// <summary>
        /// The name of the managing Editor
        /// </summary>
        public string ManagingEditorName
        {
            get { return _managingEditorName; }
            set { _managingEditorName = value; }
        }

        /// <summary>
        /// The name of the Web Master
        /// </summary>
        public string WebMasterName
        {
            get { return _webMasterName; }
            set { _webMasterName = value; }
        }

        /// <summary>
        /// The Items contained in the RSS Channel
        /// </summary>
        public List<RSSItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        /// <summary>
        /// Gets the name of the channel. It's how people refer
        /// to your service. If you have an HTML website 
        /// that contains the same information as your RSS 
        /// file, the title of your channel should be the 
        /// same as the title of your website.
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }

        }

        /// <summary>
        /// Gets the URL to the HTML website corresponding to the channel.
        /// </summary>
        public string Link
        {
            get
            {
                return _link;
            }
        }

        /// <summary>
        /// Gets the Phrase or sentence describing the channel.
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
        }

        /// <summary>
        /// The language the channel is written in. This allows
        /// aggregators to group all Italian language sites, for example,
        /// on a single page. A list of allowable values for this element,
        /// as provided by Netscape, is http://cyber.law.harvard.edu/rss/languages.html.
        /// You may also use values defined by the W3C.
        /// </summary>
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
            }
        }

        /// <summary>
        /// Copyright notice for content in the channel. use &amp;#169; for ©.
        /// </summary>
        public string Copyright
        {
            get
            {
                return _copyright;
            }
            set
            {
                if (value.Contains("©"))
                {
                    value = value.Replace("©", "&#169");
                }
                _copyright = value;
            }
        }

        /// <summary>
        /// Email address for person responsible for editorial content.
        /// </summary>
        public string MangingEditorEmail
        {
            get
            {
                return _managingEditorEmail;
            }
            set
            {
                _managingEditorEmail = value;
            }
        }

        /// <summary>
        /// Email address for person responsible for technical issues relating to channel.
        /// </summary>
        public string WebMasterEmail
        {
            get
            {
                return _webMasterEmail;
            }
            set
            {
                _webMasterEmail = value;
            }
        }

        /// <summary>
        /// 	The publication date for the content in the channel.
        /// For example, the New York Times publishes on a daily basis,
        /// the publication date flips once every 24 hours. That's when
        /// the pubDate of the channel changes. 
        /// i.e. Sat, 07 Sep 2002 00:00:01 GMT
        /// </summary>
        public DateTime? PubDate
        {
            get
            {
                return _pubDate;
            }
            set
            {
                _pubDate = value;
            }
        }

        /// <summary>
        /// The last time the content of the channel changed.	
        /// i.e. Sat, 07 Sep 2002 00:00:01 GMT
        /// </summary>
        public DateTime? LastBuildDate
        {
            get
            {
                return _lastBuildDate;
            }
            set
            {
                _lastBuildDate = value;
            }
        }

        /// <summary>
        /// Specify one or more categories that the 
        /// channel belongs to. Follows the same rules 
        /// as the item-level category element.
        /// </summary>
        public List<RSSCategory> Categorys
        {
            get
            {
                return _categorys;
            }
            set
            {
                _categorys = value;
            }
        }

        /// <summary>
        /// ttl stands for time to live. It's a number
        /// of minutes that indicates how long a 
        /// channel can be cached before refreshing from the source
        /// </summary>
        public uint? Ttl
        {
            get
            {
                return _ttl;
            }
            set
            {
                _ttl = value;
            }
        }

        /// <summary>
        /// Specifies a GIF, JPEG or PNG
        /// image that can be displayed with the channel.
        /// </summary>
        public RSSImage Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        /// <summary>
        /// Get the hours that are being skipped.
        /// </summary>
        public UInt16[] SkipHours
        {
            get
            {
                return _skipHours.ToArray();
            }
        }

        /// <summary>
        /// Clears the list of hours to skip.
        /// </summary>
        public void ClearSkipHours()
        {
            _skipHours.Clear();
        }

        /// <summary>
        /// Adds a specific hour that aggregators can bypass updating your RSS feed.
        /// </summary>
        /// <param name="hourToSkip">an hour (0-23) that the aggregator should not bother checking for updates.</param>
        public void AddToSkipHours(UInt16 hourToSkip)
        {
            bool dontAdd = false;
            if (hourToSkip < 24)
            {
                foreach (uint hour in _skipHours)
                    if (hourToSkip == hour)
                    { dontAdd = true; break; }
            }
            else
                dontAdd = false;
            //Add it to the list of it is not a repeat and less than 24
            if (!dontAdd)
                _skipHours.Add(hourToSkip);
        }

        /// <summary>
        /// Adds a Range of Hours to skip.
        /// </summary>
        /// <param name="HoursToSkip">The Hours to Skip</param>
        public void AddRangeToSkipHours(params UInt16[] HoursToSkip)
        {
            foreach (UInt16 hour in HoursToSkip)
                AddToSkipHours(hour);
        }

        /// <summary>
        /// A hint for aggregators telling them which days they can skip.
        /// </summary>
        public void AddToSkipDays(DayOfWeek dayToAdd)
        {
            bool alreadyThere = false;
            foreach (DayOfWeek day in _skipDays)
            {
                if (day == dayToAdd)
                {
                    alreadyThere = true;
                    break;
                }
            }
            if (!alreadyThere)
                _skipDays.Add(dayToAdd);
        }

        /// <summary>
        /// Adds a Range of Days to skip.
        /// </summary>
        /// <param name="daysToAdd">The Days to Skip</param>
        public void AddRangeToSkipDays(params DayOfWeek[] daysToAdd)
        {
            foreach (DayOfWeek day in daysToAdd)
                AddToSkipDays(day);
        }

        /// <summary>
        /// Clears the Days to skip.
        /// </summary>
        public void ClearSkipDays()
        {
            _skipDays.Clear();
        }

        /// <summary>
        /// Gets the days currently set to be skipped
        /// </summary>
        public DayOfWeek[] SkipDays
        {
            get { return _skipDays.ToArray(); }
        }

        /// <summary>
        /// Includes the Namespace extensions if you want to include them.
        /// </summary>
        public List<RSSExtensionTag> ChannelExtensions
        {
            get { return _channelExtensions; }
            set { _channelExtensions = value; }
        }
        #endregion

        #region RSS Generation methods

        /// <summary>
        /// Creates the RSS feed and *stops* the output of the current page.
        /// </summary>
        /// <param name="currentPage">The page to create the rss with</param>
        public void CreateRSS(System.Web.UI.Page currentPage)
        {
            currentPage.Response.Clear();
            currentPage.Response.BufferOutput = true;
            currentPage.Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Xml;
            currentPage.Response.Write(this.CreateRSS());
            currentPage.Response.End();
        }

        /// <summary>
        /// Creates the RSS for this RSSChannel
        /// </summary>
        /// <returns>The RSS 2.0 xml file in a string</returns>
        string CreateRSS()
        {
            StringBuilder RSSFeed = new StringBuilder("<?xml version=\"1.0\"?>");

            RSSFeed.Append("<rss version=\"2.0\"");
            foreach (KeyValuePair<string, string> nmsp in _rssNamespaces)
                RSSFeed.AppendFormat(" xlmns:{0}=\"{1}\"", nmsp.Key, nmsp.Value);
            RSSFeed.Append(">");

            RSSFeed.Append("<channel>");
            MakeChannelHeader(RSSFeed);

            if (this.Items.Count > 0)
                MakeItems(this, RSSFeed);

            RSSFeed.Append("</channel>");
            RSSFeed.Append("</rss>");

            return RSSFeed.ToString();
        }

        /// <summary>
        /// Creates the header information for the RSS
        /// </summary>
        /// 
        /// <param name="RSSFeed">The StringBuilder to add too.</param>
        void MakeChannelHeader(StringBuilder RSSFeed)
        {
            #region Required feilds

            if (Title != null)
                RSSFeed.AppendFormat("<title>{0}</title>", Title);
            else
                throw new Exception("Empty title element in RSS Element!");
            if (Link != null)
                RSSFeed.AppendFormat("<link>{0}{1}{2}</link>", _cdataBegin, Link, _cdataEnd);
            else
                throw new Exception("Empty Link element in RSS Element!");
            if (Description != null)
                RSSFeed.AppendFormat("<description>{1}{0}{2}</description>", Description, _cdataBegin, _cdataEnd);
            else
                throw new Exception("Empty Description in RSS Element!");

            #endregion

            #region Optional Feilds

            if (Language != null)
                RSSFeed.AppendFormat("<language>{0}</language>", Language);
            if (Copyright != null)
                RSSFeed.AppendFormat("<copyright>{0}</copyright>", Copyright);

            if (MangingEditorEmail != null)
                RSSFeed.AppendFormat("<managingEditor>{0} ({1})</managingEditor>", MangingEditorEmail, ((ManagingEditorName == null) ? string.Empty : ManagingEditorName));
            if (WebMasterEmail != null)
                RSSFeed.AppendFormat("<webMaster>{0} ({1})</webMaster>", WebMasterEmail, ((WebMasterName == null) ? string.Empty : ManagingEditorName));

            if (PubDate != null)
                RSSFeed.AppendFormat("<pubDate>{0}</pubDate>", ((DateTime)(PubDate)).ToString("r"));
            if (LastBuildDate != null)
                RSSFeed.AppendFormat("<lastBuildDate>{0}</lastBuildDate>", ((DateTime)(LastBuildDate)).ToString("r"));

            foreach (RSSCategory cat in Categorys)
                cat.ToString();

            //add our signiture so that they know that it was us who created it.
            RSSFeed.AppendFormat("<generator>{0}</generator>", _generator);
            //point to the RSS 2.0 documentation site.
            RSSFeed.AppendFormat("<docs>{0}</docs>", RSSChannel._docs);
            if (Ttl != null)
                RSSFeed.AppendFormat("<ttl>{0}</ttl>", Ttl);

            MakeRSSImageTag(RSSFeed);
            GenerateSkipDaysAndHours(RSSFeed);

            foreach (RSSExtensionTag ext in ChannelExtensions)
                ext.ToString();
            #endregion
        }

        private void GenerateSkipDaysAndHours(StringBuilder RSSFeed)
        {
            if (SkipHours.Length > 0)
            {
                RSSFeed.Append("<skipHours>");
                foreach (UInt16 hour in SkipHours)
                    RSSFeed.AppendFormat("<hour>{0}</hour>", hour);
                RSSFeed.Append("</skipHours>");
            }
            if (SkipDays.Length > 0)
            {
                RSSFeed.Append("<skipDays>");
                foreach (DayOfWeek day in SkipDays)
                    RSSFeed.AppendFormat("<day>{0}</day>", Enum.GetName(typeof(DayOfWeek), day));
                RSSFeed.Append("</skipDays>");
            }
        }

        /// <summary>
        /// Adds an image to the feed.
        /// </summary>
        /// <param name="RSSFeed">The Stringbuilder you want the Image eliments added too.</param>
        void MakeRSSImageTag(StringBuilder RSSFeed)
        {
            if (Image != null)
                if (Image.ImageURL != null)
                {
                    RSSFeed.AppendFormat("<image>");
                    RSSFeed.AppendFormat("<url>{0}{1}{2}</url>", _cdataBegin, Image.ImageURL, _cdataEnd);
                    RSSFeed.AppendFormat("<title>{0}</title>", ((Image.ImageTitle != null) ? Image.ImageTitle : this.Title));
                    RSSFeed.AppendFormat("<link>{0}{1}{2}</link>", _cdataBegin, ((Image.ImageLink != null) ? Image.ImageLink : Link), _cdataEnd);
                    RSSFeed.AppendFormat("<height>{0}</height>", Image.Height);
                    RSSFeed.AppendFormat("<width>{0}</width>", Image.Width);
                    RSSFeed.AppendFormat("</image>");
                }
        }

        /// <summary>
        /// Creates the Items for the Channel that is passed in.
        /// </summary>
        /// <param name="Feed">The Channel to generate the items from</param>
        /// <param name="RSSFeed">The StringBuilder to add the xml too.</param>
        void MakeItems(RSSChannel Feed, StringBuilder RSSFeed)
        {
            foreach (RSSItem i in Feed.Items)
            {
                RSSFeed.Append(i.ToString());
            }

        }

        /// <summary>
        /// Out puts the Rss to a string for you maniputlation fun, or just so you can use it however you want to use the rss.
        /// </summary>
        /// <returns>Freaking cool RSS!!!!</returns>
        public override string ToString()
        {
            return this.CreateRSS();
        }

        #endregion

        #region RSSItem methods

        /// <summary>
        /// Adds to the RSSItems that will be stored.
        /// </summary>
        /// <param name="item">The RSS Item to add</param>
        public void Add(RSSItem item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Adds a range of RSSItems to the RSSChannel.
        /// </summary>
        /// <param name="items">The RSSItems to store</param>
        public void AddRange(RSSItem[] items)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// clears the RSSItems that are stored in the RSSChannel.
        /// </summary>
        public void Clear()
        {
            Items.Clear();
        }

        #endregion

    }

}

