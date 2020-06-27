using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

/// <summary>
/// Code common to all of RSS, Atom and podcast feeds. 
/// </summary>
namespace RSSCommon
{
    /// <summary>
    /// Indicates that a feed has a certain number of items read or unread,
    /// and that the UI should update the list.
    /// </summary>
    /// <param name="url">The url of the feed</param>
    /// <param name="count">The number of unread items.</param>
    public delegate void ItemCountUpdatedHandler(string url, int count);

    /// <summary>
    /// Indicates that the feed has progressed towards being loaded, and
    /// the UI should update the status bar.
    /// </summary>
    /// <param name="message"></param>
    public delegate void LoadProgressHandler(string message);

    /// <summary>
    /// Fires when an RSS feed is successfully loaded, and the
    /// UI should display its contents
    /// </summary>
    public delegate void LoadCompletedHandler();

    /// <summary>
    /// Fires when there is a problem loading a feed, and the UI should
    /// tell the user.
    /// </summary>
    public delegate void LoadFailedHandler(string url, RSSLibraryClass.ParseResult parseResult, string message);

    /// <summary>
    /// Manages the user's RSS and Atom feeds and podcasts.
    /// </summary>
    public class RSSLibraryClass
    {
        /// <summary>
        /// The result of trying to parse a feed or podcast. 
        /// </summary>
        public enum ParseResult
        {
            /// <summary>
            /// The RSS or Atom feed or podcast was obtained and parsed correctly.
            /// </summary>
            OK,
            /// <summary>
            /// The Microsoft XML library returned an "Invalid Url" error.
            /// </summary>
            InvalidURL,
            /// <summary>
            /// This is not an RSS feed/podcast, nor an Atom feed/podcast. 
            /// </summary>
            InvalidDocumentType,
            /// <summary>
            /// This is an HTML document, not an RSS or Atom feed/podcast.
            /// </summary>
            ThisIsHTMLNotRSSOrAtom,
            /// <summary>
            /// We haven't downloaded this feed/podcast to the cache and we can't now, but
            /// it might just be that the network is down.
            /// </summary>
            CouldNotGetRightNowMaybeNetwork,
            /// <summary>
            /// Some other parsing error, possibly from the Microsoft XML library
            /// </summary>
            UnknownError
        }

        /// <summary>
        /// Used in my state machine. Shows what the next XML text node result is believed to contain.
        /// </summary>
        private enum NextTextNodeIsEnum
        {
            Unknown,
            Title,
            Link,
            Url,
            Contents,
            PubDate
        }

        /// <summary>
        /// The index of the feed last checked for its item count to update the UI.
        /// </summary>
        private int _FeedWithUpdatedItemCountIndex;

        /// <summary>
        /// Update on progress in loading a feed.
        /// </summary>
        public event LoadProgressHandler LoadProgress;

        /// <summary>
        /// This feed didn't load.
        /// </summary>
        public event LoadFailedHandler LoadFailed;

        /// <summary>
        /// Whether this is a podcast, defined as an RSS or Atom feed with at least one enclosure. 
        /// </summary>
        public bool IsPodcast
        {
            get; private set;
        }

        /// <summary>
        /// Feed loaded okay.
        /// </summary>
        public event LoadCompletedHandler LoadFinished;

        /// <summary>
        /// Whether we are quietly updating feeds/podcasts in the background, checking for new items and so on. 
        /// </summary>
        private enum LibraryState
        {
            /// <summary>
            /// Caching things, working away, quite happy.
            /// </summary>
            Quiet,
            /// <summary>
            /// Loading a feed from the Internet.
            /// </summary>
            LoadingFeed
        }

        /// <summary>
        /// Records which items are deleted/removed.
        /// </summary>
        private WhichItemsHaveBeenReadDatabase _itemsAlreadyRead;

        /// <summary>
        /// The level of child nodes of the item nodes in an Atom document.
        /// </summary>
        private const int ATOM_ITEM_LEVEL = 2;

        /// <summary>
        /// The level of child nodes of the item nodes in an RSS document.
        /// </summary>
        private const int RSS_ITEM_LEVEL = 3;

        /// <summary>
        /// The level of child nodes of the document node in an Atom document.
        /// </summary>
        private const int ATOM_DOCUMENT_LEVEL = 1;

        /// <summary>
        /// The level of child nodes of the document node in an RSS document.
        /// </summary>
        private const int RSS_DOCUMENT_LEVEL = 2;

        /// <summary>
        /// MyFeedsOPMLDocument is an OPML file defining my subscribed feeds.
        /// </summary>
        private System.Xml.XmlDocument _MyFeedsOPMLDocument;

        /// <summary>
        /// Used for saving feeds.
        /// </summary>
        private string _UserAppDataPath;

        /// <summary>
        /// Flag indicating that this Library object is currently updating the cache of feeds/podcasts in the background. 
        /// </summary>
        private bool _LibraryIsCurrentlyUpdatingCache;

        /// <summary>
        /// The file we're caching to at present.
        /// </summary>
        private static string _UrlOfFeedBeingCached;

        /// <summary>
        /// The currently-selected RSS feed.
        /// </summary>
        private RSSFeedClass _CurrentFeed;

        /// <summary>
        /// The index of the current feed in the _MyFeedsOPMLDocument collection.
        /// </summary>
        private int _CurrentFeedIndex;

        /// <summary>
        /// The number of unread items in each feed.
        /// </summary>
        private System.Collections.Hashtable _UnreadItemCount;

