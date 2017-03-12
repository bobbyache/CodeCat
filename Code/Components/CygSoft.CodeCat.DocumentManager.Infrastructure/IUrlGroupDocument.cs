using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IUrlItem
    {
        string Id { get; set; }
        string Title { get; set; }
        string Category { get; set; }
        string Url { get; set; }
        string HostName { get; }
        string Description { get; set; }
        DateTime DateModified { get; set; }
        DateTime DateCreated { get; set; }
    }

    public interface IUrlGroupDocument : IDocument, IPositionedItem
    {
        event EventHandler Paste;
        event EventHandler PasteConflict;

        IUrlItem[] Items { get; }
        string[] Categories { get; }
        void Add(IUrlItem urlItem);
        void Remove(IUrlItem urlItem);
        string CopyXmlFor(string[] ids);
        void PasteXml(string xml);
        IUrlItem CreateNewUrl();
    }
}
