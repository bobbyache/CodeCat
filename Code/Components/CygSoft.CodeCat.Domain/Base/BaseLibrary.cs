using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Files.Infrastructure;
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
        protected Dictionary<string, IFile> workItems;

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

        public IFile GetWorkItem(IKeywordIndexItem indexItem)
        {
            IFile workItem = FindOpenWorkItem(indexItem.Id);

            if (workItem == null)
                workItem = CreateWorkItem(indexItem);

            return workItem;
        }

        public IFile CreateWorkItem(IKeywordIndexItem indexItem)
        {
            IFile workItem = WorkItemFactory.Create(indexItem, this.FolderPath);
            OpenWorkItem(workItem);

            return workItem;
        }
        private void OpenWorkItem(IFile workItem)
        {
            if (this.workItems == null)
                this.workItems = new Dictionary<string, IFile>();

            if (workItem.Exists)
                workItem.Open();

            workItem.BeforeClose += target_BeforeClose;
            workItem.AfterSave += workItem_AfterSave;
            workItem.AfterDelete += workItem_AfterDelete;

            this.workItems.Add(((ITitledEntity)workItem).Id, workItem);
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

        public IKeywordIndexItem[] FindIndeces(string commaDelimitedKeywords)
        {
            return this.index.Find(commaDelimitedKeywords);
        }

        public IKeywordIndexItem[] FindByIds(string[] ids)
        {
            return this.index.FindByIds(ids);
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

        private IFile FindOpenWorkItem(string id)
        {
            IFile workItem = null;

            if (this.workItems == null)
                this.workItems = new Dictionary<string, IFile>();

            // first check to see if the file exists..
            if (this.workItems.ContainsKey(id))
            {
                workItem = this.workItems[id];
            }

            return workItem;
        }

        private void RemoveLibraryFileReference(string id, bool save = false)
        {
            if (this.workItems == null)
                return;

            if (this.workItems.ContainsKey(id))
            {
                if (save)
                {
                    this.workItems[id].Save();
                }
                this.workItems.Remove(id);
            }
        }

        private void BeforeIndexLoad()
        {
            // make sure you detach any existing events
            if (this.index != null)
                this.index.IndexModified -= Index_IndexModified;

            if (workItems == null)
                return;

            while (workItems.Count > 0)
            {
                KeyValuePair<string, IFile> workItem = workItems.ElementAt(0);
                RemoveLibraryFileReference(workItem.Key);
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

        private void workItem_AfterDelete(object sender, FileEventArgs e)
        {
            IFile workItem = sender as IFile;

            workItem.BeforeClose -= target_BeforeClose;
            workItem.AfterSave -= workItem_AfterSave;
            workItem.AfterDelete -= workItem_AfterDelete;

            this.RemoveLibraryFileReference(((ITitledEntity)workItem).Id);
            this.index.Remove(((ITitledEntity)workItem).Id);
        }

        private void workItem_AfterSave(object sender, FileEventArgs e)
        {
            IKeywordTarget keywordTarget = sender as IKeywordTarget;
            this.index.Update(keywordTarget.IndexItem);
        }

        private void target_BeforeClose(object sender, FileEventArgs e)
        {
            IFile file = sender as IFile;
            file.BeforeClose -= target_BeforeClose;
            file.AfterSave -= workItem_AfterSave;
            file.AfterDelete -= workItem_AfterDelete;

            this.RemoveLibraryFileReference(((IKeywordTarget)file).IndexItem.Id);
        }
    }
}
