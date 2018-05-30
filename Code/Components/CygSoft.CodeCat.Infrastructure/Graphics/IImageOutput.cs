using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure.Graphics
{
    public interface IImageOutput
    {
        Icon Icon { get; }
        Image Image { get; }
        string ImageKey { get; }
        int Index { get; }
    }
}
