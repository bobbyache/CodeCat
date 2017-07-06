using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.TopicSections.SearchableEventDiary
{
    public interface ISearchableEventTopicSection : ITextTopicSection
    {
        ISearchableEventKeywordIndexItem[] Find(string commaDelimitedKeywordList);
        ISearchableEventKeywordIndexItem NewEvent(string title);
        string[] Keywords { get; }
        string[] Categories { get; }

        void AddEvent(ISearchableEventKeywordIndexItem diaryEvent);
        void UpdateEvent(ISearchableEventKeywordIndexItem diaryEvent);
        void DeleteEvent(string id);
        void DeleteEvents(IEnumerable<ISearchableEventKeywordIndexItem> diaryEvents);
    }

    public class SearchableEventTopicSection : TextTopicSection, ISearchableEventTopicSection
    {
        IKeywordSearchIndex searchIndex = null;
        private SearchableEventKeywordSearchIndexRepository repository = new SearchableEventKeywordSearchIndexRepository("Snippet_Repository");

        public SearchableEventTopicSection(string folder, string title, string extension)
            : base(folder, title, extension)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.SearchableEvent);
        }

        public SearchableEventTopicSection(string folder, string id, string title, string extension, int ordinal, string description)
            : base(folder, id, title, extension, ordinal, null)
        {
            this.DocumentType = SectionTypes.GetDocumentType(TopicSectionType.SearchableEvent);
        }

        protected override void OnOpen()
        {
            if (!this.Exists)
                repository.CreateIndex(this.FilePath, GetVersion());
            searchIndex = repository.OpenIndex(this.FilePath, GetVersion());
        }

        protected override void OnSave()
        {
            if (this.searchIndex == null)
                searchIndex = repository.CreateIndex(this.FilePath, GetVersion());
            repository.SaveIndex(searchIndex);
        }

        private Version GetVersion()
        {
            return new Version(1, 0);
        }

        public string[] Categories { get { return EventDateGrouper.Groups; } }

        public string[] Keywords
        {
            get
            {
                if (searchIndex != null)
                    return searchIndex.Keywords;
                else
                    return new string[0];
            }
        }

        public void UpdateEvent(ISearchableEventKeywordIndexItem diaryEvent)
        {
            searchIndex.Update(diaryEvent);
        }

        public void AddEvent(ISearchableEventKeywordIndexItem diaryEvent)
        {
            searchIndex.Update(diaryEvent);
        }

        public void DeleteEvents(IEnumerable<ISearchableEventKeywordIndexItem> diaryEvents)
        {
            foreach (ISearchableEventKeywordIndexItem diaryEvent in diaryEvents)
                DeleteEvent(diaryEvent.Id);
        }

        public void DeleteEvent(string id)
        {
            searchIndex.Remove(id);
        }

        public ISearchableEventKeywordIndexItem NewEvent(string title)
        {
            return new SearchableEventKeywordIndexItem() { Title = title };
        }

        public ISearchableEventKeywordIndexItem[] Find(string commaDelimitedKeywordList)
        {
            if (searchIndex == null)
                return new SearchableEventKeywordIndexItem[0];

            if (commaDelimitedKeywordList.Trim() == string.Empty)
                return searchIndex.All().OfType<ISearchableEventKeywordIndexItem>().OrderByDescending(k => k.DateCreated).ToArray();
            else
                return searchIndex.Find(commaDelimitedKeywordList).OfType<ISearchableEventKeywordIndexItem>().OrderByDescending(k => k.DateCreated).ToArray();
        }
    }
}
