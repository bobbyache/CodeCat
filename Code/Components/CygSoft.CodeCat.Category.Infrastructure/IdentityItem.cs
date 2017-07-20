using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category.Infrastructure
{
    public class IdentityItem
    {
        private Guid identifyingGuid;

        public IdentityItem()
        {
            this.identifyingGuid = Guid.NewGuid();
        }

        public IdentityItem(string id)
        {
            this.identifyingGuid = new Guid(id);
        }

        public string Id
        {
            get { return this.identifyingGuid.ToString(); }
        }
    }
}
