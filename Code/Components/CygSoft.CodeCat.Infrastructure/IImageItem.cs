using System;

namespace CygSoft.CodeCat.Infrastructure
{
    public interface IImageItem : IPositionedItem
    {
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
        string Description { get; set; }
        string Extension { get; }
        string FileName { get; }
        string FilePath { get; }
        string FolderPath { get; }
        string Id { get; set; }
        string UnsavedFileName { get; }
    }
}
