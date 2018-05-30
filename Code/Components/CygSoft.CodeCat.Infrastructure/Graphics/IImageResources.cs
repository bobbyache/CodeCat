using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure.Graphics
{
    public interface IImageResources
    {
        Image GetImage(string key);
    }
}
