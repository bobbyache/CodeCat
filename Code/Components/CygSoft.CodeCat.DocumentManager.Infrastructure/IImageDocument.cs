using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IImageDocument :  IVersionableFile, IPositionedItem
    {
        string Title { get; set; }
        string DocumentType { get; set; }
        string Description { get; set; }
    }
}
