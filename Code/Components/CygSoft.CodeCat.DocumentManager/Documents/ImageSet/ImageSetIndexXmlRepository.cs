using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace CygSoft.CodeCat.DocumentManager.Documents.ImageSet
{
    internal class ImageSetIndexXmlRepository : IDocumentIndexRepository
    {
        private readonly string filePath;
        private readonly string folder;

        public ImageSetIndexXmlRepository(ImageSetIndexPathGenerator indexPathGenerator)
        {
            this.filePath = indexPathGenerator.FilePath;
            this.folder = Path.GetDirectoryName(indexPathGenerator.FilePath);
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

                IImagePagerImageTopicSection item = new ImagePagerImageTopicSection(
                    imagePathGenerator,
                    (int)element.Attribute("Ordinal"),
                    (string)element.Element("Description")
                );

                imageItems.Add(item);
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

            foreach (ITopicSection docFile in documents)
            {
                if (docFile is IImagePagerImageTopicSection)
                {
                    IImagePagerImageTopicSection imgDoc = docFile as IImagePagerImageTopicSection;

                    filesElement.Add(new XElement("Image",
                        new XAttribute("Id", imgDoc.Id),
                        new XAttribute("Extension", imgDoc.FileExtension),
                        new XAttribute("Ordinal", imgDoc.Ordinal),
                        new XElement("Description", new XCData(imgDoc.Description))
                    ));
                }
            }

            indexDocument.Save(this.filePath);
        }
    }
}
