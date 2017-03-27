﻿using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.DocumentManager.PathGenerators;
using System;
using System.IO;

namespace CygSoft.CodeCat.DocumentManager.TopicSections
{
    public abstract class TextTopicSection : TopicSection, ITextTopicSection
    {
        public string Text { get; set; }

        // Only create these documents internally.
        internal TextTopicSection(string folder, string title, string extension) : base(new DocumentPathGenerator(folder, extension), title, null)
        {
            this.Text = null;
        }

        internal TextTopicSection(string folder, string id, string title, string extension, int ordinal, string description)
            : base(new DocumentPathGenerator(folder, extension, id), title, description, ordinal)
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
