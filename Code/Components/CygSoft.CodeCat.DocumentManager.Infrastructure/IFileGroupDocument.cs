using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IFileAttachment
    {
        string Id { get; }
        string Title { get; set; }
        string Category { get; set; }
        string FileName { get; }
        string FilePath { get; }
        bool FileExists { get; }
        string FileTitle { get; }
        string ModifiedFileName { get; }
        string ModifiedFilePath { get; }
        string FileExtension { get; }
        string Description { get; set; }
        bool AllowOpenOrExecute { get; set; }
        DateTime DateModified { get; set; }
        DateTime DateCreated { get; set; }

        bool HasFileName(string fileName);
        bool ValidateImportFile(string filePath);
        void ImportFile(string filePath);
        void ChangeFileName(string fileName);
        void Save();
        void Delete();
        void Revert();
        void Open();

    }

    public interface IFileAttachmentsTopicSection : ITopicSection, IPositionedItem
    {
        IFileAttachment[] Items { get; }
        string[] Categories { get; }
        void Add(IFileAttachment fileAttachment);
        void Remove(IFileAttachment fileAttachment);
        bool ValidateFileName(string fileName, string id = "");
        IFileAttachment CreateNewFile(string fileName, string sourcePath);
    }
}
