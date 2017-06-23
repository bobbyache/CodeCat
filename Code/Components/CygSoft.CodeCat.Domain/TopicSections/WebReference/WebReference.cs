using CygSoft.CodeCat.DocumentManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Domain.TopicSections.WebReference
{
    public class WebReference : IWebReference
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public string HostName
        {
            get
            {
                if (Uri.IsWellFormedUriString(this.Url, UriKind.Absolute))
                    return new Uri(this.Url).Host;

                return string.Empty;
            }
        }

        private Guid identifyingGuid;

        public WebReference()
        {
            this.identifyingGuid = Guid.NewGuid();
            this.DateCreated = DateTime.Now;
            this.DateModified = this.DateCreated;
        }

        public WebReference(string id, string title, string category, string description, string url, DateTime dateCreated, DateTime dateModified)
        {
            this.Title = title;
            this.Url = url;
            this.Description = description;
            this.Category = category;
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
            this.identifyingGuid = new Guid(id);
        }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }

        public string Id
        {
            get { return identifyingGuid.ToString(); }
            set { this.identifyingGuid = new Guid(value); }
        }
    }
}
