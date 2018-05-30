﻿using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Files.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
{
    //TODO: IPersistableTarget and a TopicSection? Need to look and identifiy whether these two can't be merged.
    public interface ITopicDocument : IKeywordTarget, IFile, ITitledEntity
    {
        event EventHandler<TopicSectionEventArgs> TopicSectionAdded;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedLeft;
        event EventHandler<TopicSectionEventArgs> TopicSectionMovedRight;
        event EventHandler<TopicSectionEventArgs> TopicSectionRemoved;

        string Text { get; set; }
        string Syntax { get; set; }
        bool TopicSectionExists(string id);

        // don't like this... should not be returning "all" documents.
        // should be treating a collection of Template and a collection of Script.
        ITopicSection[] TopicSections { get; }
        ITopicSection GetTopicSection(string id);

        ITopicSection AddTopicSection(TopicSectionType documentType, string title = "New Document", string syntax = null, string extension = null);
        void RemoveTopicSection(string id);

        void MoveTopicSectionLeft(string id);
        void MoveTopicSectionRight(string id);
    }
}