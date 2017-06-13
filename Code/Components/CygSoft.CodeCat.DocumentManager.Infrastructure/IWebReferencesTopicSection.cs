﻿using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    public interface IWebReference
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

    public interface IWebReferencesTopicSection : ITopicSection, IPositionedItem
    {
        event EventHandler Paste;
        event EventHandler PasteConflict;

        IWebReference[] WebReferences { get; }
        string[] Categories { get; }
        void Add(IWebReference webReference);
        void Remove(IWebReference webReference);
        string GetXml(string[] ids);
        void AddXml(string xml);
        IWebReference CreateWebReference();
        IWebReference CreateWebReference(string url, string title, string description, string category = "Unknown");

        bool IsFullUrl(string url);
        bool IsValidWebReferenceXml(string xml);
    }
}
