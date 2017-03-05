using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IImageSetDocument : IVersionableFile, IPositionedItem
    {
        event EventHandler ImageAdded;
        event EventHandler ImageRemoved;
        event EventHandler ImageMovedUp;
        event EventHandler ImageMovedDown;

        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
        int ImageCount { get; }
        IImgDocument FirstImage { get; }

        bool IsLastImage(IImgDocument imageItem);
        IImgDocument NextImage(IImgDocument imageItem);
        bool IsFirstImage(IImgDocument imageItem);
        IImgDocument PreviousImage(IImgDocument imageItem);


        IImgDocument Add(); // Adds a blank image (default image/initial image)
        IImgDocument Add(string description, string extension);
        void Remove(IImgDocument urlItem);
        bool CanMovePrevious(IImgDocument documentFile);
        bool CanMoveNext(IImgDocument documentFile);
        void MovePrevious(IImgDocument documentFile);
        void MoveNext(IImgDocument documentFile);
    }
}