        /// <summary>
        /// A feed has a new current number of items.
        /// </summary>
        public event ItemCountUpdatedHandler ItemCountUpdated;

        /// <summary>
        /// The url of the feed the user is trying to load. 
        /// </summary>
        private string _UrlOfFeedTheUserIsWaitingFor;

        /// <summary>
        /// The path of the cache file to save _UrlOfFeedTheUserIsWaitingFor to.
        /// </summary>
        private string _FilePathOfFeedTheUserIsWaitingFor;

        /// <summary>
        /// Initialise the library from the user's settings - loads all the subscribed RSS feeds/podcasts.
        /// </summary>
        /// <param name="programFolder">Path of the directory containing the program executable.</param>
        /// <param name="userAppDataPath">Path of the user's AppData directory.</param>
        public RSSLibraryClass(string programFolder, string userAppDataPath)
        {
            this._UnreadItemCount = new System.Collections.Hashtable();
            this._MyFeedsOPMLDocument = new System.Xml.XmlDocument();
            this._UserAppDataPath = userAppDataPath;
            this._CurrentFeed = null;
            // Do we have an existing library?
            if (System.IO.File.Exists(this.MyFeedOPMLDocumentPath))
            {
                // Yes, load that.
                this._MyFeedsOPMLDocument.Load(this.MyFeedOPMLDocumentPath);
            }
            else
            {
                // No, first run. Load the defaults for the program.
                try
                {
                    this.MyFeedsOPMLDocument.Load("https://www.webbie.org.uk/podcatcher/defaultpodcasts.opml");
                }
                catch
                {
                    string localPath = programFolder + "\\defaultpodcasts.opml";
                    // Failed to load latest default feeds from server, use local copy.
                    if (System.IO.File.Exists(localPath))
                    {
                        this.MyFeedsOPMLDocument.Load(localPath);
                    }
                    else
                    {
                        throw new Exception("Tried to fall back to loading a local default OPML file containing " +
                            "default podcasts, but this was missing. Add this file to the installer! File sought:" +
                            localPath);
                    }
                    
                }
            }
            //

            // Load the "read items" file. 
            this._itemsAlreadyRead = new WhichItemsHaveBeenReadDatabase((new System.IO.DirectoryInfo(userAppDataPath)).Parent.FullName);

            // Start updating
            //StartUpdatingFeedCacheInTheBackground();
        }

        public void StartUpdatingFeedCacheInTheBackground()
        {
            if (this._LibraryIsCurrentlyUpdatingCache)
            {
                // Already running
            }
            else
            {
                // Start caching in the background.
                this._FeedWithUpdatedItemCountIndex = 0;
                this._LibraryIsCurrentlyUpdatingCache = true;
                StartCachingTheNextFeed();
            }
        }

        void backgroundWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // OK, got a feed. Now need to parse it to find items.
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bwCaching_DoWork;
            bw.RunWorkerAsync();
        }

        void bwCaching_DoWork(object sender, DoWorkEventArgs e)
        {
            RSSFeedClass feed;
            if (LoadFeedFromCacheAndUpdateOutline(RSSLibraryClass._UrlOfFeedBeingCached, out feed, null, false))
            {
                // Loaded okay!
                if (this._UnreadItemCount.ContainsKey(feed.Url))
                {
                    this._UnreadItemCount[feed.Url] = feed.UnreadItems.Count;
                }
                else
                {
                    this._UnreadItemCount.Add(feed.Url, feed.UnreadItems.Count);
                }
                // Raise the event
                if (ItemCountUpdated != null)
                {
                    ItemCountUpdated(feed.Url, feed.UnreadItems.Count);
                }
            }

            if (feed.Url == this._UrlOfFeedTheUserIsWaitingFor)
            {
                // The user is waiting for this feed to parse. 
                ParseLoadedFeed();
            }

            // Go on to the next one.
            StartCachingTheNextFeed(); 
        }
        
        public System.Xml.XmlDocument MyFeedsOPMLDocument
        {
            get
            {
                return this._MyFeedsOPMLDocument;
            }
        }

        /// <summary>
        /// Path to the user's feed OPML library feed. This isn't stored in the UserAppDataPath folder,
        /// because this path changes every version, which would result in many feeds.opml files. Instead
        /// it uses this special path and hops up one directory. I hope this balances respecting the storage
        /// conventions with the needs of the program.
        /// </summary>
        private string MyFeedOPMLDocumentPath
        {
            get
            {
                return (new System.IO.DirectoryInfo(this._UserAppDataPath)).Parent.FullName + "\\feeds.opml";
            }
        }

        /// <summary>
        /// Tries to get the webpage, rather than feed or media url. If the attribute is not present returns "".
        /// </summary>
        /// <param name="OutlineNode"></param>
        /// <returns></returns>
        private string GetOutlineWebpageUrl(System.Xml.XmlNode OutlineNode)
        {
            if (OutlineNode.Attributes.GetNamedItem("htmlUrl") != null)
            {
                return OutlineNode.Attributes.GetNamedItem("htmlUrl").Value;
            }
            else
            {
                // We can live with no value for this.
                return "";
            }
        }

