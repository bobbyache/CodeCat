using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ExportCodeCatWebReferences
{
    public class WebReference
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

        public WebReference(string id, string title, string category, string description, string url, DateTime dateCreated, DateTime dateModified)
        {
            this.Title = title;
            this.Url = url;
            this.Description = description;
            this.Category = category;
            this.DateCreated = dateCreated;
            this.DateModified = dateModified;
            this.identifyingGuid = new Guid(id);

            if (TryGetSeconds(url, out int seconds))
            {
                var timeDisplay = TimeFuncs.DisplayTimeFromSeconds(seconds);

                this.Title = $"{timeDisplay} {title}";
            }
        }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }

        public string Id
        {
            get { return identifyingGuid.ToString(); }
            set { this.identifyingGuid = new Guid(value); }
        }

        private bool TryGetSeconds(string url, out int seconds)
        {
            var uri = new Uri(url);
            var result = uri.DecodeQueryParameters().Where(p => p.Key == "t");

            if (result.Count() == 1)
            {
                seconds = 0;
                seconds = int.Parse(result.Select(p => p.Value).Single());
                return true;
            }
            else
            {
                seconds = 0;
                return false;
            }
        }
    }
}