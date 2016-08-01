using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using CygSoft.CodeCat.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.Base
{
    internal abstract class BaseLibrary
    {
        private const string CODE_LIBRARY_INDEX_FILE = "_code.xml";
        private const string CODE_LIBRARY_LAST_OPENED_FILE = "_lastopened.txt";

        private string subFolder = null;

        protected IKeywordSearchIndex index;
        private IKeywordSearchIndexRepository indexRepository;
        protected Dictionary<string, IPersistableTarget> openFiles;

        protected string FileExtension { get; set; }

        public string FilePath { get { return this.index.FilePath; } }
        public string FileTitle { get { return this.index.FileTitle; } }
        public string FolderPath { get { return this.index.FolderPath; } }

        public int IndexCount { get { return this.index.ItemCount; } }
        public int CurrentFileVersion
        {
            get
            {
                if (index != null)
                    return index.CurrentVersion;
                return -1;
            }
        }
        public bool Loaded
        {
            get
            {
                if (this.index == null)
                    return false;
                return true;
            }
        }

        public BaseLibrary(IKeywordSearchIndexRepository indexRepository, string subFolder)
        {
            this.indexRepository = indexRepository;
            this.subFolder = subFolder;
        }

        protected abstract IPersistableTarget CreateSpecializedTarget(IKeywordIndexItem indexItem);
        protected abstract IPersistableTarget OpenSpecializedTarget(IKeywordIndexItem indexItem);

        public IPersistableTarget OpenTarget(IKeywordIndexItem indexItem)
        {
            IPersistableTarget target = OpenSpecializedTarget(indexItem);
            
            target.ContentClosed += target_ContentClosed;
            target.ContentSaved += target_ContentSaved;
            target.ContentDeleted += target_ContentDeleted;
            return target;
        }

        public IPersistableTarget CreateTarget(IKeywordIndexItem indexItem)
        {
            IPersistableTarget target = CreateSpecializedTarget(indexItem);
            
            target.ContentClosed += target_ContentClosed;
            target.ContentSaved += target_ContentSaved;
            target.ContentDeleted += target_ContentDeleted;
            return target;
        }

        public void Open(string parentFolder, int currentVersion)
        {
            BeforeIndexLoad();

            string indexPath = Path.Combine(parentFolder, subFolder, CODE_LIBRARY_INDEX_FILE);
            this.index = indexRepository.OpenIndex(indexPath, currentVersion);
            AfterIndexLoad();
        }

        public void Create(string parentFolder, int currentVersion)
        {
            BeforeIndexLoad();

            Directory.CreateDirectory(Path.Combine(parentFolder, subFolder));
            string indexPath = Path.Combine(parentFolder, subFolder, CODE_LIBRARY_INDEX_FILE);
            this.index = indexRepository.CreateIndex(indexPath, currentVersion);
            AfterIndexLoad();
        }

        public void Save()
        {
            indexRepository.SaveIndex(this.index);
        }

        public void SaveAs(string filePath)
        {
            BeforeIndexLoad();
            this.index = indexRepository.SaveIndexAs(this.index, filePath);
            AfterIndexLoad();
        }

        public void OpenProjectFolder()
        {
            if (!string.IsNullOrEmpty(this.FilePath) && File.Exists(this.FilePath))
            {
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = this.FolderPath;
                prc.Start();
            }
        }

        public IKeywordIndexItem[] FindIndeces(string commaDelimitedKeywords)
        {
            return this.index.Find(commaDelimitedKeywords);
        }

        public IKeywordIndexItem[] FindIndecesByIds(string[] ids)
        {
            List<IKeywordIndexItem> indeces = new List<IKeywordIndexItem>();
            foreach (string id in ids)
            {
                IKeywordIndexItem indexItem = this.index.FindById(id);
                if (indexItem != null)
                {
                    indeces.Add(indexItem);
                }
            }
            return indeces.ToArray();
        }

        public string[] AllKeywords(IKeywordIndexItem[] indeces)
        {
            return this.index.AllKeywords(indeces);
        }

        public string CopyAllKeywords(IKeywordIndexItem[] indeces)
        {
            return this.index.CopyAllKeywords(indeces);
        }

        public void AddKeywords(IKeywordIndexItem[] indeces, string delimitedKeywordList)
        {
            this.index.AddKeywords(indeces, delimitedKeywordList);
        }

        public bool RemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords, out IKeywordIndexItem[] invalidIndeces)
        {
            if (this.index.ValidateRemoveKeywords(indeces, keywords, out invalidIndeces))
            {
                this.index.RemoveKeywords(indeces, keywords);
                return true;
            }
            return false;
        }

        public IKeywordIndexItem[] FindMissingFiles()
        {
            List<IKeywordIndexItem> missingItems = new List<IKeywordIndexItem>();
            IKeywordIndexItem[] IndexItems = this.index.All();
            foreach (IKeywordIndexItem IndexItem in IndexItems)
            {
                string fullPath = Path.Combine(this.FolderPath, IndexItem.FileTitle);
                if (!File.Exists(fullPath))
                {
                    missingItems.Add(IndexItem);
                }
            }
            return missingItems.ToArray();
        }

        public string[] FindOrphanedFiles()
        {
            List<string> orphanedFiles = new List<string>();
            List<string> files = Directory.EnumerateFiles(this.FolderPath, this.FileExtension).ToList();
            foreach (string file in files)
            {
                if (!this.index.Contains(Path.GetFileNameWithoutExtension(file)))
                {
                    if (file != this.FilePath)
                        orphanedFiles.Add(file);
                }
            }
            return orphanedFiles.ToArray();
        }

        public string LastOpenedFilePath
        {
            get { return Path.Combine(this.FolderPath, CODE_LIBRARY_LAST_OPENED_FILE); }
        }

        public IKeywordIndexItem[] GetLastOpenedIds()
        {
            LastCodeFileRepository lastCodeFileRepo = new LastCodeFileRepository(this.LastOpenedFilePath);
            return this.FindIndecesByIds(lastCodeFileRepo.Load());
        }

        public void SetLastOpenedIds(string[] ids)
        {
            IKeywordIndexItem[] foundItems = this.FindIndecesByIds(ids);
            string[] foundIds = foundItems.Select(k => k.Id).ToArray();

            LastCodeFileRepository lastCodeFileRepo = new LastCodeFileRepository(this.LastOpenedFilePath);
            lastCodeFileRepo.Save(ids);
        }

        private void RemoveLibraryFileReference(string id, bool save = false)
        {
            if (this.openFiles == null)
                return;

            if (this.openFiles.ContainsKey(id))
            {
                if (save)
                {
                    this.openFiles[id].Save();
                }
                this.openFiles.Remove(id);
            }
        }

        private IPersistableTarget GetLibraryReferenceOrOpenFile(string id)
        {
            IPersistableTarget persistableFile;

            if (this.openFiles != null && this.openFiles.ContainsKey(id))
            {
                persistableFile = this.openFiles[id];
            }
            else
            {
                IKeywordIndexItem indexItem = this.index.FindById(id);
                persistableFile = OpenTarget(indexItem);
            }

            return persistableFile;
        }

        private void BeforeIndexLoad()
        {
            // make sure you detach any existing events
            if (this.index != null)
                this.index.IndexModified -= Index_IndexModified;

            if (openFiles == null)
                return;

            while (openFiles.Count > 0)
            {
                KeyValuePair<string, IPersistableTarget> persistableFile = openFiles.ElementAt(0);
                RemoveLibraryFileReference(persistableFile.Key);
            }
        }

        private void AfterIndexLoad()
        {
            if (this.index != null)
                this.index.IndexModified += Index_IndexModified;
        }

        private void Index_IndexModified(object sender, EventArgs e)
        {
            indexRepository.SaveIndex(this.index);
        }

        private void target_ContentDeleted(object sender, EventArgs e)
        {
            IPersistableTarget target = sender as IPersistableTarget;

            target.ContentClosed -= target_ContentClosed;
            target.ContentSaved -= target_ContentSaved;
            target.ContentDeleted -= target_ContentDeleted;

            this.RemoveLibraryFileReference(target.Id);
            this.index.Remove(target.Id);
        }

        private void target_ContentSaved(object sender, EventArgs e)
        {
            IKeywordTarget targetFile = sender as IKeywordTarget;
            this.index.Update(targetFile.IndexItem);
        }

        private void target_ContentClosed(object sender, EventArgs e)
        {
            IPersistableTarget target = sender as IPersistableTarget;

            target.ContentClosed -= target_ContentClosed;
            target.ContentSaved -= target_ContentSaved;
            target.ContentDeleted -= target_ContentDeleted;

            this.RemoveLibraryFileReference(target.Id);
        }
    }
}
