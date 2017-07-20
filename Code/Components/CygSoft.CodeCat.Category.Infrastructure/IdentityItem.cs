using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category.Infrastructure
{
    [Serializable]
    public class IdentityItem
    {
        [NonSerialized]
        private Guid identifyingGuid;

        public IdentityItem()
        {
            this.identifyingGuid = Guid.NewGuid();
        }

        public IdentityItem(string guidString)
        {
            this.identifyingGuid = new Guid(guidString);
        }

        public string Id
        {
            get { return this.identifyingGuid.ToString(); }
            set { this.identifyingGuid = new Guid(value); } // if you want the GUID serialized, this must be public.
        }
    }
}
