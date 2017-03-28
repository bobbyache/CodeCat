using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public Version CurrentFileVersion
        {
            get
            {
                if (index != null)
                    return index.CurrentVersion;
                return null;
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
        public abstract IndexExportImportData[] GetExportData(IKeywordIndexItem[] indexItems);

        public void Import(IndexExportImportData[] importData)
        {
            this.indexRepository.ImportItems(this.FilePath, this.CurrentFileVersion, importData.Select(idx => idx.KeywordIndexItem).ToArray());

            foreach (IndexExportImportData import in importData)
            {
                if (import.IsFile)
                {
                    File.Copy(import.Path, Path.Combine(this.FolderPath, import.FileOrFolderName), false);
                }
                else
                {
                    FileSys.DirectoryCopy(import.Path, Path.Combine(this.FolderPath, import.FileOrFolderName), true);
                }
            }
        }

        public IPersistableTarget OpenTarget(IKeywordIndexItem indexItem)
        {
            IPersistableTarget target = OpenSpecializedTarget(indexItem);

            target.BeforeClose += target_BeforeClose;
            target.AfterSave += target_AfterSave;
            target.AfterDelete += target_AfterDelete;
            return target;
        }

        public IPersistableTarget CreateTarget(IKeywordIndexItem indexItem)
        {
            IPersistableTarget target = CreateSpecializedTarget(indexItem);

            target.BeforeClose += target_BeforeClose;
            target.AfterSave += target_AfterSave;
            target.AfterDelete += target_AfterDelete; ;
            return target;
        }

        public void Open(string parentFolder, Version currentVersion)
        {
            BeforeIndexLoad();

            string indexPath = Path.Combine(parentFolder, subFolder, CODE_LIBRARY_INDEX_FILE);
            this.index = indexRepository.OpenIndex(indexPath, currentVersion);
            AfterIndexLoad();
        }

        public void Create(string parentFolder, Version currentVersion)
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

        public bool CanRemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords, out IKeywordIndexItem[] invalidIndeces)
        {
            if (this.index.ValidateRemoveKeywords(indeces, keywords, out invalidIndeces))
                return true;
            return false;
        }

        public void RemoveKeywords(IKeywordIndexItem[] indeces, string[] keywords)
        {
            IKeywordIndexItem[] invalidIndeces;
            if (!this.index.ValidateRemoveKeywords(indeces, keywords, out invalidIndeces))
            {
                throw new ApplicationException("Removing these keywords would result in unsearchable item(s).");
            }
            this.index.RemoveKeywords(indeces, keywords);
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

        public void SetLastOpenedIds(IKeywordIndexItem[] keywordIndexItems)
        {
            LastCodeFileRepository lastCodeFileRepo = new LastCodeFileRepository(this.LastOpenedFilePath);
            lastCodeFileRepo.Save(FindExistingIds(keywordIndexItems));
        }

        public bool ImportIndecesExist(IndexExportImportData[] importDataList, out IKeywordIndexItem[] existingItems)
        {
            return this.index.IndexesExistFor(importDataList.Select(k => k.KeywordIndexItem).ToArray(), out existingItems);
        }

        private string[] FindExistingIds(IKeywordIndexItem[] indeces)
        {
            IKeywordIndexItem[] foundItems = this.FindIndecesByIds(indeces.Select(r => r.Id).ToArray());
            string[] foundIds = foundItems.Select(k => k.Id).ToArray();

            return foundIds;
        }

        protected IKeywordIndexItem[] FindIndecesByIds(string[] ids)
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

        private void target_AfterDelete(object sender, TopicEventArgs e)
        {
            IPersistableTarget target = sender as IPersistableTarget;

            target.BeforeClose += target_BeforeClose;

            target.AfterSave -= target_AfterSave;
            target.AfterDelete -= target_AfterDelete;

            this.RemoveLibraryFileReference(target.Id);
            this.index.Remove(target.Id);
        }

        private void target_AfterSave(object sender, TopicEventArgs e)
        {
            IPersistableTarget targetFile = sender as IPersistableTarget;
            this.index.Update(targetFile.IndexItem);
        }

        private void target_BeforeClose(object sender, TopicEventArgs e)
        {
            IPersistableTarget target = sender as IPersistableTarget;

            target.BeforeClose -= target_BeforeClose;
            target.AfterSave -= target_AfterSave;
            target.AfterDelete -= target_AfterDelete;

            this.RemoveLibraryFileReference(target.Id);
        }
    }
}
