using CygSoft.CodeCat.DocumentManager.Documents;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager
{
    public class TopicSectionFactory
    {
        private class DocArgs
        {
            public string DocumentType { get; set; }
            public string Folder { get; set; }
            public string Title { get; set; }
            public string Id { get; set; }
            public int Ordinal { get; set; }
            public string Description { get; set; }
            public string Extension {get; set;}
            public string Syntax { get; set; }
            public string FileName { get; set; }
        }

        public static ITopicSection Create(TopicSectionType documentType, string folder, string title, string id = null, int ordinal = 0, string description = null, string extension = null, string syntax = null)
        {
            DocArgs docArgs = new DocArgs { DocumentType = GetDocumentType(documentType), Folder = folder, Title = title, Id = id, Ordinal = ordinal, Description = description, Extension = extension, Syntax = syntax };

            if (docArgs.DocumentType == "CODESNIPPET")
                return CreateCodeSnippet(docArgs);

            if (docArgs.DocumentType == "URLGROUP")
                return CreateUrlGroup(docArgs);

            if (docArgs.DocumentType == "QIKSCRIPT")
                return CreateQikScript(docArgs);

            if (docArgs.DocumentType == "PDFDOCUMENT")
                return CreatePdfDocument(docArgs);

            if (docArgs.DocumentType == "IMAGEDOCUMENT")
                return CreateImageDocument(docArgs);

            if (docArgs.DocumentType == "IMAGESET")
                return CreateImageSet(docArgs);

            if (docArgs.DocumentType == "RICHTEXT")
                return CreateRichText(docArgs);

            if (docArgs.DocumentType == "FILEGROUP")
                return CreateFileGroup(docArgs);

            return null;
        }

        public static TopicSectionType GetDocumentType(string documentType)
        {
            switch (documentType)
            {
                case "CODESNIPPET":
                    return TopicSectionType.CodeSnippet;

                case "QIKSCRIPT":
                    return TopicSectionType.QikScript;

                case "URLGROUP":
                    return TopicSectionType.UrlGroup;

                case "PDFDOCUMENT":
                    return TopicSectionType.PdfDocument;

                case "IMAGEDOCUMENT":
                    return TopicSectionType.ImageDocument;

                case "IMAGESET":
                    return TopicSectionType.ImageSet;
                case "RICHTEXT":
                    return TopicSectionType.RichTextDocument;

                case "FILEGROUP":
                    return TopicSectionType.FileGroup;

                default:
                    return TopicSectionType.CodeSnippet;
            }
        }

        public static string GetDocumentType(TopicSectionType documentType)
        {
            switch (documentType)
            {
                case TopicSectionType.CodeSnippet:
                    return "CODESNIPPET";
                case TopicSectionType.QikScript:
                    return "QIKSCRIPT";
                case TopicSectionType.UrlGroup:
                    return "URLGROUP";
                case TopicSectionType.PdfDocument:
                    return "PDFDOCUMENT";
                case TopicSectionType.ImageDocument:
                    return "IMAGEDOCUMENT";
                case TopicSectionType.ImageSet:
                    return "IMAGESET";
                case TopicSectionType.RichTextDocument:
                    return "RICHTEXT";
                case TopicSectionType.FileGroup:
                    return "FILEGROUP";

                default:
                    return "CODESNIPPET";
            }
        }


        private static ITopicSection CreateFileGroup(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new FileAttachmentsTopicSection(docArgs.Folder, docArgs.Title);
            else
                return new FileAttachmentsTopicSection(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Ordinal, docArgs.Description);
        }

        private static ITopicSection CreateImageSet(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new ImageSetDocument(docArgs.Folder, docArgs.Title);
            else
                return new ImageSetDocument(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Ordinal, docArgs.Description);
        }

        private static ITopicSection CreateImageDocument(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new ImageDocument(docArgs.Folder, docArgs.Title, docArgs.Extension);
            else
                return new ImageDocument(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Extension, docArgs.Ordinal, docArgs.Description);
        }

        private static ITopicSection CreateRichText(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new RichTextDocument(docArgs.Folder, docArgs.Title);
            else
                return new RichTextDocument(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Ordinal, docArgs.Description);
        }

        private static ITopicSection CreatePdfDocument(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new PdfDocument(docArgs.Folder, docArgs.Title);
            else
                return new PdfDocument(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Ordinal, docArgs.Description);
        }

        private static ITopicSection CreateUrlGroup(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new WebReferencesTopicSection(docArgs.Folder, docArgs.Title);
            else
                return new WebReferencesTopicSection(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Ordinal, docArgs.Description);
        }

        private static ITopicSection CreateCodeSnippet(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new CodeTopicSection(docArgs.Folder, docArgs.Title, docArgs.Extension, docArgs.Syntax);
            else
                return new CodeTopicSection(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Extension, docArgs.Ordinal, docArgs.Description, docArgs.Syntax);
        }

        private static ITopicSection CreateQikScript(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new QikScriptTopicSection(docArgs.Folder, docArgs.Title, docArgs.Extension, docArgs.Syntax);
            else
                return new QikScriptTopicSection(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Extension, docArgs.Ordinal, docArgs.Description, docArgs.Syntax);
        }
    }
}
