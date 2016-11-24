using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //IImageItem[] Images { get; }
        int ImageCount { get; }
        IImgDocument FirstImage { get; }

        bool IsLastImage(IImgDocument imageItem);
        IImgDocument NextImage(IImgDocument imageItem);
        bool IsFirstImage(IImgDocument imageItem);
        IImgDocument PreviousImage(IImgDocument imageItem);

        void Add(IImgDocument imageItem);
        void Remove(IImgDocument urlItem);
        bool CanMovePrevious(IImgDocument documentFile);
        bool CanMoveNext(IImgDocument documentFile);
        void MovePrevious(IImgDocument documentFile);
        void MoveNext(IImgDocument documentFile);
    }
}
