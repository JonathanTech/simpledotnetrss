using System.Collections.Generic;
using System.Text;

namespace SimpleDotNetRSS
{
    /// <summary>
    /// Used for implementing custom xml namespaces not supported by this library. i.e. Atom and Itunes
    /// </summary>
    public class RSSExtensionTag
    {
        readonly string _extension;
        readonly string _tag;
        readonly string _value;
        Dictionary<string, string> _attributes;

        /// <summary>
        /// Creates a specialized tag for tags that are not native to RSS 2.0 but are 
        /// included bacause of an RSS namespace decleration
        /// </summary>
        /// <param name="extension">The RSS Name space (i.e. Atom or Itunes)</param>
        /// <param name="tag"> The special namespace tag</param>
        /// <param name="value">What goes between the begining and closing tags.</param>
        public RSSExtensionTag(string extension, string tag, string value)
        {
            _extension = extension;
            _tag = tag;
            _value = value;
            Attributes = new Dictionary<string, string>();
        }

        /// <summary>
        /// The RSS Name space (i.e. Atom or Itunes). Read-only.
        /// </summary>
        public string Extension
        {
            get { return _extension; }
        }
        /// <summary>
        /// The special namespace tag. Read-only.
        /// </summary>
        public string Tag
        {
            get { return _tag; }
        }
        /// <summary>
        /// What goes between the begining and closing tags. Read-only.
        /// </summary>
        public string Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Allows you to set attributes for the extension
        /// </summary>
        public Dictionary<string, string> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        /// <summary>
        /// Returns the RSS for this extension.
        /// </summary>
        /// <returns>Returns the RSS for this extension.</returns>
        public override string ToString()
        {
            StringBuilder temp = new StringBuilder();

            temp.AppendFormat("<{0}:{1} ", Extension, Tag);
            foreach (KeyValuePair<string, string> vp in Attributes)
            {
                temp.AppendFormat("{0}=\"{1}\"", vp.Key, vp.Value);
            }
            temp.AppendFormat(">{0}</{1}:{2}>", Value, Extension, Tag);
            return temp.ToString();
        }
    }
}
