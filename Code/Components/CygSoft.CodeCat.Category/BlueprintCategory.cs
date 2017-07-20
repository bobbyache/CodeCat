using CygSoft.CodeCat.Category.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category
{
    public class BlueprintCategory : IdentityItem, ITitledEntity
    {
        public BlueprintCategory() { }
        public BlueprintCategory(string guidString) : base(guidString) { }
        public string Title { get; set; }
    }
}
