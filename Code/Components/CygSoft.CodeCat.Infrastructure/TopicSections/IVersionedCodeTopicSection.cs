using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure.TopicSections
{
    public interface IVersionedCodeTopicSection : ICodeTopicSection, IVersionableFile
    {
    }
}
