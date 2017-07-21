using CygSoft.CodeCat.Category.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category
{
    public class CategorizedItem : IdentityItem, ICategorizedItem
    {
        public CategorizedItem() { }
        public CategorizedItem(string guidString) : base(guidString) { }

        public string Title { get; set; }
    }
}
