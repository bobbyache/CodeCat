using CygSoft.CodeCat.DocumentManager.TopicSections;
using NUnit.Framework;
using static DocumentManager.UnitTests.CodeTopicSectionTests;

namespace DocumentManager.UnitTests
{
    [TestFixture]
    public class TestBench
    {


        [Test]
        public void CodeTopicSection_Revert_FiresBeforeRevertEvent()
        {
            bool eventCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.BeforeRevert += (s, e) =>
            {
                eventCalled = true;
            };
            codeTopicSection.Revert();

            Assert.That(eventCalled, Is.True, "BeforeRevert event was supposed to be called on 'CodeTopicSection' before the topic section is being reverted.");
        }

        [Test]
        public void CodeTopicSection_Revert_FiresAfterRevertEvent()
        {
            bool eventCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.AfterRevert += (s, e) =>
            {
                eventCalled = true;
            };
            codeTopicSection.Revert();

            Assert.That(eventCalled, Is.True, "AfterRevert event was supposed to be called on 'CodeTopicSection' when the topic section has been reverted.");
        }

        [Test]
        public void CodeTopicSection_Revert_BeforeRevertFiresBeforeFileIsReverted()
        {
            bool firesBeforeRevertFileCalled = false;
            TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

            codeTopicSection.BeforeRevert += (s, e) =>
            {
                firesBeforeRevertFileCalled = !codeTopicSection.OpenFileCalled;
            };
            codeTopicSection.Revert();

            Assert.That(firesBeforeRevertFileCalled, Is.True, "BeforeOpen should be called before the protected OpenFile method is executed.");
        }



        //[Test]
        //public void CodeTopicSection_Open_AfterOpenFiresAfterFileIsOpened()
        //{
        //    bool firesAfterOpenFileCalled = false;
        //    TestCodeTopicSection codeTopicSection = new TestCodeTopicSection();

        //    codeTopicSection.AfterOpen += (s, e) =>
        //    {
        //        firesAfterOpenFileCalled = codeTopicSection.OpenFileCalled;
        //    };
        //    codeTopicSection.Open();

        //    Assert.That(firesAfterOpenFileCalled, Is.True, "BeforeOpen should be called before the protected OpenFile method is executed.");
        //}
    }
}
