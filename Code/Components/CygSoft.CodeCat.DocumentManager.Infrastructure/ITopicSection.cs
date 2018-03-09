using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface ITopicSection  : IPositionedItem, IFile
    {
        string Id { get; }
        string FilePath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string Folder { get; }
        bool FolderExists { get; }
        bool Exists { get; }
        bool Loaded { get; }
        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }

        void Open();
        void Delete();
        void Save();
        void Close();
        void Revert();
    }
}
