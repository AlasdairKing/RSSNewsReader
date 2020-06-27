using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSSCommon
{
    /// <summary>
    /// One item in an RSS feed
    /// </summary>
    public class RSSItemClass
    {
        private string _title;
        private string _feedUrl;
        private string _itemUrl;
        private bool _isRead;
        private string _pubDate;
        private string _contents;
        private string _enclosure;

        /// <summary>
        /// Whether RSS or Atom
        /// </summary>
        public enum RSSType
        {
            /// <summary>
            /// This is an Atom feed
            /// </summary>
            Atom,
            /// <summary>
            /// This is an RSS feed
            /// </summary>
            RSS
        }

        /// <summary>
        /// Create a new item using text obtained from an "item" element in a feed. 
        /// </summary>
        /// <param name="title">The contents of the "title" element.</param>
        /// <param name="itemUrl">The contents of the "link" element.</param>
        /// <param name="feedUrl">The URL of the feed in which this item sits.</param>
        /// <param name="pubDate">The contents of the "pubDate" element.</param>
        /// <param name="contents">The contents of the "content" or "content:encoded" or "description" node, which can be HTML.</param>
        /// <param name="enclosure">The url of an enclosure - an audio or video file found if this is an item in a podcast.</param>
        public RSSItemClass(string title, string itemUrl, string feedUrl, string pubDate, string contents, string enclosure)
        {
            _title = title;
            // Do character substitutions to catch character escape sequences that have ended up in the data
            // probably because of an error on the website. I'm sure this list could be extended, but 
            // quotation marks and dashes are the main offenders.
            _title = RSSLibraryClass.CleanUpXml(_title);
            _feedUrl = feedUrl;
            _itemUrl = itemUrl.Replace("\t", "").Replace("\n","").Trim(); // The Daily Mail feed has tabs and newlines
            // in the url, so delete and trim. 3.1.2 4 May 2014.
            DateTime dt;
            if (DateTime.TryParse(pubDate, out dt))
            {
                _pubDate = dt.ToLongDateString();
                if (_pubDate.StartsWith("0"))
                {
                    // Leading zeros look sucky.
                    _pubDate = _pubDate.Substring(1, _pubDate.Length - 1);
                }
            }
            else
            {
                _pubDate = "";
            }
            _contents = TrivialParse(contents);
            _enclosure = enclosure;
        }
        /// <summary>
        /// URL of the audio or video file for this item. Only applies to podcasts: otherwise "". 
        /// </summary>
        public string EnclosureUrl
        {
            get
            {
                return _enclosure;
            }
        }

        /// <summary>
        /// Parse HTML into text very stupidly.
        /// </summary>
        /// <param name="html">Some HTML mark-up</param>
        /// <returns>Plain text string</returns>
        private string TrivialParse(string html)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Char[] htmlChars = html.ToCharArray();
            int parseState = 0; // 0 = output, 1 = don't output, 2 = don't output this character but output next.
            for (int i = 0; i < htmlChars.Length; i++)
            {
                if (htmlChars[i] == '<')
                {
                    parseState = 1;
                }
                else if (htmlChars[i] == '>')
                {
                    parseState = 2;
                }
                else if (parseState == 0)
                {
                    sb.Append(htmlChars[i]);
                }
                if (parseState == 2)
                {
                    sb.Append(" ");
                    parseState = 0;
                }
            }
            string output = sb.ToString();
            while (output.Contains("  "))
            {
                output = output.Replace("  ", " ");
            }
            return RSSLibraryClass.CleanUpXml(output.Trim());
        }

        public string Contents
        {
            get
            {
                return _contents;
            }
        }

        public string FeedUrl
        {
            get
            {
                return _feedUrl;
            }
        }

        /// <summary>
        /// User-readable description of the RSSItem, for displaying in the list of items.
        /// </summary>
        /// <returns>Title, date, and first 640 characters of contents concatenated together.</returns>
        public override string ToString()
        {
            string name;
            if (_pubDate.Length == 0)
            {
                name = _title;
            }
            else
            {
                name = _title + " (" + _pubDate + ")";
            }
            if (_contents.Length == 0)
            {
                return name;
            }
            else if (_contents.Length > 640)
            {
                return name + " \"" + _contents.Substring(0, 640).Replace("\n", " ") + "...\"";
            }
            else
            {
                return name + " \"" + _contents.Replace("\n", " ") + "\"";
            }
        }


        public string Url
        {
            get
            {
                return _itemUrl;
            }
        }

        public bool IsRead
        {
            get
            {
                return _isRead;
            }
            set
            {
                _isRead = value;
            }
        }
    }
}
