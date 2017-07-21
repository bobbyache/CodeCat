using CygSoft.CodeCat.Category;
using CygSoft.CodeCat.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category.UnitTests
{
    [TestFixture]
    public class TestBench
    {
        [Test]
        public void CategorizedItem_WhenInitializedFromPersistenceStore_ParsesIds_Correctly()
        {
            CategorizedItem categorizedItem = new CategorizedItem("77ba2ad2-1727-409a-8824-dea9a9b9c11d_652405d8-6820-4cc9-bc81-635a4d444b54");
            Assert.AreEqual("77ba2ad2-1727-409a-8824-dea9a9b9c11d", categorizedItem.Id);
            Assert.AreEqual("652405d8-6820-4cc9-bc81-635a4d444b54", categorizedItem.ItemId);
            Assert.AreEqual("77ba2ad2-1727-409a-8824-dea9a9b9c11d_652405d8-6820-4cc9-bc81-635a4d444b54", categorizedItem.InstanceId);
        }

        [Test]
        public void CategorizedItem_WhenInitializedAsNew_CreatesIdsCorrectly()
        {
            ITitledEntity testEntity = new TestItem() { Title = "Title" };
            CategorizedItem categorizedItem = new CategorizedItem(testEntity);

            Guid guid = new Guid(categorizedItem.Id);
            Assert.AreEqual("652405d8-6820-4cc9-bc81-635a4d444b54", categorizedItem.ItemId);
            Assert.AreEqual($"{guid.ToString()}_652405d8-6820-4cc9-bc81-635a4d444b54", categorizedItem.InstanceId);
        }

        public class TestItem : ITitledEntity
        {
            public string Id { get { return "652405d8-6820-4cc9-bc81-635a4d444b54"; } }
            public string Title { get; set; }
        }
    }
}
