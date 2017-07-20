using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category
{
    public class BlueprintHeader : IdentityItem, ITitledEntity
    {
        public BlueprintHeader() { }
        public BlueprintHeader(string guidString) : base(guidString) { }

        public string Title { get; set; }
    }
}
