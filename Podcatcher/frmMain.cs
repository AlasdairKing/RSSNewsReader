using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RSSCommon;

// Podcatcher
//  A branch of .Net RSS News Reader that supports podcasts, at least for playing. 
//
//  Note:
//      Microsoft.VisualBasic.Logging.Log.WriteException(ex, System.Diagnostics.TraceEventType.Error, "Exception in Main(): " + ex.Message);   

namespace Podcatcher
{
    /// <summary>
    /// Podcatcher main form, contains the main UI.
    /// </summary>
    public partial class frmMain : Form
    {

        /// <summary>
        /// The RSS feeds to which we have subscribed.
        /// </summary>
        private RSSLibraryClass _Library;

        /// <summary>
        /// For internationalisation.
        /// </summary>
        private I18N _I18N;

        delegate void UpdateFeedStatusDelegate(string url, string message);
        delegate void DisplayStatusMessageDelegate(string message);
        delegate void DisplayItemsDelegate();
        delegate void UpdateItemCountDelegate(string url, int count);

        private DateTime _LastChecked;

        public frmMain()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CheckForUpdates()
        {
            // Have we checked already today?
            if (Properties.Settings.Default.UpdateCheck != System.DateTime.Now.ToShortDateString())
            {
                // No! Let's do so.
                // First note that we have now checked today.
                Properties.Settings.Default.UpdateCheck = System.DateTime.Now.ToShortDateString();
                Properties.Settings.Default.Save();
                
                WebbIE.Updater.CheckForUpdates("http://www.webbie.org.uk/rssnewsreader/updates.xml");
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Check for updates. Only do this is WebbIEUpdater.dll is found. This is so that we can do a 
            //build without the updater for the Windows 10 Store. 
            string exeFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            if (System.IO.File.Exists(exeFolder + "\\WebbIEUpdater.dll"))
                CheckForUpdates();

            // Normal WebbIE I18N code.
            this._I18N = new I18N();
            this._I18N.DoForm(this);

            // Load the user-subscribed list of feeds.
            this._Library = new RSSLibraryClass(Environment.CurrentDirectory, Application.UserAppDataPath);
            this._Library.ItemCountUpdated += _Library_itemCountUpdated;
            // Handle the events when the user has requested a feed.
            this._Library.LoadProgress += _Library_LoadProgress;
            this._Library.LoadFailed += _Library_LoadFailed;
            this._Library.LoadFinished += _Library_LoadFinished;

            // Updating previous versions. Check for feeds.xml. This will be located in:
            string existingFeedFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WebbIE\\AccessibleRSS\\1\\feeds.xml";
            if (System.IO.File.Exists(existingFeedFile))
            {
                // We have an existing feed file. Do we need to import it? Have we already?
                System.IO.FileInfo fi = new System.IO.FileInfo(existingFeedFile);
                if (fi.LastWriteTimeUtc > Properties.Settings.Default.ImportedPreviousVersionFeeds)
                {
                    // Need to import.
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.Load(existingFeedFile);
                    foreach (System.Xml.XmlNode node in doc.DocumentElement.SelectNodes("feed"))
                    {
                        // I do not get the webpage url, but I've added code so when it is loaded the htmlUrl
                        // is obtained from the feed XML document.
                        this._Library.AddFeed(node.SelectSingleNode("url").InnerText, node.SelectSingleNode("title").InnerText, "");
                    }
                }
                // Ideally I'd now do the Read section, but I've changed how the settings are saved (why? bad move)
                // so it's hard so I'm not going to do it.
            }
            Properties.Settings.Default.ImportedPreviousVersionFeeds = System.DateTime.UtcNow;
            
            // Open any OPML file that's provided and add it to the existing list.
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                // Been passed an OPML file, presumably.
                string opmlFile = Environment.GetCommandLineArgs()[1];
                string url = "";
                for (int i = 1; i < Environment.GetCommandLineArgs().Length; i++)
                {
                    url += Environment.GetCommandLineArgs()[i] + " ";
                }
                ImportOPML(url);
            }

            this.deletedItemsToolStripMenuItem.Checked = Properties.Settings.Default.ViewDeletedItems;

            // OK, I have a recurring bug where I end up with duplicates and unsorted feeds, so let's sort it
            // right now.
            this._Library.SortFeeds();
            this._Library.RemoveDuplicateFeeds();
            
            // Display the current feeds.
            DisplayFeeds();
            if (lstFeeds.Items.Count > 0 && Properties.Settings.Default.SelectedFeedIndex < lstFeeds.Items.Count - 1)
            {
                lstFeeds.SelectedIndex = Properties.Settings.Default.SelectedFeedIndex;
            }
            if (lstFeeds.SelectedIndex == -1 && lstFeeds.Items.Count > 0)
            {
                lstFeeds.SelectedIndex = 0;
            }

            // Add sounds to list boxes.
            ListBoxSounds.AddSounds(this);

            // Set window state
            this.WindowState = Properties.Settings.Default.WindowState;

            // Start the library refreshing the list of read/unread items.
            this._Library.StartUpdatingFeedCacheInTheBackground();
            // Remember that we've done this.
            this._LastChecked = DateTime.Now;

            #region MakeListboxWrapText1of2
            /// <see cref="https://stackoverflow.com/questions/17613613/winforms-dotnet-listbox-items-to-word-wrap-if-content-string-width-is-bigger-tha"/>
            lstItems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            lstItems.MeasureItem += lst_MeasureItem;
            lstItems.DrawItem += lst_DrawItem;
            #endregion

        }

