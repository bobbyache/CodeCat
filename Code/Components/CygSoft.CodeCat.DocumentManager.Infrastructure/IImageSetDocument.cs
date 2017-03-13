using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IImageSetDocument : ITopicSection, IPositionedItem
    {
        event EventHandler ImageAdded;
        event EventHandler ImageRemoved;
        event EventHandler ImageMovedUp;
        event EventHandler ImageMovedDown;

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
