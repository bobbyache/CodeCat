using CygSoft.CodeCat.Domain.Code.Base;
using CygSoft.CodeCat.Domain.CodeGroup;
using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Domain.Qik;
using CygSoft.CodeCat.Infrastructure.Search.KeywordIndex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain
{
    public class ProjManFacade
    {
        private ManageableProject sourceProject;
        private ManageableProject destinationProject;

        public bool Loaded
        {
            get 
            {
                if (this.sourceProject == null || this.destinationProject == null)
                    return false;
                return this.sourceProject.Loaded && this.destinationProject.Loaded; 
            }
        }

        public void LoadProjects(string fromProjectFile, string toProjectFile, int version)
        {
            sourceProject = new ManageableProject();
            destinationProject = new ManageableProject();

            sourceProject.Open(fromProjectFile, version);
            destinationProject.Open(toProjectFile, version);
        }

        public IKeywordIndexItem[] FindIndeces(string commaDelimitedKeywords)
        {
            IKeywordIndexItem[] keywordIndexItems = sourceProject.FindIndeces(commaDelimitedKeywords);

            return keywordIndexItems.ToArray();
        }

        public IndexExportImportData[] GetExportData(IKeywordIndexItem[] indexItems)
        {
            return sourceProject.GetExportData(indexItems);
        }

        public void ImportData(IndexExportImportData[] exportData)
        {
            this.destinationProject.ImportData(this.sourceProject.RootFilePath, exportData);
        }
    }
}
