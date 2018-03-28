using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TopicSections.CodeTemplate.UnitTests
{
    public class HelperMethods
    {
        public static IFileIndexRepository GetRepositoryWithTwoItems()
        {
            var fileIndexRepository = new Mock<IFileIndexRepository>();
            fileIndexRepository.Setup(r => r.LoadIndeces()).Returns(() =>
            {
                return new List<IIndexFile>
                {
                    new IndexFile { FilePath = "Template_01.tpl" },
                    new IndexFile { FilePath = "Template_01.tpl" }
                };
            });

            return fileIndexRepository.Object;
        }
    }
}
