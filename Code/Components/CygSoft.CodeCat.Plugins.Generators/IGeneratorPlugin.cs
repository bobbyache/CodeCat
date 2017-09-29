using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Plugins.Generators
{
    public interface IGeneratorPlugin
    {
        string Id { get; }
        string Title { get; }
        event EventHandler Generated;
    }
}
