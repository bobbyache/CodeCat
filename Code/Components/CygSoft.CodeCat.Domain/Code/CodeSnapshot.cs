using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using System;

namespace CygSoft.CodeCat.Domain.Code
{
    public class CodeSnapshot : PersistableObject
    {
        public CodeSnapshot(string text) : base()
        {
            this.Text = text;
            this.Description = string.Format("Snapshot: {0} {1}", this.DateCreated.ToLongDateString(), this.DateCreated.ToLongTimeString());
        }

        public CodeSnapshot(string id, string description, string text, DateTime dateCreated, DateTime dateModified) : base(id, dateCreated, dateModified)
        {
            this.Text = text;
            this.Description = description;
        }

        public string Description { get; set; }
        public string Text { get; internal set; }
    }
}
