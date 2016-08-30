using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IDocument  : IVersionableFile, IPositionedItem
    {
        string Title { get; set; }
        string Description { get; set; }
    }
}
