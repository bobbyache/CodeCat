using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Search.KeywordIndex.Infrastructure
{
    public abstract class PersistableObject
    {
        private Guid identifyingGuid;

        public PersistableObject()
        {
            this.identifyingGuid = Guid.NewGuid();
            this.DateCreated = DateTime.Now;
            this.DateModified = this.DateCreated;
        }

        public PersistableObject(string guidString, DateTime dateCreated, DateTime dateModified)
        {
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
            this.identifyingGuid = new Guid(guidString);
        }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }

        [Browsable(false)]
        public string Id
        {
            get { return this.identifyingGuid.ToString(); }
            set
            {
                this.identifyingGuid = new Guid(value);
            }
        }
    }
}
