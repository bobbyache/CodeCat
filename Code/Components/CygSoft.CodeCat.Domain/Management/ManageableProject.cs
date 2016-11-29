using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Code.Base;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.Management
{
    public class ManageableProject
    {
        private CodeLibrary codeLibrary;
        private QikLibrary qikLibrary;
        private CodeGroupLibrary codeGroupLibrary;
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
            this.qikLibrary = new QikLibrary();
            this.codeGroupLibrary = new CodeGroupLibrary();
        }

        public void Open(string filePath, int currentVersion)
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

        public void ImportData(string sourceIndexFilePath, IndexExportImportData[] exportData)
        {
            IKeywordIndexItem[] codeItems;
            IKeywordIndexItem[] qikItems;
            IKeywordIndexItem[] codeGroupItems;

            bool anyExist =
                this.codeGroupLibrary.ImportIndecesExist(exportData, out codeGroupItems) &&
                this.codeLibrary.ImportIndecesExist(exportData, out codeItems) &&
                this.qikLibrary.ImportIndecesExist(exportData, out qikItems);

            if (anyExist)
                throw new ApplicationException("they exist!");

            ImportToIndex(sourceIndexFilePath, exportData);
            ImportToDisk(exportData);

            
        }

        /// <summary>
        /// NB. You could write to one index library at a time. That way you can create a library writer object (base)
        /// and have its sub classes change their functionality when they have to write to different index types and file types
        /// Generally you will just copy over the folder or file pointed to by the index.
        /// 
        /// First load the source index, capture its elements (they're all called IndexItem or ItemIndex)
        /// Copy all these elements and "append" them in mass to the destination index file.
        /// Copy all the files, folders they point to, to the correct destination.
        /// 
        /// REMEMBER TO CHECK THE VERSION !!!
        /// 
        /// ALSO CHECK WHETHER YOUR INDEX ITEMS HAVE A TYPE (OR BETTER YET A TYPE NAME FIELD) BECAUSE THEN YOU CAN ADD THIS TO
        /// THE LISTVIEW !!!
        /// </summary>
        /// <param name="sourceIndexFilePath"></param>
        /// <param name="exportData"></param>
        
        private void ImportToIndex(string sourceIndexFilePath, IndexExportImportData[] exportData)
        {
            //XDocument codeDocument = XDocument.Load(this.codeLibrary.FilePath);
            //XDocument codeGroupDocument = XDocument.Load(this.codeGroupLibrary.FilePath);
            //XDocument qikDocument = XDocument.Load(this.qikLibrary.FilePath);

            //foreach (IndexExportImportData item in data)
            //{
            //    if (item.KeywordIndexItem is ICodeGroupKeywordIndexItem)
            //    {

            //    }
            //    else if (item.KeywordIndexItem is ICodeKeywordIndexItem)
            //    {

            //    }
            //    else if (item.KeywordIndexItem is IQikKeywordIndexItem)
            //    {

            //    }
            //}
        }



        private void ImportToDisk(IndexExportImportData[] exportData)
        {
            throw new NotImplementedException();
        }
    }
}
