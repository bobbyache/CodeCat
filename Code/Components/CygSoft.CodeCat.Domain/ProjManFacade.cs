using CygSoft.CodeCat.Domain.Management;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Linq;

namespace CygSoft.CodeCat.Domain
{
    public class ProjManFacade
    {
        private ManageableProject sourceProject;
        private ManageableProject destinationProject;
        private string destinationProjectFilePath;
        private Version version;

        public bool Loaded
        {
            get 
            {
                if (this.sourceProject == null || this.destinationProject == null)
                    return false;
                return this.sourceProject.Loaded && this.destinationProject.Loaded; 
            }
        }

        public void LoadProjects(string fromProjectFile, string toProjectFile, Version version)
        {
            if (version == null)
                throw new InvalidOperationException("A valid version is required in order to continue.");
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

        public void ImportData(IndexExportImportData[] exportData, Version currentVersion)
        {
            if (currentVersion == null)
                throw new InvalidOperationException("A valid version is required in order to continue.");
            destinationProject.Open(destinationProjectFilePath, version);
            this.destinationProject.ImportData(this.sourceProject.RootFilePath, this.destinationProject.RootFilePath, exportData, currentVersion);
        }
    }
}