        /// <summary>
        /// Tries to extract the xml url from an OPML outline node. Returns "" if not found. 
        /// </summary>
        /// <param name="OutlineNode"></param>
        /// <returns></returns>
        public static string GetOutlineUrl(System.Xml.XmlNode OutlineNode)
        {
            if (OutlineNode.Attributes.GetNamedItem("xmlUrl") == null)
            {
                if (OutlineNode.Attributes.GetNamedItem("url") == null)
                {
                    // Uh-oh! Didn't find anything useful.
                    return "";
                }
                else
                {
                    return OutlineNode.Attributes.GetNamedItem("url").Value;
                }
            }
            else
            {
                return OutlineNode.Attributes.GetNamedItem("xmlUrl").Value;
            }

        }

        /// <summary>
        /// Removes any (adjacent) identical feeds from the user's OPML document, as judged by the case-sensitive URL.
        /// </summary>
        public void RemoveDuplicateFeeds()
        {
            // Removes duplicate feeds from the list. Now, we could use the xmlUrl, which is canonical, but
            // what if the user has renamed the feed? Well, only a tiny proportion of users will do that, but
            // then this is the same set of users who will call me if there is a problem. Let's assume that
            // these people can work it out themselves, and delete by xmlUrl, which will be more reliable.
            for (int i = this.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline").Count - 2; i >= 0; i--)
            {
                System.Xml.XmlNode firstNode = this.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[i];
                string firstXmlUrl = GetOutlineUrl(firstNode);
                System.Xml.XmlNode secondNode = this.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[i + 1];
                string secondXmlUrl = GetOutlineUrl(secondNode);
                if (firstXmlUrl == secondXmlUrl && firstXmlUrl != "")
                {
                    // Duplicate!
                    this.MyFeedsOPMLDocument.DocumentElement.SelectSingleNode("body").RemoveChild(secondNode);
                }
            }
        }

        /// <summary>
        /// Sort the feed list (which is an OPML file) by title.
        /// </summary>
        public void SortFeeds()
        {
            if (this.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline").Count < 2)
                return;

            bool swapped = true;
            while (swapped)
            {
                swapped = false;
                for (int i = 0; i < this.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline").Count - 1; i++)
                {
                    System.Xml.XmlNode firstNode = this.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[i];
                    System.Xml.XmlNode secondNode = this.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[i + 1];
                    string firstTitle = GetOutlineTitle(firstNode);
                    string secondTitle = GetOutlineTitle(secondNode);
                    if (string.Compare(firstTitle, secondTitle, true, System.Globalization.CultureInfo.CurrentCulture) > 0)
                    {
                        // Need to swap.
                        swapped = true;
                        System.Xml.XmlNode tempNode = this.MyFeedsOPMLDocument.DocumentElement.SelectSingleNode("body").RemoveChild(secondNode);
                        this.MyFeedsOPMLDocument.DocumentElement.SelectSingleNode("body").InsertBefore(tempNode, firstNode);
                    }
                }
            }
        }

        /// <summary>
        /// Tries to get the title or text of an outline node. If not found, returns "".
        /// </summary>
        /// <param name="OutlineNode"></param>
        /// <returns></returns>
        private string GetOutlineTitle(System.Xml.XmlNode OutlineNode)
        {
            if (OutlineNode.Attributes.GetNamedItem("title") == null)
            {
                if (OutlineNode.Attributes.GetNamedItem("text") == null)
                {
                    return "";
                }
                else
                {
                    return OutlineNode.Attributes.GetNamedItem("text").Value;
                }
            }
            else
            {
                return OutlineNode.Attributes.GetNamedItem("title").Value;
            }

        }

        /// <summary>
        /// Save the user's feeds as an OPML file to the path provided.
        /// </summary>
        /// <param name="path">Path of the output OPML file.</param>
        public void ExportFeedOPMLDocument(string path)
        {
            this._MyFeedsOPMLDocument.Save(path);
        }

        /// <summary>
        /// Makes the library save the current set of feeds and read items. Call before closing
        /// any program using the library.
        /// </summary>
        public void SaveState()
        {
            ExportFeedOPMLDocument(this.MyFeedOPMLDocumentPath);
            // SaveState any read/hidden items.
            this._itemsAlreadyRead.WriteItems(this._CurrentFeed);
            // SaveState what's read
            this._itemsAlreadyRead.Save();
        }

        public bool AddFeed(System.Xml.XmlNode outlineNode)
        {
            try
            {
                string xmlUrl = GetOutlineUrl(outlineNode);
                if (xmlUrl == "")
                {
                    // Well, this isn't going to work!
                    return false;
                }
                string title = GetOutlineTitle(outlineNode);
                if (title == "")
                {
                    // No name for it. Let's give up.
                    return false;
                }
                string htmlUrl = GetOutlineWebpageUrl(outlineNode);
                return AddFeed(xmlUrl, title, htmlUrl);
            }
            catch
            {
                // Failed to parse outline node.
                System.Diagnostics.Debug.Print("Failed to import: " + outlineNode.OuterXml);
                return false;
            }
        }

        /// <summary>
        /// Creates an outline node in the feed document, populates it with
        /// the values provided, adds it to end of the feed list.
        /// </summary>
        /// <param name="url">The actual Atom or RSS file.</param>
        /// <param name="title">The human-readable title/name</param>
        /// <param name="websiteUrl">The URL of the website that "owns" this feed.</param>
        public bool AddFeed(string xmlUrl, string title, string websiteUrl)
        {
            //if (title.Length == 0)
            //{
            //    throw new ArgumentException("Missing title attribute for new feed");
            //}
            // Does this feed already exist? If so, don't add.
            if (this.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline[@xmlUrl=\"" + xmlUrl + "\"]").Count == 0)
            {
                System.Xml.XmlNode newOutline = this._MyFeedsOPMLDocument.CreateElement("outline");
                System.Xml.XmlAttribute attr;
                attr = this._MyFeedsOPMLDocument.CreateAttribute("title");
                attr.Value = title;
                newOutline.Attributes.SetNamedItem(attr);
                attr = this._MyFeedsOPMLDocument.CreateAttribute("text");
                attr.Value = title;
                newOutline.Attributes.SetNamedItem(attr);
                attr = this._MyFeedsOPMLDocument.CreateAttribute("type");
                attr.Value = "rss";
                newOutline.Attributes.SetNamedItem(attr);
                attr = this._MyFeedsOPMLDocument.CreateAttribute("xmlUrl");
                attr.Value = xmlUrl;
                newOutline.Attributes.SetNamedItem(attr);
                attr = this._MyFeedsOPMLDocument.CreateAttribute("htmlUrl");
                attr.Value = websiteUrl;
                newOutline.Attributes.SetNamedItem(attr);
                this._MyFeedsOPMLDocument.DocumentElement.SelectSingleNode("body").AppendChild(newOutline);
                SortFeeds();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the path to where the downloaded feed will be store. This will be in user settings, %appdata%.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetCachePath(string url)
        {
            string cachedFilename = SanitizeUrlForFilename(url);
            return new System.IO.DirectoryInfo(this._UserAppDataPath).Parent.FullName + "\\" + cachedFilename;
        }

        /// <summary>
        /// Converts an HTTP(s) URL into a valid Windows filename by replacing invalid characters with _
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string SanitizeUrlForFilename(string url)
        {
            StringBuilder sb = new StringBuilder(url.Length);
            char[] urlCharacters = url.ToCharArray();
            for (int i = 0; i < urlCharacters.Length; i++ )
            {
                char urlChar = urlCharacters[i];
                if (System.IO.Path.GetInvalidPathChars().Contains(urlChar))
                {
                    sb.Append("_");
                }
                else if (System.IO.Path.GetInvalidFileNameChars().Contains(urlChar))
                {
                    sb.Append("_");
                }
                else
                {
                    sb.Append(urlChar);
                }
            }
            return sb.ToString();
        }

        private void ParseLoadedFeed()
        {
            System.Diagnostics.Debug.Print("Starting parse");
            BackgroundWorker bwCurrent = new BackgroundWorker();
            bwCurrent.DoWork += bwCurrent_DoWork;
            bwCurrent.RunWorkerAsync();
        }

        private void bwCurrent_DoWork(object sender, DoWorkEventArgs e)
        {
            RSSFeedClass feed;
            System.Xml.XmlNode outlineNode = this._MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[this._CurrentFeedIndex];
            string url = GetOutlineUrl(outlineNode);
            if (LoadFeedFromCacheAndUpdateOutline(url, out feed, outlineNode, true))
            {
                this._CurrentFeed = feed;
                ItemCountUpdated(url, feed.UnreadItems.Count);
                LoadFinished();
            }
            // Finished.
            System.Diagnostics.Debug.Print("FINISHED feed load.");
        }


        /// <summary>
        /// User-requested feed.
        /// </summary>
        /// <param name="feedIndex"></param>
        public void SelectFeed(int feedIndex)
        {
            System.Diagnostics.Debug.Print("Starting SelectFeed(" + feedIndex + ")");
            // What is this feed? Is it a valid index?
            if (feedIndex < 0 || feedIndex > this._MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline").Count - 1)
            {
                throw new IndexOutOfRangeException();
            }

            this._CurrentFeedIndex = feedIndex;
            LoadProgress("Opening feed...");

            if (!(this._CurrentFeed == null))
            {
                // First save any read/hidden items.
                _itemsAlreadyRead.WriteItems(this._CurrentFeed);
            }


            // Get feed details.
            System.Xml.XmlNode outlineNode = this._MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[this._CurrentFeedIndex];
            string url = GetOutlineUrl(outlineNode);
            this._UrlOfFeedTheUserIsWaitingFor = url;

            if (url == RSSLibraryClass._UrlOfFeedBeingCached)
            {
                // Whoa, we're already loading this!
                // Yeah, but still have to notify that we're going to display it - which we do by setting
                // the url, above.
                return;
            }

            string cachedPath = GetCachePath(url);
            System.Diagnostics.Debug.Print("URL: " + Environment.NewLine + "\t" + url);

            // The logic is:
            // * if the feed is missing from the cache, download.
            // * if the feed is present in the cache, and less than fifteen seconds old, serve that.
            // * if the feed is present in the cache, and more than fifteen second old, download it. 
            // * if the feed fails to load then serve the cache version.

            bool needToDownload = false;
            if (System.IO.File.Exists(cachedPath))
            {
                System.Diagnostics.Debug.Print("Feed found in cache: " + Environment.NewLine + "\t" + cachedPath);
                // OK, is it < 5 mins old?
                System.IO.FileInfo fi = new System.IO.FileInfo(cachedPath);
                bool olderThanTimeSpan = fi.LastWriteTime < DateTime.Now - new TimeSpan(0, 1, 0);
                if (olderThanTimeSpan)
                {
                    // Need to download afresh. 
                    System.Diagnostics.Debug.Print("Feed old, re-download.");
                    needToDownload = true;
                }
                else if (fi.Length == 0)
                {
                    // Zero-length file? Think I'd better download it!
                    System.Diagnostics.Debug.Print("Feed zero bytes, re-download.");
                    needToDownload = true;
                }
            }
            else
            {
                System.Diagnostics.Debug.Print("Feed not found in cache.");
                needToDownload = true;
            }
            if (needToDownload)
            {
                LoadProgress("Checking feed address is valid...");
                // We haven't got this file. Need to download the file to the cache.
                System.Diagnostics.Debug.Print("Starting feed download process (start by checking URL)");

                // OK, let's go. Start by checking the url.
                BackgroundWorker bwGetFeed = new BackgroundWorker();
                this._FilePathOfFeedTheUserIsWaitingFor = cachedPath;
                bwGetFeed.DoWork += bwGetFeed_DoWork;
                bwGetFeed.RunWorkerAsync();
            }
            else
            {
                // OK, easy, used the cached one.
                LoadProgress("Parsing feed...");
                ParseLoadedFeed();
            }
        }

        /// <summary>
        /// Download the RSS feed the user has requested in the background, so it doesn't hang the UI
        /// thread. To be clear though: this is the handler for the requested RSS feed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bwGetFeed_DoWork(object sender, DoWorkEventArgs e)
        {
            if (UrlIsOk(this._UrlOfFeedTheUserIsWaitingFor))
            {
                System.Diagnostics.Debug.Print("URL OK, can download.");

                // OK, looks fine, go on.
                LoadProgress("Getting feed from Internet...");
                System.Net.WebClient wc = new System.Net.WebClient();
                try
                {
                    wc.DownloadFileCompleted += wc_DownloadFileCompleted;
                    System.Diagnostics.Debug.Print("Starting actual file download from:" + Environment.NewLine + "\t" + this._UrlOfFeedTheUserIsWaitingFor + Environment.NewLine + "\t" + this._FilePathOfFeedTheUserIsWaitingFor);
                    wc.DownloadFileAsync(new System.Uri(this._UrlOfFeedTheUserIsWaitingFor), this._FilePathOfFeedTheUserIsWaitingFor);
                }
                catch
                {
                    //message = ex.Message;
                    //return ParseResult.CouldNotGetRightNowMaybeNetwork;
                    LoadFailed(this._UrlOfFeedTheUserIsWaitingFor, ParseResult.CouldNotGetRightNowMaybeNetwork, "Could not get news feed: probably your network, check you are online.");
                }
            }
            else
            {
                System.Diagnostics.Debug.Print("URL failed");
                //"Cannot get the RSS news feed. You might not be online, or the website may be down, or the feed is no longer there.");
                //return ParseResult.CouldNotGetRightNowMaybeNetwork;
                if (System.IO.File.Exists(this._FilePathOfFeedTheUserIsWaitingFor))
                {
                    // Serve the cached version of the file.
                    LoadProgress("Parsing feed...");
                    ParseLoadedFeed();
                }
                else
                {
                    LoadFailed(this._UrlOfFeedTheUserIsWaitingFor, ParseResult.InvalidURL, "Could not load news feed: the address (URL) you have given " +
                        "for it looks like it is wrong: " + this._UrlOfFeedTheUserIsWaitingFor);
                }
                return;
            }
        }

        /// <summary>
        /// Feed has been downloaded from the Internet to the local cache, and can now be cached.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Debug.Print("Finished file downloading");
            LoadProgress("Feed downloaded, now parsing...");
            // So it's in the cache now, so I can go ahead and call SelectFeed()...
            SelectFeed(this._CurrentFeedIndex);
        }

        /// <summary>
        /// Load the feed from the cache and attempt to parse it, at the same time updating the OPML item that 
        /// represents it. (Not terribly well-engineered, there!)
        /// </summary>
        /// <param name="UrlToIdentifyFeed">The URL of the feed online (not accessed, it uses the cache)</param>
        /// <param name="OutputFeed">Returns the RSSFeed result.</param>
        /// <param name="OutlineNode">The OPML node in _MyFeedsOPMLDocument corresponding to this feed.</param>
        /// <param name="SelectedFeed">Set to true if this is the feed the user is waiting for, false 
        /// otherwise (i.e. false when just caching the feed)</param>
        /// <returns></returns>
        private bool LoadFeedFromCacheAndUpdateOutline(string UrlToIdentifyFeed, out RSSFeedClass OutputFeed, System.Xml.XmlNode OutlineNode, bool SelectedFeed)
        {
            string cachedPath = GetCachePath(UrlToIdentifyFeed);
            OutputFeed = new RSSFeedClass(UrlToIdentifyFeed);
            
            //The MSDN documentation says it's best to create an XmlTextReader through .Create
            //rather than = new XmlTextReader. However, if you do it through new, you get an
            //XmlTextReader that doesn't choke on malformed XML, and if you do it through
            //.Create you choke on malformed XML. So clearly new is better!
            System.Xml.XmlTextReader xtr;
            try
            {
                //xtr = new System.Xml.XmlTextReader(this._CurrentFeed.url);
                //xtr = new System.Xml.XmlTextReader("C:\\Users\\Alasdair\\SkyDrive\\Projects\\RSSParseTest\\RSSParseTest\\Example Atom Feed.xml");
                xtr = new System.Xml.XmlTextReader(GetCachePath(UrlToIdentifyFeed));
            }
            catch (System.UriFormatException ufe)
            {
                if (SelectedFeed)
                {
                    LoadFailed(UrlToIdentifyFeed, ParseResult.InvalidURL, "Invalid web address (URL) for this news feed: " + ufe.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                // Unknown error - File Not Found, possibly - display and quit.
                if (SelectedFeed)
                {
                    LoadFailed(UrlToIdentifyFeed, ParseResult.UnknownError, "Error parsing the news feed: " + ex.Message);
                }
                return false;
            }

            // Fix entities. Hmmm, I think the problem is actually malformed input. For example:
            // <title><![CDATA[The World&#8217;s Simplest HTML5 WYSISYG Inline Editor]]></title>
            // So leave it for now.

            //xtr.EntityHandling = System.Xml.EntityHandling.ExpandEntities;
            // Now to parse the XML stream into the rendered feed. This is complicated by the
            // fact that many feeds are invalid XML, so we must use an XmlTextReader rather than
            // traversing the DOM.

            // If we run into a problem reading, stop.
            bool readOK = true;

            // States
            string rootNodeName = ""; // The root node: indicates RSS or Atom
            string title = "";
            bool inImageNode = false;
            NextTextNodeIsEnum nextTextNodeContains = NextTextNodeIsEnum.Title;
            string link = "";
            string pubDate = "";
            string contents = "";
            string enclosure = "";
            bool isRss = false;
            bool isAtom = false;
            while (!xtr.EOF && readOK)
            {
                try
                {
                    readOK = xtr.Read();
                    if (xtr.NodeType == System.Xml.XmlNodeType.Element)
                    {
                        string elementName = xtr.Name.ToLowerInvariant();
                        // If this is the first node then it is the root node. 
                        // Remember the name so that we know what type of document
                        // the user has given us - Atom, RSS, or a web page to
                        // search.
                        if (rootNodeName.Length == 0)
                        {
                            rootNodeName = elementName;
                            if (rootNodeName == "rss")
                            {
                                isRss = true;
                            }
                            else if (rootNodeName == "feed")
                            {
                                isAtom = true;
                            }
                            else if (rootNodeName == "html")
                            {
                                // This is an HTML document.
                                // Can't load HTML files as RSS!
                                xtr.Close();
                                if (SelectedFeed)
                                {
                                    LoadFailed(UrlToIdentifyFeed, ParseResult.ThisIsHTMLNotRSSOrAtom, "This news feed is an HTML file, not RSS or Atom.");
                                }
                                return false;
                            }
                            else
                            {
                                // Alert user not RSS or Atom 
                                //message = rootNodeName;
                                xtr.Close();
                                if (SelectedFeed)
                                {
                                    LoadFailed(UrlToIdentifyFeed, ParseResult.InvalidDocumentType, System.String.Format("Not an RSS or Atom news feed or podcast (root node is '{0}')", rootNodeName));
                                }
                                return false;
                                //return ParseResult.InvalidDocumentType; 
                            }
                        }
                        else if (isRss || isAtom)
                        {
                            // This is an RSS document.
                            // RSS ("rss") or Atom ("feed")

                            // OK, do we do anything with this node?
                            if (elementName == "item" || elementName == "entry") // entry for Atom, I think. V5.
                            {
                                // It's a new item, so if we have already got valid information add the item now, unless
                                // we were "in" an image node: if we were in an image node, the title and link we have
                                // collected are bogus. 
                                if (title.Length > 0 && link.Length > 0 && !inImageNode)
                                {
                                    RSSItemClass rssItem = new RSSItemClass(title, link, OutputFeed.Url, pubDate, contents, enclosure);
                                    OutputFeed.AddItem(rssItem);
                                    if (enclosure != "")
                                    {
                                        //We've found an enclosure, so this is a podcast. 
                                        IsPodcast = true;
                                    }
                                }
                                inImageNode = false; // OK, we're really not in an image node now .
                                title = "";
                                link = "";
                                pubDate = "";
                                contents = "";
                                enclosure = "";
                            }
                            else if (elementName == "title")
                            {
                                if ((xtr.Depth == RSS_ITEM_LEVEL && isRss) || (xtr.Depth == ATOM_ITEM_LEVEL && isAtom))
                                {
                                    // <title> element of an <item> (rss) or <entry> (Atom), not of the document. 
                                    // But could also be the <title> element of an <image> element, which is not valid.
                                    nextTextNodeContains = NextTextNodeIsEnum.Title;
                                }
                            }
                            else if (elementName == "link")
                            {
                                if ((xtr.Depth == RSS_DOCUMENT_LEVEL && isRss) || (xtr.Depth == ATOM_DOCUMENT_LEVEL && isAtom))
                                {
                                    // Check for rel="self", which is the feed itself in Atom. The attribute
                                    // will be missing in RSS, so the test catches that too.
                                    if (xtr.GetAttribute("rel") != "self")
                                    {
                                        // <link> element for the document (not for the <item> or <entry> in the feed)
                                        // Populate/update (missing) webpage url in my OPML file if required.
                                        if (xtr.GetAttribute("href") == "" || xtr.GetAttribute("href") == null)
                                        {
                                            // RSS
                                            nextTextNodeContains = NextTextNodeIsEnum.Url;
                                        }
                                        else
                                        {
                                            // Atom
                                            if (OutlineNode != null)
                                            {
                                                OutlineNode.Attributes.GetNamedItem("htmlUrl").Value = xtr.GetAttribute("href");
                                            }
                                        }
                                    }
                                }
                                else if ((xtr.Depth == RSS_ITEM_LEVEL && isRss) || (xtr.Depth == ATOM_ITEM_LEVEL && isAtom))
                                {
                                    // <link> element for an <item> or <entry> (not for the feed as a whole)
                                    if (xtr.GetAttribute("href") == "" || xtr.GetAttribute("href") == null)
                                    {
                                        // RSS2
                                        nextTextNodeContains = NextTextNodeIsEnum.Link;
                                    }
                                    else
                                    {
                                        // Atom
                                        if (xtr.GetAttribute("rel") == "alternate")
                                        {
                                            link = xtr.GetAttribute("href");
                                        }
                                    }
                                }
                            }
                            else if (elementName == "dc:date" || elementName == "published" || elementName == "pubDate" || elementName == "pubdate")
                            {
                                // Next node contents is date of publication
                                nextTextNodeContains = NextTextNodeIsEnum.PubDate;
                            }
                            else if (elementName == "content" || elementName == "description")
                            {
                                nextTextNodeContains = NextTextNodeIsEnum.Contents;
                            }
                            else if (elementName == "image")
                            {
                                // We don't use these - otherwise some sites give you a random link at the top. 
                                inImageNode = true;
                            }
                            else if (elementName == "enclosure")
                            {
                                if (xtr.GetAttribute("url") != null)
                                {
                                    if (xtr.GetAttribute("url") != "")
                                    {
                                        enclosure = xtr.GetAttribute("url");
                                    }
                                }
                            }
                            else
                            {
                                // Element we don't use.
                                System.Diagnostics.Debug.Print("Element we don't use when parsing RSS/Atom:" + elementName);
                            }
                        }
                    }
                    else if ((xtr.NodeType == System.Xml.XmlNodeType.Text) || (xtr.NodeType == System.Xml.XmlNodeType.CDATA))
                    {
                        if (nextTextNodeContains == NextTextNodeIsEnum.Title)
                        {
                            title = xtr.Value;
                            nextTextNodeContains = NextTextNodeIsEnum.Unknown;
                        }
                        if (nextTextNodeContains == NextTextNodeIsEnum.Link)
                        {
                            link = xtr.Value;
                            nextTextNodeContains = NextTextNodeIsEnum.Unknown;
                        }
                        if (nextTextNodeContains == NextTextNodeIsEnum.Url)
                        {
                            if (OutlineNode != null)
                            {
                                // Update the outline node's link to the actual web page, if provided. 
                                if (OutlineNode.Attributes.GetNamedItem("htmlUrl") != null)
                                {
                                    OutlineNode.Attributes.GetNamedItem("htmlUrl").Value = xtr.Value;
                                }
                            }
                            nextTextNodeContains = NextTextNodeIsEnum.Unknown;
                        }
                        if (nextTextNodeContains == NextTextNodeIsEnum.Contents)
                        {
                            contents = xtr.Value;
                            nextTextNodeContains = NextTextNodeIsEnum.Unknown;
                        }
                        if (nextTextNodeContains == NextTextNodeIsEnum.PubDate)
                        {
                            pubDate = xtr.Value;
                            nextTextNodeContains = NextTextNodeIsEnum.Unknown;
                        }
                    }
                }
                catch (System.Net.WebException wexc)
                {
                    if (SelectedFeed)
                    {
                        LoadFailed(UrlToIdentifyFeed, ParseResult.CouldNotGetRightNowMaybeNetwork, "Network error: " + wexc.Message); 
                    }
                    // Get out.
                    readOK = false;
                    return false;
                }
                catch
                {
                    //Parsing error from the web page or XML = very common, carry on going
                    //until we find something useful.
                    System.Diagnostics.Debug.Print("Error parsing: ");
                }
            }
            xtr.Close();
            if (SelectedFeed)
            {
                LoadProgress("Loaded feed...");
            }

            // Do pubdate and contents for the last node.
            //TODO this smells bad: I suspect this will have parsing errors, especially with the title/link/enclosure nulling below. 
            if (title.Length > 0 && link.Length > 0)
            {
                RSSItemClass rssItem = new RSSItemClass(title, link, OutputFeed.Url, pubDate, contents, enclosure);
                OutputFeed.AddItem(rssItem);
                title = "";
                link = "";
                enclosure = ""; // this isn't the same set of "" as above! Smells bad. 
            }

            // Work out which items are read or not.
            OutputFeed.IdentifyReadItems(this._itemsAlreadyRead);
            return true;
        }


        public RSSFeedClass currentFeed
        {
            get {
                return this._CurrentFeed;
            }
        }

        /// <summary>
        /// Download any feeds to the cache. 
        /// </summary>
        private void StartCachingTheNextFeed()
        {
            System.Xml.XmlNodeList nl = this.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline");
            bool fileDownloadRequired = false;
            string url = "";

            // First look a file that's completely missing OR _FeedWithUpdatedItemCountIndex indicates
            // that we haven't updated it this time through yet.
            string cacheFile = "";
            for (int i = 0; i < nl.Count; i++)
            {
                url = GetOutlineUrl(nl[0]);
                cacheFile = GetCachePath(url);
                if (System.IO.File.Exists(cacheFile))
                {
                    // Exists! Do we still need to download a new one, because we're doing a run through
                    // updating everything?
                    if  (i >= this._FeedWithUpdatedItemCountIndex)
                    {
                        // Yes, we do. Do in background:
                        this._FeedWithUpdatedItemCountIndex = i + 1;
                        fileDownloadRequired = true;
                        RSSLibraryClass._UrlOfFeedBeingCached = url;
                        break;
                    }
                }
                else
                {
                    fileDownloadRequired = true;
                    RSSLibraryClass._UrlOfFeedBeingCached = url;
                    break;
                }
            }
            if (!fileDownloadRequired)
            {
                // Nope, all got. Look for a file we haven't checked for more than five minutes.
                System.DateTime fiveMinutesAgo = System.DateTime.UtcNow;
                fiveMinutesAgo = fiveMinutesAgo - new System.TimeSpan(0, 5, 0);
                for (int i = 0; i < nl.Count; i++)
                {
                    cacheFile = GetCachePath(url);
                    System.IO.FileInfo fi = new System.IO.FileInfo(cacheFile);
                    if (fi.LastWriteTimeUtc < fiveMinutesAgo)
                    {
                        fileDownloadRequired = true;
                        RSSLibraryClass._UrlOfFeedBeingCached = url;
                        break;
                    }
                }
            }
            if (fileDownloadRequired)
            {
                // Found a feed that's completely missing, or old. So download it.
                RSSLibraryClass._UrlOfFeedBeingCached = url;
                System.Net.WebClient backgroundWebClient = new System.Net.WebClient();
                backgroundWebClient.DownloadFileCompleted += backgroundWebClient_DownloadFileCompleted;
                backgroundWebClient.DownloadFileAsync(new System.Uri(url), cacheFile);
            }
        }

        public void RemoveAllFeeds()
        {
            this._MyFeedsOPMLDocument.DocumentElement.SelectSingleNode("body").RemoveAll();
            this._itemsAlreadyRead.ClearAllItemReadStatuses();
            this._CurrentFeed = null;
        }

        public void RemoveFeed(int index)
        {
            System.Xml.XmlNode outlineNode = this._MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[index];
            this._itemsAlreadyRead.RemoveFeed(outlineNode.Attributes.GetNamedItem("xmlUrl").InnerText);
            this._MyFeedsOPMLDocument.DocumentElement.SelectSingleNode("body").RemoveChild(outlineNode);
        }

        private bool UrlIsOk(string url)
        {
            System.Net.HttpWebRequest myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            myHttpWebRequest.Method = "GET";
            // Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
            System.Net.HttpWebResponse myHttpWebResponse;
            try
            {
                myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();
            }
            catch (System.Net.WebException we)
            {
                // Probably not online!
                LoadFailed(url, ParseResult.CouldNotGetRightNowMaybeNetwork, "Error connecting to the news feed website: " + we.Message);
                return false;
            }
            System.Net.HttpStatusCode statusCode = myHttpWebResponse.StatusCode;
            myHttpWebResponse.Close();
            switch (statusCode)
            {
                case System.Net.HttpStatusCode.Accepted:
                    return true;
                case System.Net.HttpStatusCode.Ambiguous:
                    return true;
                case System.Net.HttpStatusCode.Continue:
                    return true;
                case System.Net.HttpStatusCode.Created:
                    return true;
                case System.Net.HttpStatusCode.Found:
                    return true;
                case System.Net.HttpStatusCode.Moved:
                    return true;
                //case System.Net.HttpStatusCode.MovedPermanently:  Same as Moved
                //    return true;
                //case System.Net.HttpStatusCode.MultipleChoices:   Same as Ambiguous
                //    return true;
                case System.Net.HttpStatusCode.NoContent:
                    return true;
                case System.Net.HttpStatusCode.NonAuthoritativeInformation:
                    return true;
                case System.Net.HttpStatusCode.NotModified:
                    return true;
                case System.Net.HttpStatusCode.OK:
                    return true;
                case System.Net.HttpStatusCode.PartialContent:
                    return true;
                //case System.Net.HttpStatusCode.Redirect:    Same as Found
                //    return true;
                case System.Net.HttpStatusCode.RedirectKeepVerb:
                    return true;
                case System.Net.HttpStatusCode.RedirectMethod:
                    return true;
                case System.Net.HttpStatusCode.ResetContent:
                    return true;
                //case System.Net.HttpStatusCode.SeeOther:    Same as RedirectMethod
                //    return true;
                case System.Net.HttpStatusCode.SwitchingProtocols:
                    return true;
                //case System.Net.HttpStatusCode.TemporaryRedirect: Same as RedirectKeepVerb
                //    return true;
                case System.Net.HttpStatusCode.Unused:
                    return true;
                case System.Net.HttpStatusCode.UseProxy:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// XML found in RSS often has rubbish character encoding. This tries to clean it up.
        /// </summary>
        /// <param name="xml">The text from your XML file. </param>
        /// <returns>The fixed string.</returns>
        public static string CleanUpXml(string xml)
        {
            return xml.Replace("&#8217;", '\''.ToString()).Replace("&#8211;", "-").Replace("&#8216", '\x8216'.ToString()).Replace("&#8220", '\x8220'.ToString()).Replace("&#8221", '\x8221'.ToString()).Replace("&amp;", "&").Replace("&rsquo;", '\x8217'.ToString()).Replace("&lsquo;", '\x8216'.ToString()).Replace("%lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'").Replace("&nbsp;", " ").Replace("&#160;", " ").Replace("&#039;", "'").Replace("&#xa;", " ").Trim();
        }

    }
}
