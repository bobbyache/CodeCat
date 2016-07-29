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

namespace CygSoft.CodeCat.Domain
{
    internal abstract class BaseLibrary
    {
        private const string CODE_LIBRARY_ELEMENT = "CodeLibrary";
        private const string CODE_LIBRARY_INDEX_FILE = "_code.xml";
        private const string CODE_LIBRARY_LAST_OPENED_FILE = "_lastopened.txt";

        private string subFolder = null;

        protected IKeywordSearchIndex index;
        private IKeywordSearchIndexRepository indexRepository;
        protected Dictionary<string, IPersistableFile> openFiles;

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

        protected abstract IPersistableFile CreateFile(IKeywordIndexItem indexItem);

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

        public void DeleteFile(string id)
        {
            IPersistableFile persistableFile;

            if (this.openFiles != null && this.openFiles.ContainsKey(id))
            {
                persistableFile = this.openFiles[id];
            }
            else
            {
                IKeywordIndexItem indexItem = this.index.FindById(id);
                persistableFile = CreateFile(indexItem);
            }

            persistableFile.Delete();
            CloseFile(id);
            this.index.Remove(id);
        }

        public void CloseFile(string id, bool save = false)
        {
            if (this.openFiles == null)
                return;

            if (this.openFiles.ContainsKey(id))
            {
                if (save)
                {
                    this.openFiles[id].Save();
                }
                this.openFiles[id].ContentSaved -= File_ContentSaved;
                this.openFiles.Remove(id);
            }
        }

        public IPersistableFile CreateFile()
        {
            return GetFile(new XmlKeywordIndexItem());
        }

        public IPersistableFile OpenFile(IKeywordIndexItem indexItem)
        {
            IPersistableFile persistableFile = GetFile(indexItem);
            return persistableFile;
        }

        protected IPersistableFile GetFile(IKeywordIndexItem indexItem)
        {
            IPersistableFile persistableFile;

            if (this.openFiles == null)
                this.openFiles = new Dictionary<string, IPersistableFile>();

            // first check to see if the file exists..
            if (this.openFiles.ContainsKey(indexItem.Id))
            {
                persistableFile = this.openFiles[indexItem.Id];
            }
            else
            {
                // retrieve the file and add it to the opened code files.
                persistableFile = this.CreateFile(indexItem);

                if (this.openFiles == null)
                    this.openFiles = new Dictionary<string, IPersistableFile>();
                this.openFiles.Add(persistableFile.Id, persistableFile);

                persistableFile.ContentSaved += File_ContentSaved;
            }

            return persistableFile;
        }

        private void File_ContentSaved(object sender, EventArgs e)
        {
            IKeywordTarget targetFile = sender as IKeywordTarget;
            this.index.Update(targetFile.IndexItem);
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
                KeyValuePair<string, IPersistableFile> persistableFile = openFiles.ElementAt(0);
                CloseFile(persistableFile.Key);
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
    }
}
