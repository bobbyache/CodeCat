using CygSoft.CodeCat.Infrastructure;

namespace CygSoft.CodeCat.Category
{
    public class ItemCategory : IdentityItem, IItemCategory
    {
        public ItemCategory() { }
        public ItemCategory(string id) : base(id) { }

        public string Title { get; set; }
    }
}
