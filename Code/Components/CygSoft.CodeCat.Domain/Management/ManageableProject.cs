using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Management
{
    public class ManageableProject
    {
        private TopicLibrary codeGroupLibrary;
        private Project project = new Project();

        public bool Loaded
        {
            get { return this.codeGroupLibrary.Loaded; }
        }

        public string RootFilePath
        {
            get { return this.project.FilePath; }
        }

        public ManageableProject()
        {
            this.codeGroupLibrary = new TopicLibrary();
        }

        public void Open(string filePath, Version currentVersion)
        {
            project.Open(filePath, currentVersion);
            this.codeGroupLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
        }

        public IKeywordIndexItem[] FindIndeces(string commaDelimitedKeywords)
        {
            List<IKeywordIndexItem> keywordIndexItems = new List<IKeywordIndexItem>();

            keywordIndexItems.AddRange(this.codeGroupLibrary.FindIndeces(commaDelimitedKeywords));

            return keywordIndexItems.ToArray();
        }

        public IndexExportImportData[] GetExportData(IKeywordIndexItem[] indexItems)
        {
            List<IndexExportImportData> indexExportData = new List<IndexExportImportData>();
            indexExportData.AddRange(this.codeGroupLibrary.GetExportData(indexItems));

            return indexExportData.ToArray();
        }

        public bool FindPotentialDuplicates(IndexExportImportData[] exportData, out IKeywordIndexItem[] potentialDuplicates)
        {
            List<IKeywordIndexItem> items = new List<IKeywordIndexItem>();

            IKeywordIndexItem[] codeItems = new IKeywordIndexItem[0];
            IKeywordIndexItem[] qikItems = new IKeywordIndexItem[0];
            IKeywordIndexItem[] codeGroupItems = new IKeywordIndexItem[0];

            bool exist = this.codeGroupLibrary.ImportIndecesExist(exportData, out codeGroupItems);

            if (exist)
            {
                items.AddRange(codeGroupItems);
                items.AddRange(codeItems);
                items.AddRange(qikItems);
                potentialDuplicates = items.ToArray();

                return true;
            }
            else
            {
                potentialDuplicates = new IKeywordIndexItem[0];
                return false;
            }
        }

        public void ImportData(string sourceIndexFilePath, string destinationIndexFilePath, IndexExportImportData[] exportData, Version currentVersion)
        {
            IKeywordIndexItem[] codeGroupItems;

            // Use this to "move" items from one project to another:
            //                          this.codeGroupLibrary.OpenTarget() <--- Now you can use the target to delete itself.
            // ... will this delete the index? probably not...

            bool anyExist =
                this.codeGroupLibrary.ImportIndecesExist(exportData, out codeGroupItems);

            if (anyExist)
                throw new ApplicationException("they exist!");

            TopicLibrary codeGroupLibrary = new TopicLibrary();
            codeGroupLibrary.Open(Path.GetDirectoryName(destinationIndexFilePath), currentVersion);
            codeGroupLibrary.Import(exportData.Where(ex => ex.KeywordIndexItem is ITopicKeywordIndexItem).ToArray());
        }
    }
}
