using CygSoft.CodeCat.Syntax.Infrastructure;
using System.Collections.Generic;

namespace CygSoft.CodeCat.Infrastructure.Graphics
{
    public interface IIconRepository
    {
        IImageResources ImageResources { get; }

        IImageOutput Get(string key, bool isFileExtensionKey = false);
    }
}
