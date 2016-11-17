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
    public class DocumentFactory
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
        }

        public static IDocument Create(DocumentTypeEnum documentType, string folder, string title, string id = null, int ordinal = 0, string description = null, string extension = null, string syntax = null)
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

            return null;
        }

        public static DocumentTypeEnum GetDocumentType(string documentType)
        {
            switch (documentType)
            {
                case "CODESNIPPET":
                    return DocumentTypeEnum.CodeSnippet;

                case "QIKSCRIPT":
                    return DocumentTypeEnum.QikScript;

                case "URLGROUP":
                    return DocumentTypeEnum.UrlGroup;

                case "PDFDOCUMENT":
                    return DocumentTypeEnum.PdfDocument;

                case "IMAGEDOCUMENT":
                    return DocumentTypeEnum.ImageDocument;

                default:
                    return DocumentTypeEnum.CodeSnippet;
            }
        }

        public static string GetDocumentType(DocumentTypeEnum documentType)
        {
            switch (documentType)
            {
                case DocumentTypeEnum.CodeSnippet:
                    return "CODESNIPPET";
                case DocumentTypeEnum.QikScript:
                    return "QIKSCRIPT";
                case DocumentTypeEnum.UrlGroup:
                    return "URLGROUP";
                case DocumentTypeEnum.PdfDocument:
                    return "PDFDOCUMENT";
                case DocumentTypeEnum.ImageDocument:
                    return "IMAGEDOCUMENT";
                default:
                    return "CODESNIPPET";
            }
        }

        private static IDocument CreateImageDocument(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new ImageDocument(docArgs.Folder, docArgs.Title, docArgs.Extension);
            else
                return new ImageDocument(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Extension, docArgs.Ordinal, docArgs.Description);
        }

        private static IDocument CreatePdfDocument(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new PdfDocument(docArgs.Folder, docArgs.Title);
            else
                return new PdfDocument(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Ordinal, docArgs.Description);
        }

        private static IDocument CreateUrlGroup(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new UrlGroupDocument(docArgs.Folder, docArgs.Title);
            else
                return new UrlGroupDocument(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Ordinal, docArgs.Description);
        }

        private static IDocument CreateCodeSnippet(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new CodeDocument(docArgs.Folder, docArgs.Title, docArgs.Extension, docArgs.Syntax);
            else
                return new CodeDocument(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Extension, docArgs.Ordinal, docArgs.Description, docArgs.Syntax);
        }

        private static IDocument CreateQikScript(DocArgs docArgs)
        {
            if (docArgs.Id == null)
                return new QikScriptDocument(docArgs.Folder, docArgs.Title, docArgs.Extension, docArgs.Syntax);
            else
                return new QikScriptDocument(docArgs.Folder, docArgs.Id, docArgs.Title, docArgs.Extension, docArgs.Ordinal, docArgs.Description, docArgs.Syntax);
        }
    }
}
