using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IImageItem : IPositionedItem
    {
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
        string Description { get; set; }
        string Extension { get; }
        string FileName { get; }
        string Id { get; set; }
        string UnsavedFileName { get; }
        int Ordinal { get; set; }
    }
}
