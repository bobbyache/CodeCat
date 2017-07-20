using CygSoft.CodeCat.Category.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category
{
    public class ItemCategory : IdentityItem, IItemCategory
    {
        public ItemCategory() { }
        public ItemCategory(string guidString) : base(guidString) { }

        public string Title { get; set; }
    }
}
