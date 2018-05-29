using System;

namespace CygSoft.CodeCat.Infrastructure.TopicSections
{
    public interface IImagePagerTopicSection : ITopicSection
    {
        event EventHandler ImageAdded;
        event EventHandler ImageRemoved;
        event EventHandler ImageMovedUp;
        event EventHandler ImageMovedDown;

        int ImageCount { get; }
        IPagerImage FirstImage { get; }

        bool IsLastImage(IPagerImage pagerImage);
        IPagerImage NextImage(IPagerImage pagerImage);
        bool IsFirstImage(IPagerImage pagerImage);
        IPagerImage PreviousImage(IPagerImage pagerImage);


        IPagerImage Add(); // Adds a blank image (default image/initial image)
        IPagerImage Add(string description, string extension);
        void Remove(IPagerImage pagerImage);
        bool CanMovePrevious(IPagerImage pagerImage);
        bool CanMoveNext(IPagerImage pagerImage);
        void MovePrevious(IPagerImage pagerImage);
        void MoveNext(IPagerImage pagerImage);
    }
}
