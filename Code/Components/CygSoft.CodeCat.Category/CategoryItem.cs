using CygSoft.CodeCat.Category.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category
{
    public class CategoryItem : IdentityItem, ICategoryItem
    {
        public CategoryItem() { }
        public CategoryItem(string guidString) : base(guidString) { }

        public string Title { get; set; }
    }
}
