using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IImagePagerTopicSection : ITopicSection, IPositionedItem
    {
        event EventHandler ImageAdded;
        event EventHandler ImageRemoved;
        event EventHandler ImageMovedUp;
        event EventHandler ImageMovedDown;

        int ImageCount { get; }
        IImagePagerImageTopicSection FirstImage { get; }

        bool IsLastImage(IImagePagerImageTopicSection imageItem);
        IImagePagerImageTopicSection NextImage(IImagePagerImageTopicSection imageItem);
        bool IsFirstImage(IImagePagerImageTopicSection imageItem);
        IImagePagerImageTopicSection PreviousImage(IImagePagerImageTopicSection imageItem);


        IImagePagerImageTopicSection Add(); // Adds a blank image (default image/initial image)
        IImagePagerImageTopicSection Add(string description, string extension);
        void Remove(IImagePagerImageTopicSection urlItem);
        bool CanMovePrevious(IImagePagerImageTopicSection documentFile);
        bool CanMoveNext(IImagePagerImageTopicSection documentFile);
        void MovePrevious(IImagePagerImageTopicSection documentFile);
        void MoveNext(IImagePagerImageTopicSection documentFile);
    }
}
