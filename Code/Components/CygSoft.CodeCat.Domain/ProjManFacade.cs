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
        private string destinationProjectFilePath;
        private int version;

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
            destinationProjectFilePath = toProjectFile;
            this.version = version;
            sourceProject.Open(fromProjectFile, this.version);
        }

        public IKeywordIndexItem[] FindIndeces(string commaDelimitedKeywords, out bool hasPotentialDuplicates, out IKeywordIndexItem[] potentialDuplicates)
        {
            IKeywordIndexItem[] keywordIndexItems = sourceProject.FindIndeces(commaDelimitedKeywords);
            IndexExportImportData[] exportData = sourceProject.GetExportData(keywordIndexItems);
            destinationProject.Open(destinationProjectFilePath, version);
            hasPotentialDuplicates = destinationProject.FindPotentialDuplicates(exportData, out potentialDuplicates);

            return keywordIndexItems.ToArray();
        }

        public IndexExportImportData[] GetExportData(IKeywordIndexItem[] indexItems, out bool hasPotentialDuplicates, out IKeywordIndexItem[] potentialDuplicates)
        {
            IndexExportImportData[] exportData = sourceProject.GetExportData(indexItems);
            destinationProject.Open(destinationProjectFilePath, version);
            hasPotentialDuplicates = destinationProject.FindPotentialDuplicates(exportData, out potentialDuplicates);

            return exportData;
        }

        public void ImportData(IndexExportImportData[] exportData, int currentVersion)
        {
            destinationProject.Open(destinationProjectFilePath, version);
            this.destinationProject.ImportData(this.sourceProject.RootFilePath, this.destinationProject.RootFilePath, exportData, currentVersion);
        }
    }
}
