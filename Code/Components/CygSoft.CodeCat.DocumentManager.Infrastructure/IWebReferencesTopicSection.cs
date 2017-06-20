using System;
using System.Collections.Generic;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IWebReference : ICategorizedItem
    {
        string Id { get; set; }
        string Title { get; set; }
        string Url { get; set; }
        string HostName { get; }
        string Description { get; set; }
        DateTime DateModified { get; set; }
        DateTime DateCreated { get; set; }
    }

    public interface IWebReferencesTopicSection : ITopicSection
    {
        event EventHandler Paste;
        event EventHandler PasteConflict;

        IWebReference[] WebReferences { get; }
        string[] Categories { get; }
        void Add(IWebReference webReference);
        void Remove(IWebReference webReference);
        void Remove(IEnumerable<IWebReference> webReferences);
        string GetXml(string[] ids);
        void AddXml(string xml);
        IWebReference CreateWebReference();
        IWebReference CreateWebReference(string url, string title, string description, string category = "Unknown");

        bool IsFullUrl(string url);
        bool IsValidWebReferenceXml(string xml);
    }
}
