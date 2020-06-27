using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSSCommon
{
    /// <summary>
    /// Stores (to disk) the read and unread RSS items in your collection of feeds.
    /// </summary>
    public class WhichItemsHaveBeenReadDatabase
    {
        System.Collections.ArrayList _read;
        readonly string _AppPath;

        /// <summary>
        /// Load the database from disk or create a new one.
        /// </summary>
        /// <param name="userDataPath">The folder where user settings are stored.</param>
        public WhichItemsHaveBeenReadDatabase(string userDataPath)
        {
            _AppPath = userDataPath;
            _read = new System.Collections.ArrayList();
            if (System.IO.File.Exists(_AppPath + "\\read.txt"))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(_AppPath + "\\read.txt", Encoding.UTF8);
                while (!sr.EndOfStream)
                {
                    string readEntry = sr.ReadLine().Trim();
                    if (readEntry.Length > 0)
                    {
                        _read.Add(readEntry);
                    }
                }
                sr.Close();
            }
        }


        /// <summary>
        /// Call this before terminating the class to save the database to disk.
        /// </summary>
        public void Save()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(_AppPath + "\\read.txt", false, Encoding.UTF8);
            for (int i = 0; i < _read.Count; i++)
            {
                sw.WriteLine(_read[i]);
            }
            sw.Close();
        }

        /// <summary>
        /// Tells you if a particular RSSItem has been read.
        /// </summary>
        /// <param name="itemNode">The RSSItem to check.</param>
        /// <returns>True if marked as read, otherwise false.</returns>
        public bool IsRead(RSSItemClass itemNode)
        {
            //System.Diagnostics.Debug.Print("CHECKING IF READ:" + itemNode.Url);
            string id = itemNode.FeedUrl + "*" + itemNode.Url;
            for (int i = 0; i < _read.Count; i++)
            {
                if ((string)this._read[i] == id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Forget every read item, make all "not read".
        /// </summary>
        public void ClearAllItemReadStatuses()
        {
            _read = new System.Collections.ArrayList();
        }

        /// <summary>
        /// Call this when you are deleting a feed from your collection: it will
        /// remove the read/unread information for the feed. 
        /// </summary>
        /// <param name="feed"></param>
        public void RemoveFeed(string url)
        {
            for (int i = _read.Count - 1; i >= 0; i--)
            {
                if (_read[i].ToString().StartsWith(url))
                {
                    _read.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Call this before you clear the list or exit the program to save read.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="feedUrl"></param>
        public void WriteItems(RSSFeedClass feed)
        {
            if (feed == null)
                return;
            else if (feed.Items.Count == 0)
                return;
            System.Collections.ArrayList itemIds = new System.Collections.ArrayList();
            foreach (RSSItemClass item in feed.Items)
            {
                string id = feed.Url + "*" + item.Url;
                itemIds.Add(id);
                if (item.IsRead)
                {
                    // Add to _read array.
                    bool found = false;
                    for (int i = 0; i < _read.Count; i++)
                    {
                        if ((string)_read[i] == id)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        _read.Add(id);
                        System.Diagnostics.Debug.Print("READ:" + id);
                    }
                }
            }
            // Now clean up the _read array, removing items that are no longer items
            // in the feed.
            for (int i = _read.Count - 1; i > -1; i--)
            {
                string savedId = _read[i].ToString();
                if (savedId.StartsWith(feed.Url))
                {
                    // Item from this feed. Is it still an item?
                    bool stillItem = false;
                    for (int j = 0; j < itemIds.Count; j++)
                    {
                        System.Diagnostics.Debug.Print("CHECK:" + (string)itemIds[j]);
                        if (savedId == (string)itemIds[j])
                        {
                            // Yes.
                            stillItem = true;
                            break;
                        }
                    }
                    if (!stillItem)
                    {
                        System.Diagnostics.Debug.Print("DELETING:" + savedId);
                        _read.RemoveAt(i);
                    }
                }
            }
        }
    }
}
