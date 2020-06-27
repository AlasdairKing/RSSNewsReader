using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSSCommon
{
    /// <summary>
    /// One loaded RSS Feed
    /// </summary>
    public class RSSFeedClass
    {
        string _url;
        System.Collections.Generic.List<RSSItemClass> _items;

        public RSSFeedClass(string url)
        {
            _url = url;
            _items = new System.Collections.Generic.List<RSSItemClass>();
        }

        /// <summary>
        /// Add an RSSItem item to the feed contents. 
        /// </summary>
        /// <param name="item">An item you've parsed out of the feed.</param>
        public void AddItem(RSSItemClass item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// All the RSSItem items in the feed (which you've added using AddItem())
        /// </summary>
        public System.Collections.Generic.List<RSSItemClass> Items
        {
            get
            {
                return this._items;
            }
        }

        /// <summary>
        /// An ArrayList containing RSSItem items that aren't flagged isRead.
        /// </summary>
        public System.Collections.Generic.List<RSSItemClass> UnreadItems
        {
            get
            {
                var al = new System.Collections.Generic.List<RSSItemClass>();
                foreach (RSSItemClass item in this._items)
                {
                    if (!item.IsRead)
                    {
                        al.Add(item);
                    }
                }
                return al;
            }
        }

        /// <summary>
        /// The URL for the feed, e.g. http://www.webbie.org.uk/blog/feed/
        /// </summary>
        public string Url
        {
            get
            {
                return _url;
            }
        }

        /// <summary>
        /// Marks the identified RSSItem as read.
        /// </summary>
        /// <param name="readItem">The RSSItem to mark as read.</param>
        public void MarkAsRead(RSSItemClass readItem)
        {
            foreach (RSSItemClass item in _items)
            {
                if (item.Url == readItem.Url)
                {
                    item.IsRead = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Mark all the RSSItem items in this feed as read.
        /// </summary>
        public void MarkAllAsRead()
        {
            foreach (RSSItemClass item in _items)
            {
                item.IsRead = true;
            }
        }
        
        /// <summary>
        /// When loading an RSS Feed, call this after loading the items
        /// so each one is correctly marked read or not.
        /// </summary>
        /// <param name="readDatabase">The ReadDatabase for the program. You probably only have one.</param>
        public void IdentifyReadItems(WhichItemsHaveBeenReadDatabase readDatabase)
        {
            foreach (RSSItemClass item in _items)
            {
                item.IsRead = readDatabase.IsRead(item);
            }
        }
    }

}
