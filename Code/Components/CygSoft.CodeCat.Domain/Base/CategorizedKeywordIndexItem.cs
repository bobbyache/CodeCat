using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.Domain.Base
{
    public class CategorizedKeywordIndexItem : ICategorizedKeywordIndexItem
    {
        public CategorizedKeywordIndexItem(string id, IKeywordIndexItem indexItem)
        {
            this.Id = id;
            this.IndexItem = indexItem;
        }

        public string Id { get; private set; }
        public IKeywordIndexItem IndexItem { get; private set; }
        public string ItemId { get { return IndexItem.Id; } }
        public string Title
        {
            get { return IndexItem.Title; }
            set { IndexItem.Title = value; }
        }
    }
}
