using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Domain.TopicSections.ImagePager
{
    internal class ImageSetIndexXmlRepository : IDocumentIndexRepository
    {
        private readonly string filePath;
        private readonly string folder;

        public ImageSetIndexXmlRepository(ImagePagerPathGenerator imagePagerPathGenerator)
        {
            this.filePath = imagePagerPathGenerator.FilePath;
            this.folder = Path.GetDirectoryName(imagePagerPathGenerator.FilePath);
        }

        public List<ITopicSection> LoadDocuments()
        {
            List<ITopicSection> imageItems = new List<ITopicSection>();
            XDocument xDocument = XDocument.Load(this.filePath);

            foreach (XElement element in xDocument.Element("ImageSet").Elements("Images").Elements())
            {
                string id = (string)element.Attribute("Id");
                string extension = (string)element.Attribute("Extension");
                ImagePathGenerator imagePathGenerator = new ImagePathGenerator(this.folder, extension, id);

                IPagerImage pagerImage = new PagerImage(
                    imagePathGenerator,
                    (int)element.Attribute("Ordinal"),
                    (string)element.Element("Description")
                );

                imageItems.Add(pagerImage);
            }

            return imageItems;
        }

        public void WriteDocuments(List<ITopicSection> documents)
        {
            if (!File.Exists(this.filePath))
                CreateFile();
            WriteFile(documents);
        }

        private void CreateFile()
        {
            Directory.CreateDirectory(this.folder);

            XElement rootElement = new XElement("ImageSet", new XElement("Images"));
            XDocument xDocument = new XDocument(rootElement);
            xDocument.Save(this.filePath);
        }

        private void WriteFile(List<ITopicSection> documents)
        {
            XDocument indexDocument = XDocument.Load(this.filePath);
            XElement filesElement = indexDocument.Element("ImageSet").Element("Images");
            filesElement.RemoveNodes();

            foreach (ITopicSection topicSection in documents)
            {
                if (topicSection is IPagerImage)
                {
                    IPagerImage pagerImage = topicSection as IPagerImage;

                    filesElement.Add(new XElement("Image",
                        new XAttribute("Id", pagerImage.Id),
                        new XAttribute("Extension", pagerImage.FileExtension),
                        new XAttribute("Ordinal", pagerImage.Ordinal),
                        new XElement("Description", new XCData(pagerImage.Description))
                    ));
                }
            }

            indexDocument.Save(this.filePath);
        }
    }
}
