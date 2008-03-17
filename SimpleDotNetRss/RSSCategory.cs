using System.Web.UI.HtmlControls;

namespace SimpleDotNetRSS
{
    /// <summary>
    /// Represents a category that you want your RSSItem or RSSChannel to have.
    /// </summary>
    public class RSSCategory
    {
        string _domain = null;
        string _categoryText = null;

        /// <summary>
        /// Stores a category for the RSS Channel or Item
        /// </summary>
        /// <param name="categoryText">The category to be added</param>
        public RSSCategory(string categoryText) : this(categoryText, null) { }
        /// <summary>
        /// Stores a category for the RSS Channel or Item. If you don't understand the domain 
        /// paramater, don't use this constructor.
        /// </summary>
        /// <param name="categoryText">The category to be added</param>
        /// <param name="domain">The value of the element is a forward-slash-separated
        /// string that identifies a hierarchic location in the indicated taxonomy. 
        /// Processors may establish conventions for the interpretation of categories.
        /// An example is: http://www.fool.com/cusips </param>
        public RSSCategory(string categoryText, string domain)
        {
            if (domain != null)
                Domain = domain;
            CategoryText = categoryText;
        }

        /// <summary>
        /// The value of the element is a forward-slash-separated
        /// string that identifies a hierarchic location in the indicated taxonomy. 
        /// Processors may establish conventions for the interpretation of categories.
        /// An example is: http://www.fool.com/cusips
        /// </summary>
        public string Domain
        {
            get
            {
                return _domain;
            }
            set
            {
                HtmlGenericControl temp = new HtmlGenericControl();
                temp.InnerText = value;
                value = temp.InnerHtml;
                _domain = value;
            }
        }

        /// <summary>
        /// Stores a category for the RSS Channel or Item. If you don't understand the domain 
        /// paramater, don't use this constructor. Read-Only.
        /// </summary>
        public string CategoryText
        {
            get
            {
                return _categoryText;
            }
            protected set
            {
                _categoryText = value;
            }
        }

        /// <summary>
        /// Returns the RSS Category Eliment.
        /// </summary>
        /// <returns>the RSS string</returns>
        public override string ToString()
        {
            string temp = string.Empty;
            if (this.Domain != null)
                temp = string.Format("<category domain=\"{0}\">{1}</category>", this.Domain, this.CategoryText);
            else
                temp = string.Format("<category>{0}</category>", this.CategoryText);

            return temp;
        }

    }
}
