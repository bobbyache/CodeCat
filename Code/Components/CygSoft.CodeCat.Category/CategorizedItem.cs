using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category
{
    public class CategorizedItem : ICategorizedItem
    {
        private Guid ItemIdGuid;
        private Guid IdGuid;


        public CategorizedItem(ITitledEntity targetEntity)
        {
            this.IdGuid = Guid.NewGuid();
            this.ItemIdGuid = new Guid(targetEntity.Id);
        }

        public CategorizedItem(string id)
        {
            string[] guids = id.Split('_');

            this.IdGuid = new Guid(guids[0]);
            this.ItemIdGuid = new Guid(guids[1]);
        }

        public string Id { get { return this.IdGuid + "_" + this.ItemIdGuid; } }
        public string ItemId { get { return ItemIdGuid.ToString(); } }
        public string Title { get; set; }
        public string InstanceId { get { return this.IdGuid + "_" + this.ItemIdGuid; } }
    }
}
