using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure.Search.KeywordIndex
{
    public interface IKeywordSearchIndexRepository
    {
        IKeywordSearchIndex OpenIndex(string filePath, int currentVersion);
        void SaveIndex(IKeywordSearchIndex Index);
        IKeywordSearchIndex SaveIndexAs(IKeywordSearchIndex Index, string filePath);
        IKeywordSearchIndex CloneIndex(IKeywordSearchIndex sourceIndex, string filePath);
        IKeywordSearchIndex CreateIndex(string filePath, int currentVersion);

        void ImportItems(string filePath, int currentVersion, IKeywordIndexItem[] items);
    }
}