        #region MakeListboxWrapText2of2
        private void lst_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            e.ItemHeight = (int)e.Graphics.MeasureString(lstItems.Items[e.Index].ToString(), lstItems.Font, lstItems.Width).Height;
        }
        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(lstItems.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }
        #endregion

        void _Library_LoadFinished()
        {
            // Loaded okay!
            string newText = "Loaded feed OK";
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DisplayStatusMessageDelegate(DisplayStatusMessage), newText);
                this.BeginInvoke(new DisplayItemsDelegate(DisplayItems));
            }
            else
            {
                DisplayStatusMessage(newText);
                DisplayItems();
            }
        }

        void UpdateFeedStatus(string url, string message)
        {
            int counter = 0;
            foreach (System.Xml.XmlNode outlineNode in this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline"))
            {
                string href = RSSLibraryClass.GetOutlineUrl(outlineNode);
                if (href == url)
                {
                    // Found it.
                    string listItem = (string)lstFeeds.Items[counter];
                    if (listItem.EndsWith(")") && listItem.Contains("("))
                    {
                        listItem = listItem.Substring(0, listItem.LastIndexOf("(")).Trim();
                    }
                    lstFeeds.Items[counter] = listItem + " (" + _I18N.GetText("Error") + ")";
                    if (counter == lstFeeds.SelectedIndex)
                    {
                        this.lblStatus.Text = message; 
                    }
                    break;
                }
                counter++;
            }
            if (url == this._Library.currentFeed.Url)
            {
                MessageBox.Show(this, "Failed to load feed: " + message,Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void _Library_LoadFailed(string url, RSSLibraryClass.ParseResult result, string message)
        {
            if (this.InvokeRequired)
            {
                object[] prms = { url, message };
                this.BeginInvoke(new UpdateFeedStatusDelegate(UpdateFeedStatus), prms);
            }
            else
            {
                UpdateFeedStatus(url, message);
            }
        }

        void DisplayStatusMessage(string message)
        {
            this.lblStatus.Text = message;
         
        }

        void _Library_LoadProgress(string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new DisplayStatusMessageDelegate(DisplayStatusMessage), message);
            }
            else
            {
                DisplayStatusMessage(message);
            }
        }

        /// <summary>
        /// We have identified a number of unread items on an item.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="count"></param>
        void _Library_itemCountUpdated(string url, int count)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UpdateItemCountDelegate(UpdateItemCount), url, count);
            }
            else
            {
                UpdateItemCount(url, count);
            }
        }

        void UpdateItemCount(string url, int count)
        {
            int counter = 0;
            foreach (System.Xml.XmlNode outlineNode in this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline"))
            {
                string href = RSSLibraryClass.GetOutlineUrl(outlineNode);
                if (href == url)
                {
                    // Found it.
                    string listItem = (string)lstFeeds.Items[counter];
                    if (listItem.EndsWith(")") && listItem.Contains("("))
                    {
                        listItem = listItem.Substring(0, listItem.LastIndexOf("(")).Trim();
                    }
                    lstFeeds.Items[counter] = listItem + " (" + count + ")";
                }
                counter++;
            }
        }



        void _webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Done downloading a feed, now open it. I can just call FeedSeleted, and now the
            // required feed will be in the cached folder!
            FeedSelected();
            //StartCachingTheNextFeed(); Next version!
        }

        /// <summary>
        /// Iterate through the feeds and display them. 
        /// </summary>
        private void DisplayFeeds()
        {
            this.lstFeeds.Items.Clear();
            foreach (System.Xml.XmlNode outline in this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline"))
            {
                // It's tempting to be clever and add the outline elements directly to the list 
                // control, then implement my own DrawItem to render the content. But that's 
                // more complicated, so I won't. Oh, I can just override .ToString()!
                lstFeeds.Items.Add(outline.Attributes.GetNamedItem("text").Value.ToString());
            }
        }

        private void lstFeeds_Click(object sender, EventArgs e)
        {
            FeedSelected();
        }

        /// <summary>
        /// A feed has been selected from the lstFeeds list. Show the news items.
        /// </summary>
        private void FeedSelected()
        {
            if (lstFeeds.SelectedIndex < 0 || lstFeeds.Items.Count == 0)
            {
                return;
            }
            
            lblStatus.Text = _I18N.GetText("Getting news...");
            lstItems.Items.Clear();
            
            //// Now get the new feed XML file. We need to do this with a System.Xml.XmlTextReader to handle 
            //// broken feeds.
            //System.Xml.XmlNode outlineNode = this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[lstFeeds.SelectedIndex]; 
            //this._CurrentFeed = new RSSFeed(outlineNode.Attributes.GetNamedItem("xmlUrl").Value);
            //System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.DoEvents();
            //System.Windows.Forms.Application.DoEvents();

            //// Later, we're going to populate the htmlUrl attribute with the latest information. Make sure it's there.
            //if (outlineNode.Attributes.GetNamedItem("htmlUrl") == null)
            //{
            //    outlineNode.Attributes.SetNamedItem(this._Library.MyFeedsOPMLDocument.CreateAttribute("htmlUrl"));
            //}

            // Uncomment this to save the new feed XML file to the desktop - handy for debugging.
            //System.Xml.XmlDocument docSave = new System.Xml.XmlDocument();
            //docSave.Load(this._CurrentFeed.url);
            //int feedIndex = 0;
            //while (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Feed" + feedIndex + ".xml"))
            //{
            //    feedIndex++;
            //}
            //docSave.SaveState(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Feed" + feedIndex + ".xml");

            this._Library.SelectFeed(lstFeeds.SelectedIndex);
        }

       
        private void DisplayItems()
        {
            this.lstItems.Items.Clear();
            System.Collections.Generic.List<RSSItemClass> items;
            if (Properties.Settings.Default.ViewDeletedItems)
            {
                items = this._Library.currentFeed.Items;
            }
            else
            {
                items = this._Library.currentFeed.UnreadItems;
            }
            // Now add each item to the list 
            foreach (RSSItemClass item in items)
            {
                lstItems.Items.Add(item);
            }
            if (lstItems.Items.Count == 0)
            {
                lstItems.Items.Add(_I18N.GetText("No news items"));
                lblStatus.Text = _I18N.GetText("No news items");
            }
            else
            {
                lblStatus.Text = lstItems.Items.Count + " " + _I18N.GetText("items") + " - " + lstFeeds.Text;
            }
            lstItems.SelectedIndex = 0;
            lstItems.Focus();
        }

        private void lstItems_Click(object sender, EventArgs e)
        {
            OpenItem();
        }

        /// <summary>
        /// Open the item indicated by lstItems.SelectedIndex
        /// </summary>
        private void OpenItem()
        {
            if (lstItems.SelectedIndex < 0)
                return;

            if (this.lstItems.Items[0].GetType().ToString() == "System.String")
            {
                // We have the "No news items!" message. Get out.
                return;
            }

            RSSItemClass rssItem = (RSSItemClass)this.lstItems.Items[lstItems.SelectedIndex];
            // We could display the contents from the RSS feed, but lots of sites don't have good contents. 
            // So leave it at linking for now, like the previous version.
            //if (rssItem.contents.Length > 0)
            //{
            //    frmContent content = new frmContent();
            //    content.ParseContent(rssItem.contents);
            //    content.ShowDialog(this);
            //}
            //else
            //{
            string url = rssItem.EnclosureUrl;
            if (url == "")
            {
                //TODO Add "Nothing to Play!" to I18N.
                lblStatus.Text = _I18N.GetText("Nothing to play!");
            }
            else
            {
                this.wmp.URL = url;
                this.wmp.Ctlcontrols.play();
            }
        }

        private void mnuFileImport_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.DefaultExt = "opml";
            ofd.Filter = "OPML files|*.opml";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImportOPML(ofd.FileName);
                DisplayFeeds();
            }
        }

        /// <summary>
        /// Imports an OPML file, adding any feeds not already in the list.
        /// </summary>
        /// <param name="filename"></param>
        private void ImportOPML(string filename)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            System.Xml.XmlDocument newFeedDoc = new System.Xml.XmlDocument();
            try
            {
                newFeedDoc.Load(filename);
            }
            catch (System.Xml.XmlException xmlEx)
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
                MessageBox.Show(this, xmlEx.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            catch (System.IO.FileNotFoundException fnfEx)
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
                MessageBox.Show(this, fnfEx.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int added = 0;
            foreach (System.Xml.XmlNode outlineNode in newFeedDoc.DocumentElement.SelectNodes("//outline"))
            {
                if (this._Library.AddFeed(outlineNode))
                {
                    added++;
                }
            }
            this.lblStatus.Text = _I18N.GetText("Imported") + " " + added + " " + _I18N.GetText("new feeds.");
            DisplayFeeds();
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }


        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // SaveState the feeds we're using.
            this._Library.SaveState();
            // SaveState the current feed
            Properties.Settings.Default.SelectedFeedIndex = lstFeeds.SelectedIndex;
            // SaveState the window state
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.WindowState = FormWindowState.Normal;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.WindowState = FormWindowState.Maximized;
            }
            // SaveState our settings
            Properties.Settings.Default.Save();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeedSelected();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstFeeds.SelectedIndex < lstFeeds.Items.Count - 1)
            {
                lstFeeds.SelectedIndex++;
                FeedSelected();
            }
            else
            {
                Microsoft.VisualBasic.Interaction.Beep();
            }
        }

        private void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstFeeds.SelectedIndex > 0)
            {
                lstFeeds.SelectedIndex--;
                FeedSelected();
            }
            else
            {
                Microsoft.VisualBasic.Interaction.Beep();
            }
        }


        /// <summary>
        /// Delete the currently-selected feed, if there is one.
        /// </summary>
        private void DeleteSelectedFeed()
        {
            if (lstFeeds.SelectedIndex != -1)
            {
                this._Library.RemoveFeed(lstFeeds.SelectedIndex);
                if (this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline").Count == 0)
                {
                    lstFeeds.Items.Clear();
                }
                else
                {
                    if (lstFeeds.SelectedIndex == lstFeeds.Items.Count - 1)
                    {
                        lstFeeds.SelectedIndex = lstFeeds.Items.Count - 2;
                    }
                    int selected = lstFeeds.SelectedIndex;
                    DisplayFeeds();
                    lstFeeds.SelectedIndex = selected;
                    FeedSelected();
                }
            }
            else
            {
                Microsoft.VisualBasic.Interaction.Beep();
            }
        }

        private void deleteFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedFeed();
        }

        private void lstFeeds_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.deleteFeedToolStripMenuItem.Enabled = (lstFeeds.SelectedIndex != -1);
            this.renameFeedToolStripMenuItem.Enabled = this.deletedItemsToolStripMenuItem.Enabled;
        }

        private void addFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newUrl = Microsoft.VisualBasic.Interaction.InputBox(_I18N.GetText("Enter URL:"), Application.ProductName);
            if (newUrl.Length == 0)
                return;
            this.staMain.Text = "";
            if (!newUrl.ToLowerInvariant().StartsWith("http"))
            {
                newUrl = "http://" + newUrl;
            }
            //The MSDN documentation says it's best to create an XmlTextReader through .Create
            //rather than = new XmlTextReader. However, if you do it through new, you get an
            //XmlTextReader that doesn't choke on malformed XML, and if you do it through
            //.Create you choke on malformed XML. So clearly new is better!
            System.Xml.XmlTextReader xtr;
            try
            {
                xtr = new System.Xml.XmlTextReader(newUrl);
            }
            catch (System.UriFormatException ufe)
            {
                // Not a valid URL. Skip trying to analyse.
                MessageBox.Show(_I18N.GetText("Invalid URL:") + " " + ufe.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception ex)
            {
                // Unknown error - File Not Found, possibly - display and quit.
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string newFeedUrl = "";
            string rootNodeName = "";
            bool finishedSearching = false;
            bool readOK = true;
            while (!xtr.EOF && !finishedSearching && readOK)
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
                        System.Diagnostics.Debug.Print("Node:" + elementName);
                        if (rootNodeName.Length == 0)
                        {
                            rootNodeName = elementName;
                        }
                        if (rootNodeName == "html")
                        {
                            // This is an HTML file, scan it for RSS feeds.
                            if (elementName == "link")
                            {
                                // Got a link: is it valid?
                                if (xtr.HasAttributes)
                                {
                                    string href = xtr.GetAttribute("href");
                                    string rel = xtr.GetAttribute("rel");
                                    if (rel.ToLowerInvariant() == "alternate" && href != "")
                                    {
                                        // Got it!
                                        newFeedUrl = href;
                                        finishedSearching = true;
                                    }
                                }
                            }
                        }
                        else if (rootNodeName == "feed" || rootNodeName == "rss")
                        {
                            // It's an RSS/Atom feed, go ahead and add it 
                            // without further checking, and stop reading
                            // this document.
                            newFeedUrl = newUrl;
                            finishedSearching = true;
                        }
                    }
                }
                catch
                {
                    //Parsing error from the web page or XML = very common, carry on going
                    //until we find something useful.
                }
                System.Windows.Forms.Application.DoEvents();
            }
            xtr.Close();

            // OK, so we've examined the url the user gave us. Found anything?
            if (newFeedUrl == "")
            {
                // Failed to find anything!
                MessageBox.Show(_I18N.GetText("No RSS news feeds found"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // OK, try getting it. 
                System.Xml.XmlTextReader newFeed = new System.Xml.XmlTextReader(newFeedUrl);
                string newTitle = "";
                string newWebsiteUrl = "";
                bool foundTitleAndUrl = false;
                bool nextNodeIsTitle = false;
                bool nextNodeIsWebsiteUrl = false;
                readOK = true;
                while (!newFeed.EOF && !foundTitleAndUrl && readOK)
                {
                    try
                    {
                        readOK = newFeed.Read();
                        if (newFeed.NodeType == System.Xml.XmlNodeType.Element)
                        {
                            System.Diagnostics.Debug.Print("Node:" + newFeed.Name.ToLowerInvariant());
                            if (newFeed.Name.ToLowerInvariant() == "title")
                            {
                                if (newTitle == "")
                                {
                                    nextNodeIsTitle = true;
                                }
                            }
                            else if (newFeed.Name.ToLowerInvariant() == "link")
                            {
                                if (newWebsiteUrl == "")
                                {
                                    nextNodeIsWebsiteUrl = true;
                                }
                            }
                        }
                        else if (newFeed.NodeType == System.Xml.XmlNodeType.Text)
                        {
                            if (nextNodeIsTitle)
                            {
                                newTitle = newFeed.Value;
                                nextNodeIsTitle = false;
                            }
                            if (nextNodeIsWebsiteUrl)
                            {
                                newWebsiteUrl = newFeed.Value;
                                nextNodeIsWebsiteUrl = false;
                            }
                        }
                    }
                    catch
                    {
                        // Error in XML, very common, carry on.
                    }
                    if (newTitle.Length > 0 && newWebsiteUrl.Length > 0)
                    {
                        foundTitleAndUrl = true;
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                newFeed.Close();
                // OK, add to lists!
                if (foundTitleAndUrl)
                {
                    // Assume this is good.
                    this._Library.AddFeed(newFeedUrl, newTitle, newWebsiteUrl);
                    DisplayFeeds();
                    lblStatus.Text = _I18N.GetText("Added:") + " " + newTitle;
                    // Set focus to the new feed. 
                    try
                    { 
                        int i = 0;
                        foreach (System.Xml.XmlNode outline in this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline"))
                        {
                            if (outline.Attributes.GetNamedItem("xmlUrl").InnerText == newFeedUrl)
                            {
                                lstFeeds.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                    }
                    catch
                    {
                        // Fine, didn't set focus, carry on.
                    }

                    try
                    {
                        // Submission of RSS feed to database.
                        newTitle = System.Net.WebUtility.HtmlEncode(newTitle);
                        newUrl = System.Net.WebUtility.HtmlEncode(newUrl);
                        System.Net.HttpWebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://data.webbie.org.uk/newRSSFeed.php?title=" + newTitle + "&url=" + newFeedUrl + "&language=" + this._I18N.GetLanguage());
                        wr.KeepAlive = false;
                        wr.Method = System.Net.WebRequestMethods.Http.Get;
                        wr.ContentType = "text/html";
                        wr.AllowAutoRedirect = true;
                        wr.GetResponse();
                    }
                    catch
                    {
                        // Failed to connect and write feed information: record why to error log.
                        System.Diagnostics.EventLog.WriteEntry(Application.ProductName + " " + Application.ProductVersion, "Failed to submit new feed registration to WebbIE (" + newUrl + ")");
                    }
                }
                else
                {
                    // Not found a feed.
                    staMain.Text = this._I18N.GetText("0 feeds found.");
                }
            }
        }

        private void mnuFileExport_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.CheckPathExists = true;
            sfd.AddExtension = true;
            sfd.DefaultExt = "opml";
            sfd.Filter = "OPML Files|*.opml";
            sfd.FileName = "RSS News Feeds.opml";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this._Library.ExportFeedOPMLDocument(sfd.FileName);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.EventLog.WriteEntry(Application.ProductName + " " + Application.ProductVersion, "Failed to export OPML file: " + ex.Message);
                }
            }
        }

        private void openFeedWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Xml.XmlNode currentFeed = this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[lstFeeds.SelectedIndex];
            if (currentFeed.Attributes.GetNamedItem("htmlUrl") == null)
            {
                // Missing http value. We'll populate it from the feed next time we load it (so in theory we should
                // never get here!)
                Microsoft.VisualBasic.Interaction.Beep();
            }
            else
            {
                System.Diagnostics.Process.Start(currentFeed.Attributes.GetNamedItem("htmlUrl").InnerText);
            }
        }

        private void lstItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lstItems.SelectedIndex > -1 && lstItems.Text != _I18N.GetText("No news items"))
            {
                MarkCurrentItemAsRead();
            }
        }

        /// <summary>
        /// Mark the currently-selected item as read - that is, remove it from
        /// display.
        /// </summary>
        private void MarkCurrentItemAsRead()
        {
            if (this.lstItems.SelectedIndex == -1)
                return;

            MarkItemAsRead(this.lstItems.SelectedIndex);
            UpdateItemCount(_Library.currentFeed.Url, _Library.currentFeed.UnreadItems.Count);
            int index = lstItems.SelectedIndex;
            DisplayItems();
            if (index < lstItems.Items.Count && lstItems.Items.Count > 0)
            {
                lstItems.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Set the item indicated by index (to the list of items) to "read"
        /// </summary>
        /// <param name="index"></param>
        private void MarkItemAsRead(int index)
        {
            RSSItemClass rssItem = (RSSItemClass)lstItems.Items[index];
            this._Library.currentFeed.MarkAsRead(rssItem);
        }
        
        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstFeeds_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FeedSelected();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedFeed();
            }
        }

        private void deletedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ViewDeletedItems = !Properties.Settings.Default.ViewDeletedItems;
            this.deletedItemsToolStripMenuItem.Checked = Properties.Settings.Default.ViewDeletedItems;
            FeedSelected();
        }

        private void mnuWebsiteDeleteallwebsitefeeds_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_I18N.GetText("Do you really want to delete every RSS news feed?"), Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Remove every feed.
                this._Library.RemoveAllFeeds();
                this.lstFeeds.Items.Clear();
                this.lstItems.Items.Clear();
            }
        }

        private void mnuHelpManual_Click(object sender, EventArgs e)
        {
            _I18N.ShowHelp();
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, Application.ProductName + "\t" + Application.ProductVersion, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lstItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                OpenItem();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                // Go to feeds list.
                lstFeeds.Focus();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkCurrentItemAsRead();
        }

        private void renameFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameSelectedFeed();
        }

        private void RenameSelectedFeed()
        {
            if (lstFeeds.SelectedIndex < 0)
                return;

            string newName = Microsoft.VisualBasic.Interaction.InputBox("Enter a new name for the feed:", Application.ProductName, lstFeeds.Text);
            if (newName.Length > 0)
            {
                System.Xml.XmlNode outlineNode = this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[lstFeeds.SelectedIndex];
                outlineNode.Attributes.GetNamedItem("text").Value = newName;
                outlineNode.Attributes.GetNamedItem("title").Value = newName;
                string url = outlineNode.Attributes.GetNamedItem("xmlUrl").Value;
                this._Library.SortFeeds();
                DisplayFeeds();

                // Set focus back to this newly-renamed feed
                System.Xml.XmlNodeList feedList = this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline");
                for (int i = 0; i < feedList.Count; i++)
                {
                    if (feedList.Item(i).Attributes.GetNamedItem("xmlUrl").Value == url)
                    {
                        // This is the one!
                        lstFeeds.SelectedIndex = i;
                        break;
                    }

                }
            }
            
        }

        private void lstFeeds_DoubleClick(object sender, EventArgs e)
        {
            FeedSelected(); 
        }

        private void downloadFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstFeeds.SelectedIndex != -1)
            {
                System.Xml.XmlNode outlineNode = this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[lstFeeds.SelectedIndex];
                string url = outlineNode.Attributes.GetNamedItem("xmlUrl").Value;
            
                System.Net.WebClient wc = new System.Net.WebClient();
                int index = 1;
                while (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\feed" + index + ".xml"))
                {
                    index++;
                }
                wc.DownloadFile(url, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\feed" + index + ".xml");
            }
        }

        private void deleteallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstItems.Text == _I18N.GetText("No news items"))
                return;

            for (int i = 0; i < lstItems.Items.Count; i++)
            {
                MarkItemAsRead(i);
            }
            DisplayItems();
            lstItems.SelectedIndex = 0;
        }

        private void tmrCheckForNewItems_Tick(object sender, EventArgs e)
        {
            //if (this._LastChecked + new TimeSpan(0,5,0) < DateTime.Now)
            //{
            //    // Need to check now!
            //    this._LastChecked = DateTime.Now;
            //    this._Library.StartUpdatingFeedCacheInTheBackground();
            //}
        }

        private void openFeedURlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstFeeds.SelectedIndex == 0)
            {
                return;
            }
            System.Xml.XmlNode outlineNode = this._Library.MyFeedsOPMLDocument.DocumentElement.SelectNodes("body/outline")[this.lstFeeds.SelectedIndex];
            string xmlUrl = outlineNode.Attributes.GetNamedItem("xmlUrl").Value;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = proc.StartInfo;
            startInfo.UseShellExecute = true;
            startInfo.FileName = "iexplore";
            startInfo.Arguments = xmlUrl;
            proc.Start();
        }

        private void deleteAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MarkAllItemsAsRead();
        }

        private void MarkAllItemsAsRead()
        {
            this._Library.currentFeed.MarkAllAsRead();
            DisplayItems();
            UpdateItemCount(this._Library.currentFeed.Url, 0);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DoBack();
        }

        private void DoBack()
        {
            wmp.Ctlcontrols.currentPosition = 0;
        }


        private void DoForwards()
        {
            if (wmp.Ctlcontrols.currentPosition < wmp.Ctlcontrols.currentItem.duration - 30)
            {
                wmp.Ctlcontrols.currentPosition = wmp.Ctlcontrols.currentPosition + 30;
            }
            else
            {
                Microsoft.VisualBasic.Interaction.Beep();
            }
        }
        private void BtnForward_Click(object sender, EventArgs e)
        {
            DoForwards();
        }

        private void Wmp_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch ((WMPLib.WMPPlayState)e.newState)
            {
                case WMPLib.WMPPlayState.wmppsBuffering:
                case WMPLib.WMPPlayState.wmppsReconnecting:
                case WMPLib.WMPPlayState.wmppsTransitioning:
                case WMPLib.WMPPlayState.wmppsWaiting:
                    btnBack.Enabled = false;
                    btnRewind.Enabled = false;
                    btnPlay.Enabled = false;
                    btnPause.Enabled = false;
                    btnStop.Enabled = true;
                    btnForward.Enabled = false;
                    break;
                case WMPLib.WMPPlayState.wmppsMediaEnded:
                    btnBack.Enabled = false;
                    btnRewind.Enabled = false;
                    btnPlay.Enabled = true;
                    btnPause.Enabled = false;
                    btnStop.Enabled = false;
                    btnForward.Enabled = false;
                    break;
                case WMPLib.WMPPlayState.wmppsPaused:
                    btnBack.Enabled = true;
                    btnRewind.Enabled = true;
                    btnPlay.Enabled = true;
                    btnPause.Enabled = false;
                    btnStop.Enabled = true;
                    btnForward.Enabled = true;
                    break;
                case WMPLib.WMPPlayState.wmppsPlaying:
                    btnBack.Enabled = true;
                    btnRewind.Enabled = true;
                    btnPlay.Enabled = false;
                    btnPause.Enabled = true;
                    btnStop.Enabled = true;
                    btnForward.Enabled = true;
                    break;
                case WMPLib.WMPPlayState.wmppsReady:
                    btnBack.Enabled = false;
                    btnRewind.Enabled = false;
                    btnPlay.Enabled = true;
                    btnPause.Enabled = false;
                    btnStop.Enabled = false;
                    btnForward.Enabled = false;
                    break;
                case WMPLib.WMPPlayState.wmppsStopped:
                    btnBack.Enabled = true;
                    btnRewind.Enabled = true;
                    btnPlay.Enabled = true;
                    btnPause.Enabled = false;
                    btnStop.Enabled = false;
                    btnForward.Enabled = true;
                    break;
                case WMPLib.WMPPlayState.wmppsLast:
                case WMPLib.WMPPlayState.wmppsScanForward:
                case WMPLib.WMPPlayState.wmppsScanReverse:
                case WMPLib.WMPPlayState.wmppsUndefined:
                default:
                    break;
            }
            mnuAudioPlay.Enabled = btnPlay.Enabled;
            mnuAudioStop.Enabled = btnStop.Enabled;
            mnuAudioForwards.Enabled = btnForward.Enabled;
            mnuAudioRewind.Enabled = btnRewind.Enabled;
            mnuAudioPause.Enabled = btnPause.Enabled;
            mnuAudioBack.Enabled = btnBack.Enabled;

        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            DoPlay();
        }

        private void DoPlay()
        {
            wmp.Ctlcontrols.play();
        }

        private void DoRewind()
        {
            if (wmp.Ctlcontrols.currentPosition > 30)
            {
                wmp.Ctlcontrols.currentPosition = wmp.Ctlcontrols.currentPosition - 30;
            }
            else
            {
                wmp.Ctlcontrols.currentPosition = 0;
            }
        }

        private void BtnRewind_Click(object sender, EventArgs e)
        {
            DoRewind();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            DoStop();
        }

        private void DoStop()
        {
            wmp.Ctlcontrols.stop();
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            DoPause();
        }

        private void DoPause()
        {
            wmp.Ctlcontrols.pause();

        }

        private void LstItems_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void MnuAudioPlay_Click(object sender, EventArgs e)
        {
            DoPlay();
        }

        private void MnuAudioStop_Click(object sender, EventArgs e)
        {
            DoStop();
        }

        private void MnuAudioPause_Click(object sender, EventArgs e)
        {
            DoPause();
        }

        private void MnuAudioForwards_Click(object sender, EventArgs e)
        {
            DoForwards();
        }

        private void MnuAudioRewind_Click(object sender, EventArgs e)
        {
            DoRewind();
        }

        private void MnuAudioBack_Click(object sender, EventArgs e)
        {
            DoBack();
        }

        private void MnuAudioIncreasevolume_Click(object sender, EventArgs e)
        {
            wmp.settings.volume = wmp.settings.volume + 10;
        }

        private void MnuAudioDecreasevolume_Click(object sender, EventArgs e)
        {
            // Don't allow the user to turn the volume off entirely, because they will do this accidentally and
            // not realise it. 
            if (wmp.settings.volume > 30)
            {
                wmp.settings.volume = wmp.settings.volume - 10;
            }
        }
    }

}
