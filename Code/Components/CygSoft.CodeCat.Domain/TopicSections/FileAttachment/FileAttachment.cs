using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using System;
using System.Diagnostics;
using System.IO;

namespace CygSoft.CodeCat.Domain.TopicSections.FileAttachment
{
    public class FileAttachment : IFileAttachment
    {
        private string sourceFilePath;
        private FilePathGenerator originalFilePathGenerator;
        private FilePathGenerator currentFilePathGenerator;

        public FileAttachment(string folder, string fileName, string sourceFilePath)
        {
            this.originalFilePathGenerator = new FilePathGenerator(folder, fileName);
            this.currentFilePathGenerator = new FilePathGenerator(folder, fileName);
            this.Title = "";
            this.Description = "";
            this.Category = "Unknown";
            this.AllowOpenOrExecute = false;
            this.DateCreated = DateTime.Now;
            this.DateModified = this.DateCreated;
            this.sourceFilePath = sourceFilePath;
        }

        public FileAttachment(string folder, string id, string title, string fileName, bool allowOpenOrExecute, string category, string description, DateTime dateCreated, DateTime dateModified)
        {
            this.originalFilePathGenerator = new FilePathGenerator(folder, fileName, id);
            this.currentFilePathGenerator = new FilePathGenerator(folder, fileName, id);
            this.Title = title;
            this.Description = description;
            this.Category = category;
            this.AllowOpenOrExecute = allowOpenOrExecute;
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
        }

        public string Id
        {
            get { return this.currentFilePathGenerator.Id; }
        }

        public string FilePath { get { return currentFilePathGenerator.FilePath; } }
        public string FileName { get { return currentFilePathGenerator.FileName; } }
        public string FileTitle { get { return Path.GetFileNameWithoutExtension(currentFilePathGenerator.FileName); } }
        public string FileExtension { get { return currentFilePathGenerator.FileExtension; } }
        public string ModifiedFileName { get { return currentFilePathGenerator.ModifiedFileName; } }
        public string ModifiedFilePath { get { return currentFilePathGenerator.ModifiedFilePath; } }
        public string Category { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool AllowOpenOrExecute { get; set; }

        public bool HasFileName(string fileName)
        {
            return FileName.ToUpper() == fileName.ToUpper();
        }

        public bool FileExists { get { return File.Exists(FilePath); } }

        public bool ValidateImportFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            if (!File.Exists(filePath))
                return false;

            string extension = Path.GetExtension(filePath);
            if (extension != this.FileExtension && !string.IsNullOrEmpty(this.FileExtension))
                return false;

            return true;
        }

        public void Delete()
        {
            if (File.Exists(originalFilePathGenerator.FilePath)) File.Delete(originalFilePathGenerator.FilePath);
            if (File.Exists(currentFilePathGenerator.ModifiedFilePath)) File.Delete(currentFilePathGenerator.ModifiedFilePath);
            if (File.Exists(currentFilePathGenerator.FilePath)) File.Delete(currentFilePathGenerator.FilePath);
        }

        public void Revert()
        {
            if (File.Exists(currentFilePathGenerator.ModifiedFilePath)) File.Delete(currentFilePathGenerator.ModifiedFilePath);
        }

        public void ImportFile(string filePath)
        {
            if (!ValidateImportFile(filePath))
                throw new Exception("Import file does not exist, is not valid, or is not of the same extension as the existing file.");

            File.Copy(filePath, this.currentFilePathGenerator.ModifiedFilePath, true);
        }

        public void ChangeFileName(string fileName)
        {

            string currentExt = Path.GetExtension(this.FileName);
            string proposeExt = Path.GetExtension(fileName);
            if (currentExt != proposeExt)
                throw new Exception("Filename change will result in a different extension.");

            this.currentFilePathGenerator.RenameFile(fileName);
        }

        public void Save()
        {
            if (this.FileName != originalFilePathGenerator.FileName)
            {
                if (File.Exists(originalFilePathGenerator.FilePath))
                {
                    // Rename the physical file.
                    File.Move(originalFilePathGenerator.FilePath, currentFilePathGenerator.FilePath);
                }
                else
                {
                    File.Create(currentFilePathGenerator.FilePath);
                }

                // Rename the stored file name.
                originalFilePathGenerator.RenameFile(currentFilePathGenerator.FileName);
            }

            // this should always copy to the new file name.
            if (File.Exists(currentFilePathGenerator.ModifiedFilePath))
            {
                // Copy the temp file to the current file and delete.
                File.Copy(currentFilePathGenerator.ModifiedFilePath, currentFilePathGenerator.FilePath, true);
                File.Delete(currentFilePathGenerator.ModifiedFilePath);
            }
        }

        public void Open()
        {
            Process.Start(this.FilePath);
        }
    }
}
