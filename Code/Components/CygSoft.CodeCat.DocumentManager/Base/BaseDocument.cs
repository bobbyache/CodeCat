using CygSoft.CodeCat.DocumentManager.Infrastructure;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class BaseDocument : BaseFile, IDocument
    {
        public int Ordinal { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }

        public BaseDocument(BaseFilePathGenerator filePathGenerator, string title, string description = null)
            : base(filePathGenerator)
        {
            this.Ordinal = -1;
            this.Title = title;
            this.Description = description;
        }

        public BaseDocument(BaseFilePathGenerator filePathGenerator, string title, string description = null, int ordinal = -1)
            : base(filePathGenerator)
        {
            this.Ordinal = -1;
            this.Title = title;
            this.Description = description;
        }

        protected override void OnAfterDelete()
        {
            base.OnAfterDelete();
            this.Ordinal = -1;
        }
    }
}
