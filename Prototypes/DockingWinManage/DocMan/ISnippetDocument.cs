using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weif_1_Test
{
    public interface ISnippetDocument
    {
        string Id { get; }
        bool IsModified { get; }
        bool IsNew { get; }

        void SaveChanges();
        void DiscardChanges();
    }
}
