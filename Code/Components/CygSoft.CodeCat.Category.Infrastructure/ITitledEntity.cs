using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category.Infrastructure
{
    public interface ITitledEntity
    {
        string Id { get; set; }
        string Title { get; set; }
    }
}
