using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;


[assembly: TagPrefix("SimpleDotNetRss", "SDNRSS")]
namespace SimpleDotNetRSS
{
    /// <summary>
    /// Generates a link on your page for the rss if your page has implimented the ISimpleDotNetRSS interface.
    /// </summary>
    [Themeable(true)]
    [ToolboxData("<{0}:SimpleRSSLink runat=\"server\" />")]
    public class SimpleRSSLink : WebControl
    {
        #region Data members
        string _text = "RSS";
        bool _isImageButton = false;
        string _imageUrl = string.Empty;
        string _rssPageLocation = "RSS.aspx";
        string _queryStringKey = "Rss";
        string _queryStringValue = "True";
        string _noRssText = "No RSS";
        string _noRssImageURL = string.Empty;
        bool _imageBorder = false;
        #endregion

        #region Properties
        /// <summary>
        /// Do you want an Image border or not?
        /// </summary>
        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Localizable(true)]
        [Description("Gives you the option to display the image border or not.")]
        public bool ImageBorder
        {
            get { return _imageBorder; }
            set { _imageBorder = value; }
        }

        /// <summary>
        /// The Query string key to use in the creation of the link (i.e. ?xxx=True)
        /// </summary>
        [Category("Behavior")]
        [Description("The Query string key to use in the creation of the link (i.e. ?xxx=True)")]
        [DefaultValue("Rss")]
        public string QueryStringName
        {
            get { return _queryStringKey; }
            set { _queryStringKey = value; }
        }
        /// <summary>
        /// The Query string value to use in the creation of the link (i.e. ?Rss=xxx)
        /// </summary>
        [Category("Behavior")]
        [Description("The Query string value to use in the creation of the link (i.e. ?Rss=xxx)")]
        [DefaultValue("True")]
        public string QueryStringValue
        {
            get { return _queryStringValue; }
            set { _queryStringValue = value; }
        }
        /// <summary>
        /// The text to display if there is no RSS for the current page
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("No RSS")]
        [Description("The text to display if there is no RSS for the current page")]
        public string NoRssText
        {
            get { return _noRssText; }
            set { _noRssText = value; }
        }
        /// <summary>
        /// The Image to use if there is no RSS for the current page
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("")]
        [Description("The Image to use if there is no RSS for the current page")]
        public string NoRssImageURL
        {
            get
            {
                if (DesignMode)
                    return _noRssImageURL;
                return MakeAbsoluteImageUrl(_noRssImageURL);
            }
            set { _noRssImageURL = value; }
        }

        /// <summary>
        /// Used to decide if anything is renedered if there is no RSS.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Used to decide if anything is renedered if there is no RSS.")]
        public bool DisplayOnNoRSS { get; set; }

        /// <summary>
        /// The Text to display if the control is not an image button, or the alt text if it is an image button.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("RSS")]
        [Localizable(true)]
        [Description("The Text to display if the control is not an image button, or the alt text if it is an image button.")]
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
            }
        }

        /// <summary>
        /// The images location for the image button."
        /// </summary>
        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [UrlProperty("*.*")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("The images location for the image button.")]
        public string ImageUrl
        {
            get
            {
                if (DesignMode)
                    return _imageUrl;
                return MakeAbsoluteImageUrl(_imageUrl);
            }
            set
            { _imageUrl = value.Trim(); }
        }
        /// <summary>
        /// Set to true to have an image used when there is no RSS feed on the current page.
        /// </summary>
        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Localizable(true)]
        [Description("Set to true to have an image used when there is no RSS feed on the current page.")]
        public bool ImageOnNoRSS { get; set; }

        private string MakeAbsoluteImageUrl(string urlToProcess)
        {
            if (Uri.IsWellFormedUriString(urlToProcess, UriKind.Relative))
            {
                return Page.Request.Url.GetLeftPart(UriPartial.Authority) + Page.Request.ApplicationPath + "/" + urlToProcess; //ResolveUrl(_imageUrl);
            }
            else if (Uri.IsWellFormedUriString(urlToProcess, UriKind.Absolute))
            {
                return urlToProcess;
            }
            return "Error";
        }

        /// <summary>
        /// Set to true to Have the control render as an image button.
        /// </summary>
        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Localizable(true)]
        [Description("Set to true to have the control render as an image button.")]
        public bool ImageButton
        {
            get { return _isImageButton; }
            set { _isImageButton = value; }
        }
        #endregion

        #region Rendering Methods

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                ISimpleDotNetRSS thisPage = this.Page as ISimpleDotNetRSS;
                if (thisPage != null && thisPage.HasRss)
                    return HtmlTextWriterTag.A;
                else
                    return HtmlTextWriterTag.Span;
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            if (this.TagKey == HtmlTextWriterTag.A)
                if (!DesignMode)
                {
                    string tempAddress;

                    if (this.Page.Request.QueryString.Count > 0)
                        tempAddress = this.Page.Request.Url.PathAndQuery + "&" + _queryStringKey + "=" + this.Page.Server.HtmlEncode(_queryStringValue);
                    else
                        tempAddress = this.Page.Request.Url.PathAndQuery + "?" + _queryStringKey + "=" + this.Page.Server.HtmlEncode(_queryStringValue);

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, tempAddress);
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, "");
                }
        }
        /// <summary>
        /// Writes out the html for the control.
        /// </summary>
        protected override void RenderContents(HtmlTextWriter output)
        {
            //Checks to see if this page has RSS.
            ISimpleDotNetRSS RssPage = Page as ISimpleDotNetRSS;
            if ((RssPage != null && RssPage.HasRss) || DesignMode)
            {
                if (ImageButton)
                {
                    CreateImageTag(output, ImageUrl, Text);
                }
                else
                {
                    output.Write(Text);
                }
            }
            else
            {
                if (DisplayOnNoRSS)
                    if (ImageOnNoRSS)
                        CreateImageTag(output, NoRssImageURL, NoRssText);
                    else
                        output.Write(NoRssText);
            }
            
            output.EndRender();
            
        }



        private void CreateImageTag(HtmlTextWriter output, string imageUrl, string imageAltText)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Src, imageUrl);
            output.AddAttribute(HtmlTextWriterAttribute.Alt, imageAltText);
            if (!ImageBorder)
            {
                output.AddStyleAttribute("border", "0px");
            }
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            //output.EndRender();
            output.RenderEndTag();
        }
        #endregion
    }

}
