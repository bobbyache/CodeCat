using CygSoft.CodeCat.DocumentManager.Base;
using CygSoft.CodeCat.DocumentManager.TopicSections;
using NUnit.Framework;

namespace DocumentManager.UnitTests
{
    [TestFixture]
    [Category("DocumentManager")]
    [Category("TopicSection.TextTopicSection")]
    [Category("Tests.UnitTests")]
    public class TextTopicSectionTests
    {
        [Test]
        public void TextTopicSection_FilePath_IsCorrectlySet()
        {
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");
            Assert.That(codeTopicSection.FilePath, Is.EqualTo(@"C:\TestFolder\filename.cs"));
        }

        [Test]
        public void TextTopicSection_FileName_IsCorrectlySet()
        {
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");
            Assert.That(codeTopicSection.FileName, Is.EqualTo("filename.cs"));
        }

        [Test]
        public void TextTopicSection_Folder_IsCorrectlySet()
        {
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");
            Assert.That(codeTopicSection.Folder, Is.EqualTo(@"C:\TestFolder"));
        }

        [Test]
        public void TextTopicSection_Id_IsCorrectlySet()
        {
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");
            Assert.That(codeTopicSection.Id, Is.EqualTo("filename"));
        }

        [Test]
        public void TextTopicSection_OnInitializationButBeforeFileOpen_IsNotLoaded()
        {
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

            Assert.That(codeTopicSection.Loaded, Is.False, "TextTopicSection.Loaded should always be false when initialized without loading a file.");
        }

        [Test]
        public void TextTopicSection_Open_IsLoaded()
        {
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");
            codeTopicSection.Open();

            Assert.That(codeTopicSection.Loaded, Is.True, "TextTopicSection.Loaded should be true after a file has been loaded.");
        }

        [Test]
        public void TextTopicSection_Open_FiresBeforeOpenEvent()
        {
            bool eventCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

            codeTopicSection.BeforeOpen += (s, e) =>
            {
                eventCalled = true;
            };
            codeTopicSection.Open();

            Assert.That(eventCalled, Is.True, "BeforeOpen event was supposed to be called on 'TextTopicSection' when the topic section is being opened.");
        }

        [Test]
        public void TextTopicSection_Open_FiresAfterOpenEvent()
        {
            bool eventCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

            codeTopicSection.AfterOpen += (s, e) =>
            {
                eventCalled = true;
            };
            codeTopicSection.Open();

            Assert.That(eventCalled, Is.True, "AfterOpen event was supposed to be called on 'TextTopicSection' when the topic section is being opened.");
        }

        [Test]
        public void TextTopicSection_Open_BeforeOpenFiresBeforeFileIsOpened()
        {
            bool firesBeforeOpenFileCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

            codeTopicSection.BeforeOpen += (s, e) =>
            {
                firesBeforeOpenFileCalled = !codeTopicSection.OpenFileCalled;
            };
            codeTopicSection.Open();

            Assert.That(firesBeforeOpenFileCalled, Is.True, "BeforeOpen should be called before the protected OpenFile method is executed.");
        }

        [Test]
        public void TextTopicSection_Open_AfterOpenFiresAfterFileIsOpened()
        {
            bool firesAfterOpenFileCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

            codeTopicSection.AfterOpen += (s, e) =>
            {
                firesAfterOpenFileCalled = codeTopicSection.OpenFileCalled;
            };
            codeTopicSection.Open();

            Assert.That(firesAfterOpenFileCalled, Is.True, "BeforeOpen should be called before the protected OpenFile method is executed.");
        }

        [Test]
        public void TextTopicSection_Revert_OnRevertIsCalledTogetherWithAllEvents()
        {
            bool beforeRevertCalled = false;
            bool afterRevertCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

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
        public void TextTopicSection_Revert_FiresBeforeRevertEvent()
        {
            bool eventCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

            codeTopicSection.BeforeRevert += (s, e) =>
            {
                eventCalled = true;
            };
            codeTopicSection.Revert();

            Assert.That(eventCalled, Is.True, "BeforeRevert event was supposed to be called on 'TextTopicSection' before the topic section is being reverted.");
        }

        [Test]
        public void TextTopicSection_Revert_FiresAfterRevertEvent()
        {
            bool eventCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

            codeTopicSection.AfterRevert += (s, e) =>
            {
                eventCalled = true;
            };
            codeTopicSection.Revert();

            Assert.That(eventCalled, Is.True, "AfterRevert event was supposed to be called on 'TextTopicSection' when the topic section has been reverted.");
        }

        [Test]
        public void TextTopicSection_Revert_BeforeRevertFiresBeforeFileIsReverted()
        {
            bool firesBeforeRevertFileCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

            codeTopicSection.BeforeRevert += (s, e) =>
            {
                firesBeforeRevertFileCalled = !codeTopicSection.OpenFileCalled;
            };
            codeTopicSection.Revert();

            Assert.That(firesBeforeRevertFileCalled, Is.True, "BeforeOpen should be called before the protected OpenFile method is executed.");
        }

        [Test]
        public void TextTopicSection_Save_OnSaveIsCalledTogetherWithAllEvents()
        {
            bool beforeSaveCalled = false;
            bool afterSaveCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

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
        public void TextTopicSection_Delete_OnDeleteIsCalledTogetherWithAllEvents()
        {
            bool beforeDeleteCalled = false;
            bool afterDeleteCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

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
        public void TextTopicSection_Close_OnCloseIsCalledTogetherWithAllEvents()
        {
            bool beforeCloseCalled = false;
            bool afterCloseCalled = false;
            TestTextTopicSection codeTopicSection = new TestTextTopicSection(GetStubBaseFilePathGenerator(), "Test TextTopicSection");

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

        public class StubFilePathGenerator : BaseFilePathGenerator
        {
            public override string FileExtension => "cs";

            public override string FileName => "filename.cs";

            public override string FilePath => @"C:\TestFolder\filename.cs";

            public override string Id => "filename";
        }

        private BaseFilePathGenerator GetStubBaseFilePathGenerator()
        {
            return new StubFilePathGenerator();
        }

        public class TestTextTopicSection : TextTopicSection
        {
            public bool OpenFileCalled = false;
            public bool OnRevertCalled = false;
            public bool OnSaveCalled = false;
            public bool OnDeleteCalled = false;
            public bool OnCloseCalled = false;

            public TestTextTopicSection(BaseFilePathGenerator filePathGenerator, string title) : base(new FileRepository(filePathGenerator), filePathGenerator, title)
            {

            }

            public TestTextTopicSection(BaseFilePathGenerator filePathGenerator, string title, int ordinal, string description) 
                : base(new FileRepository(filePathGenerator), filePathGenerator, title, ordinal, description)
            {

            }

            protected override void OnDelete() { OnDeleteCalled = true; }
            protected override void OnSave() { OnSaveCalled = true; }
            protected override void OnOpen() { OpenFileCalled = true; }
            protected override void OnRevert() { OnRevertCalled = true; }

            protected override void OnClose() { OnCloseCalled = true; }
        }
    }
}
