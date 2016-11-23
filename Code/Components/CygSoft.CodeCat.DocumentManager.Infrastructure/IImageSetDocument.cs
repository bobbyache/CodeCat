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

        void Add(IImageItem imageItem);
        void Remove(IImageItem urlItem);
        bool CanMoveDown(IImageItem documentFile);
        bool CanMoveUp(IImageItem documentFile);
        void MoveDown(IImageItem documentFile);
        void MoveUp(IImageItem documentFile);
    }
}
