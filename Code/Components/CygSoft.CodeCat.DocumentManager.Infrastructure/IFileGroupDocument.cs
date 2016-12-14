using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IFileGroupFile
    {
        string Id { get; }
        string Title { get; set; }
        string Category { get; set; }
        string FileName { get; }
        string FileExtension { get; }
        string Description { get; set; }
        DateTime DateModified { get; set; }
        DateTime DateCreated { get; set; }

        bool ValidateImportFile(string filePath);
        void ImportFile(string filePath);
        void ChangeFileName(string fileName);
        void Save();
        void Delete();
        void Revert();

    }

    public interface IFileGroupDocument : IVersionableFile, IPositionedItem
    {
        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
        IFileGroupFile[] Items { get; }
        string[] Categories { get; }
        void Add(IFileGroupFile file);
        void Remove(IFileGroupFile file);
        IFileGroupFile CreateNewFile(string fileName, string sourcePath);
    }
}
