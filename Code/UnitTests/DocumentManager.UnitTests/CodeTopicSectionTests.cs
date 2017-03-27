using CygSoft.CodeCat.DocumentManager.TopicSections;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.UnitTests
{
    [TestFixture]
    public class CodeTopicSectionTests
    {
        [Test]
        public void CodeTopicSection_OnInitializationButBeforeFileOpen_IsNotLoaded()
        {
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            Assert.That(codeTopicSection.Loaded, Is.False, "CodeTopicSection.Loaded should always be false when initialized without loading a file.");
        }

        [Test]
        public void CodeTopicSection_Open_IsLoaded()
        {
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();
            codeTopicSection.Open();

            Assert.That(codeTopicSection.Loaded, Is.True, "CodeTopicSection.Loaded should be true after a file has been loaded.");
        }

        [Test]
        public void CodeTopicSection_Open_FiresBeforeOpenEvent()
        {
            bool eventCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.BeforeOpen += (s, e) =>
            {
                eventCalled = true;
            };
            codeTopicSection.Open();

            Assert.That(eventCalled, Is.True, "BeforeOpen event was supposed to be called on 'CodeTopicSection' when the topic section is being opened.");
        }

        [Test]
        public void CodeTopicSection_Open_FiresAfterOpenEvent()
        {
            bool eventCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.AfterOpen += (s, e) =>
            {
                eventCalled = true;
            };
            codeTopicSection.Open();

            Assert.That(eventCalled, Is.True, "AfterOpen event was supposed to be called on 'CodeTopicSection' when the topic section is being opened.");
        }

        [Test]
        public void CodeTopicSection_Open_BeforeOpenFiresBeforeFileIsOpened()
        {
            bool firesBeforeOpenFileCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.BeforeOpen += (s, e) =>
            {
                firesBeforeOpenFileCalled = !codeTopicSection.OpenFileCalled;
            };
            codeTopicSection.Open();

            Assert.That(firesBeforeOpenFileCalled, Is.True, "BeforeOpen should be called before the protected OpenFile method is executed.");
        }

        [Test]
        public void CodeTopicSection_Open_AfterOpenFiresAfterFileIsOpened()
        {
            bool firesAfterOpenFileCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.AfterOpen += (s, e) =>
            {
                firesAfterOpenFileCalled = codeTopicSection.OpenFileCalled;
            };
            codeTopicSection.Open();

            Assert.That(firesAfterOpenFileCalled, Is.True, "BeforeOpen should be called before the protected OpenFile method is executed.");
        }

        public class TestCodeTopicSection : CodeTopicSection
        {
            public bool OpenFileCalled = false;

            public TestCodeTopicSection() : base(@"C:\TestFolder", "Test Code Section", "cs", "C#") { }

            protected override void DeleteFile() { }
            protected override void SaveFile() { }
            protected override void OpenFile() { OpenFileCalled = true; }
        }
    }
}
