using CygSoft.CodeCat.Plugin.Infrastructure;

namespace CygSoft.CodeCat.DocumentManager.Base
{
    public abstract class TopicSection : BaseFile, ITopicSection
    {
        public string Id {  get { return filePathGenerator.Id; } }
        public int Ordinal { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }

        public TopicSection(BaseFilePathGenerator filePathGenerator, string title, string description = null) 
            : base(filePathGenerator)
        {
            Ordinal = -1;
            Title = title;
            Description = description;
        }

        public TopicSection(BaseFilePathGenerator filePathGenerator, string title, string description = null, int ordinal = -1) 
            : base(filePathGenerator)
        {
            Ordinal = -1;
            Title = title;
            Description = description;
        }

        protected override void OnAfterDelete()
        {
            Ordinal = -1;
        }
    }
}
