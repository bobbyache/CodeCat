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

        [Test]
        public void CodeTopicSection_Revert_OnRevertIsCalledTogetherWithAllEvents()
        {
            bool beforeRevertCalled = false;
            bool afterRevertCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.BeforeRevert += (s, e) =>
            {
                beforeRevertCalled = true;
            };

            codeTopicSection.AfterRevert += (s, e) =>
            {
                afterRevertCalled = true;
            };

            codeTopicSection.Revert();

            Assert.That(beforeRevertCalled, Is.True);
            Assert.That(afterRevertCalled, Is.True);
            Assert.That(codeTopicSection.OnRevertCalled, Is.True);
        }

        [Test]
        public void CodeTopicSection_Save_OnSaveIsCalledTogetherWithAllEvents()
        {
            bool beforeSaveCalled = false;
            bool afterSaveCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.BeforeSave += (s, e) =>
            {
                beforeSaveCalled = true;
            };

            codeTopicSection.AfterSave += (s, e) =>
            {
                afterSaveCalled = true;
            };

            codeTopicSection.Save();

            Assert.That(beforeSaveCalled, Is.True);
            Assert.That(afterSaveCalled, Is.True);
            Assert.That(codeTopicSection.OnSaveCalled, Is.True);
        }

        [Test]
        public void CodeTopicSection_Delete_OnDeleteIsCalledTogetherWithAllEvents()
        {
            bool beforeDeleteCalled = false;
            bool afterDeleteCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.BeforeDelete += (s, e) =>
            {
                beforeDeleteCalled = true;
            };

            codeTopicSection.AfterDelete += (s, e) =>
            {
                afterDeleteCalled = true;
            };

            codeTopicSection.Delete();

            Assert.That(beforeDeleteCalled, Is.True);
            Assert.That(afterDeleteCalled, Is.True);
            Assert.That(codeTopicSection.OnDeleteCalled, Is.True);
        }

        [Test]
        public void CodeTopicSection_Close_OnCloseIsCalledTogetherWithAllEvents()
        {
            bool beforeCloseCalled = false;
            bool afterCloseCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.BeforeClose += (s, e) =>
            {
                beforeCloseCalled = true;
            };

            codeTopicSection.AfterClose += (s, e) =>
            {
                afterCloseCalled = true;
            };

            codeTopicSection.Close();

            Assert.That(beforeCloseCalled, Is.True);
            Assert.That(afterCloseCalled, Is.True);
            Assert.That(codeTopicSection.OnCloseCalled, Is.True);
        }

        public class TestCodeTopicSection : CodeTopicSection
        {
            public bool OpenFileCalled = false;
            public bool OnRevertCalled = false;
            public bool OnSaveCalled = false;
            public bool OnDeleteCalled = false;
            public bool OnCloseCalled = false;

            public TestCodeTopicSection() : base(@"C:\TestFolder", "Test Code Section", "cs", "C#") { }

            protected override void OnDelete() { OnDeleteCalled = true; }
            protected override void OnSave() { OnSaveCalled = true; }
            protected override void OnOpen() { OpenFileCalled = true; }
            protected override void OnRevert() { OnRevertCalled = true; }

            protected override void OnClose() { OnCloseCalled = true; }
        }
    }
}
