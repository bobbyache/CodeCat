using System.Drawing;

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
