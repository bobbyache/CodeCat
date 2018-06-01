using System;

namespace CygSoft.CodeCat.Infrastructure
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
