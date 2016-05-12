using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex
{
    /// <summary>
    /// A lookup of index items that point to code files.
    /// </summary>
    public class KeywordSearchIndex : IKeywordSearchIndex
    {
        /// <summary>
        /// When the code index has been modified in any way, this event will fire which will
        /// enable the application to respond by for instance, saving the index to the index file.
        /// </summary>
        public event EventHandler IndexModified;

        private List<IKeywordIndexItem> IndexItems;
        private KeyPhrases keyPhrases = new KeyPhrases();

        public bool FindAllForEmptySearch { get; set; }

        public int CurrentVersion { get; private set; }

        public string LibraryFolderPath { get; private set; }

        internal KeywordSearchIndex(string filePath, int currentVersion, List<IKeywordIndexItem> IndexItems)
        {
            this.FilePath = filePath;

            this.FindAllForEmptySearch = true;
            this.IndexItems = IndexItems;
            this.CurrentVersion = currentVersion;
            CreateKeywordIndex();
        }

        internal KeywordSearchIndex(string filePath, int currentVersion)
        {
            this.FilePath = filePath;
            this.CurrentVersion = currentVersion;
            this.IndexItems = new List<IKeywordIndexItem>();
        }

        public IKeywordIndexItem[] All()
        {
            return IndexItems.ToArray();
        }

        public string FilePath { get; private set; }
        public string FileTitle { get { return Path.GetFileName(this.FilePath); } }
        public string FolderPath { get { return Path.GetDirectoryName(this.FilePath); } }

        public string[] Keywords { get { return this.keyPhrases.Phrases; } }

        public IKeywordIndexItem FindById(string id)
        {
            if (this.IndexItems.Any(r => r.Id == id))
                return this.IndexItems.Where(r => r.Id == id).SingleOrDefault();
            return null;
        }

        public IKeywordIndexItem[] Find(string commaDelimitedKeywords)
        {
            List<IKeywordIndexItem> foundItemList = new List<IKeywordIndexItem>();

            if (!string.IsNullOrWhiteSpace(commaDelimitedKeywords))
            {

                KeyPhrases keys = new KeyPhrases(commaDelimitedKeywords);

                foreach (IKeywordIndexItem item in IndexItems)
                {
                    if (item.AllKeywordsFound(keys.Phrases))
                        foundItemList.Add(item);
                }
            }
            else
            {
                if (this.FindAllForEmptySearch)
                {
                    foundItemList.AddRange(this.All());
                }
            }
            return foundItemList.ToArray();
        }

        public int ItemCount { get { return this.IndexItems.Count; } }

        public bool Contains(IKeywordIndexItem item)
        {
            return this.IndexItems.Any(r => r.Id == item.Id);
        }

        public bool Contains(string id)
        {
            return this.IndexItems.Any(r => r.Id == id);
        }

        /// <summary>
        /// Fires the IndexModified event and is used to save after something has happened to the index.
        /// Index items may have been modified for instance, their keywords have been changed.
        /// </summary>
        public void Update()
        {
            // At some point you might wish the code index items to raise their own changed events to 
            // kick off this event.
            if (IndexModified != null)
                IndexModified(this, new EventArgs());
        }

        /// <summary>
        /// A single item has been  changed. This will be used when a code index items is added.
        /// </summary>
        /// <param name="item"></param>
        public void Update(IKeywordIndexItem item)
        {
            if (!Contains(item))
                Add(item);

            if (IndexModified != null)
                IndexModified(this, new EventArgs());
        }

        public string[] AllKeywords(IKeywordIndexItem[] indeces)
        {
            KeyPhrases phrases = new KeyPhrases();
            foreach (IKeywordIndexItem index in indeces)
            {
                phrases.AddKeyPhrases(index.Keywords);
            }
            return phrases.Phrases;
        }

        public string CopyAllKeywords(IKeywordIndexItem[] indeces)
        {
            KeyPhrases phrases = new KeyPhrases();
            foreach (IKeywordIndexItem index in indeces)
            {
                phrases.AddKeyPhrases(index.Keywords);
            }
            return phrases.DelimitKeyPhraseList();
        }

        public void AddKeywords(IKeywordIndexItem[] indeces, string delimitedKeywordList)
        {
            foreach (IKeywordIndexItem index in indeces)
            {
                index.AddKeywords(delimitedKeywordList);
            }
            this.Update();
        }

        public void RemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords)
        {
            foreach (IKeywordIndexItem index in indeces)
            {
                index.RemoveKeywords(keywords);
            }
            this.Update();
        }

        private void Add(IKeywordIndexItem item)
        {
            if (Contains(item))
                throw new ApplicationException("This id already exists within the index. Cannot add a duplicate id to the index.");

            this.IndexItems.Add(item);
            CreateKeywordIndex();

        }

        public void Remove(string id)
        {
            IKeywordIndexItem item = this.IndexItems.Where(cd => cd.Id == id).SingleOrDefault();
            this.IndexItems.Remove(item);
            CreateKeywordIndex();

            if (IndexModified != null)
                IndexModified(this, new EventArgs());
        }

        public void IncrementHitCount(string id)
        {
            IKeywordIndexItem item = this.IndexItems.Where(cd => cd.Id == id).SingleOrDefault();
            item.NoOfHits++;

            if (IndexModified != null)
                IndexModified(this, new EventArgs());
        }

        private void CreateKeywordIndex()
        {
            keyPhrases = new KeyPhrases();
            foreach (IKeywordIndexItem IndexItem in IndexItems)
            {
                keyPhrases.AddKeyPhrases(IndexItem.Keywords);
            }
        }
    }
}
