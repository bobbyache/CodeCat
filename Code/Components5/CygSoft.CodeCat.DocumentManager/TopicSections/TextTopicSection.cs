using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System.IO;

namespace CygSoft.CodeCat.DocumentManager.TopicSections
{
    public abstract class TextTopicSection : TopicSection, ITextTopicSection
    {
        public string Text { get; set; }

        public TextTopicSection(IFileRepository fileRepository, BaseFilePathGenerator filePathGenerator, string title) : base(fileRepository, filePathGenerator, title, null)
        {
            this.Text = null;
        }

        public TextTopicSection(IFileRepository fileRepository, BaseFilePathGenerator filePathGenerator, string title, int ordinal, string description)
            : base(fileRepository, filePathGenerator, title, description, ordinal)
        {
            this.Text = null;
        }

        // Only create these documents internally.
        public TextTopicSection(IFileRepository fileRepository, string folder, string title, string extension) : base(fileRepository, new DocumentPathGenerator(folder, extension), title, null)
        {
            this.Text = null;
        }

        public TextTopicSection(IFileRepository fileRepository, string folder, string id, string title, string extension, int ordinal, string description)
            : base(fileRepository, new DocumentPathGenerator(folder, extension, id), title, description, ordinal)
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
