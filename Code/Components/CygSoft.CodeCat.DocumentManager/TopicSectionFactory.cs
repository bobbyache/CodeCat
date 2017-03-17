using CygSoft.CodeCat.DocumentManager.Documents;
using CygSoft.CodeCat.DocumentManager.Documents.FileGroup;
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
        private class TopicSectionArgs
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
            TopicSectionArgs topicSectionArgs = new TopicSectionArgs { DocumentType = GetDocumentType(documentType), Folder = folder, Title = title, Id = id, Ordinal = ordinal, Description = description, Extension = extension, Syntax = syntax };

            if (topicSectionArgs.DocumentType == "CODESNIPPET")
                return CreateCodeTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == "URLGROUP")
                return CreateWebReferencesTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == "QIKSCRIPT")
                return CreateQikScriptTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == "PDFDOCUMENT")
                return CreatePdfViewerTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == "IMAGEDOCUMENT")
                return CreateSingleImageTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == "IMAGESET")
                return CreateImagePagerTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == "RICHTEXT")
                return CreateRichTextEditorTopicSection(topicSectionArgs);

            if (topicSectionArgs.DocumentType == "FILEGROUP")
                return CreateFileAttachmentsTopicSection(topicSectionArgs);

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


        private static ITopicSection CreateFileAttachmentsTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new FileAttachmentsTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new FileAttachmentsTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreateImagePagerTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new ImagePagerTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new ImagePagerTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreateSingleImageTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new SingleImageTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title, topicSectionArgs.Extension);
            else
                return new SingleImageTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreateRichTextEditorTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new RichTextEditorTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new RichTextEditorTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreatePdfViewerTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new PdfViewerTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new PdfViewerTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreateWebReferencesTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new WebReferencesTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title);
            else
                return new WebReferencesTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Ordinal, topicSectionArgs.Description);
        }

        private static ITopicSection CreateCodeTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new CodeTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Syntax);
            else
                return new CodeTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Ordinal, topicSectionArgs.Description, topicSectionArgs.Syntax);
        }

        private static ITopicSection CreateQikScriptTopicSection(TopicSectionArgs topicSectionArgs)
        {
            if (topicSectionArgs.Id == null)
                return new QikScriptTopicSection(topicSectionArgs.Folder, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Syntax);
            else
                return new QikScriptTopicSection(topicSectionArgs.Folder, topicSectionArgs.Id, topicSectionArgs.Title, topicSectionArgs.Extension, topicSectionArgs.Ordinal, topicSectionArgs.Description, topicSectionArgs.Syntax);
        }
    }
}
