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
        IImageItem[] Images { get; }

        IImageItem FirstImage { get; }

        bool IsLastImage(IImageItem imageItem);
        IImageItem NextImage(IImageItem imageItem);
        bool IsFirstImage(IImageItem imageItem);
        IImageItem PreviousImage(IImageItem imageItem);

        void Add(IImageItem imageItem);
        void Remove(IImageItem urlItem);
        bool CanMovePrevious(IImageItem documentFile);
        bool CanMoveNext(IImageItem documentFile);
        void MovePrevious(IImageItem documentFile);
        void MoveNext(IImageItem documentFile);
    }
}
