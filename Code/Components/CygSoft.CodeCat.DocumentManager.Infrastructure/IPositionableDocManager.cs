using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IPositionableDocManager
    {
        bool CanMoveDown(IDocumentFile documentFile);
        bool CanMoveTo(IDocumentFile documentFile, int ordinal);
        bool CanMoveUp(IDocumentFile documentFile);
        void MoveDown(IDocumentFile documentFile);
        void MoveTo(IDocumentFile documentFile, int ordinal);
        void MoveUp(IDocumentFile documentFile);
    }
}
