﻿using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Code.Base;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CygSoft.CodeCat.Domain.Management
{
    public class ManageableProject
    {
        private CodeLibrary codeLibrary;
        private QikTemplateLibrary qikLibrary;
        private TopicLibrary codeGroupLibrary;
        private Project project = new Project();

        public bool Loaded
        {
            get { return this.codeLibrary.Loaded && this.qikLibrary.Loaded && this.codeGroupLibrary.Loaded; }
        }

        public string RootFilePath
        {
            get { return this.project.FilePath; }
        }

        public ManageableProject()
        {
            this.codeLibrary = new CodeLibrary();
            this.qikLibrary = new QikTemplateLibrary();
            this.codeGroupLibrary = new TopicLibrary();
        }

        public void Open(string filePath, Version currentVersion)
        {
            project.Open(filePath, currentVersion);
            this.codeLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
            this.qikLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
            this.codeGroupLibrary.Open(Path.GetDirectoryName(filePath), currentVersion);
        }

        public IKeywordIndexItem[] FindIndeces(string commaDelimitedKeywords)
        {
            List<IKeywordIndexItem> keywordIndexItems = new List<IKeywordIndexItem>();

            keywordIndexItems.AddRange(this.codeLibrary.FindIndeces(commaDelimitedKeywords));
            keywordIndexItems.AddRange(this.qikLibrary.FindIndeces(commaDelimitedKeywords));
            keywordIndexItems.AddRange(this.codeGroupLibrary.FindIndeces(commaDelimitedKeywords));

            return keywordIndexItems.ToArray();
        }

        public IndexExportImportData[] GetExportData(IKeywordIndexItem[] indexItems)
        {
            List<IndexExportImportData> indexExportData = new List<IndexExportImportData>();
            indexExportData.AddRange(this.codeLibrary.GetExportData(indexItems));
            indexExportData.AddRange(this.qikLibrary.GetExportData(indexItems));
            indexExportData.AddRange(this.codeGroupLibrary.GetExportData(indexItems));

            return indexExportData.ToArray();
        }

        public bool FindPotentialDuplicates(IndexExportImportData[] exportData, out IKeywordIndexItem[] potentialDuplicates)
        {
            List<IKeywordIndexItem> items = new List<IKeywordIndexItem>();

            IKeywordIndexItem[] codeItems = new IKeywordIndexItem[0];
            IKeywordIndexItem[] qikItems = new IKeywordIndexItem[0];
            IKeywordIndexItem[] codeGroupItems = new IKeywordIndexItem[0];

            bool existA = this.codeGroupLibrary.ImportIndecesExist(exportData, out codeGroupItems);
            bool existB = this.codeLibrary.ImportIndecesExist(exportData, out codeItems);
            bool existC = this.qikLibrary.ImportIndecesExist(exportData, out qikItems);

            if (existA || existB || existC)
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
            IKeywordIndexItem[] codeItems;
            IKeywordIndexItem[] qikItems;
            IKeywordIndexItem[] codeGroupItems;

            // Use this to "move" items from one project to another:
            //                          this.codeGroupLibrary.OpenTarget() <--- Now you can use the target to delete itself.
            // ... will this delete the index? probably not...

            bool anyExist =
                this.codeGroupLibrary.ImportIndecesExist(exportData, out codeGroupItems) &&
                this.codeLibrary.ImportIndecesExist(exportData, out codeItems) &&
                this.qikLibrary.ImportIndecesExist(exportData, out qikItems);

            if (anyExist)
                throw new ApplicationException("they exist!");

            TopicLibrary codeGroupLibrary = new TopicLibrary();
            codeGroupLibrary.Open(Path.GetDirectoryName(destinationIndexFilePath), currentVersion);
            codeGroupLibrary.Import(exportData.Where(ex => ex.KeywordIndexItem is ITopicKeywordIndexItem).ToArray());
            
            CodeLibrary codeLibrary = new CodeLibrary();
            codeLibrary.Open(Path.GetDirectoryName(destinationIndexFilePath), currentVersion);
            codeLibrary.Import(exportData.Where(ex => ex.KeywordIndexItem is ICodeKeywordIndexItem).ToArray());

            QikTemplateLibrary qikLibrary = new QikTemplateLibrary();
            qikLibrary.Open(Path.GetDirectoryName(destinationIndexFilePath), currentVersion);
            qikLibrary.Import(exportData.Where(ex => ex.KeywordIndexItem is IQikTemplateKeywordIndexItem).ToArray());
        }
    }
}
