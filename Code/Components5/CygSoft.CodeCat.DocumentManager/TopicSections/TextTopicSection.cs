using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.IO;

namespace CygSoft.CodeCat.DocumentManager.TopicSections
{
    public abstract class TextTopicSection : TopicSection, ITextTopicSection
    {
        public string Text { get; set; }

        public TextTopicSection(IFileRepository fileRepository, string title) 
            : base(fileRepository, title, null)
        {
            this.Text = null;
        }

        public TextTopicSection(IFileRepository fileRepository, string title, int ordinal, string description)
            : base(fileRepository, title, description, ordinal)
        {
            this.Text = null;
        }

        protected override void OnOpen()
        {
            this.Text = File.ReadAllText(this.FilePath);
            base.OnOpen();
        }

        protected override void OnSave()
        {
            File.WriteAllText(this.FilePath, this.Text);
            base.OnSave();
        }
    }
}
