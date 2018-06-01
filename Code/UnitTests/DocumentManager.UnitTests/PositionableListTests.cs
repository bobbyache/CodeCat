using CygSoft.CodeCat.DocumentManager.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DocumentManager.UnitTests
{
    [TestFixture]
    [Category("PositionableList")]
    public class PositionableListTests
    {
        public class TestPositionedItem : IPositionedItem
        {
            public int Ordinal { get; set; }
            public string Identifier { get; set; }
        }

        [Test]
        public void PositionableList_Inserts_Two_Items_Successfully()
        {
            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>();
            positionableList.Insert(new TestPositionedItem { Identifier = "Item 1" });
            positionableList.Insert(new TestPositionedItem { Identifier = "Item 2" });

            Assert.AreEqual(2, positionableList.Count);
        }

        [Test]
        public void PositionableList_Inserts_Two_Items_In_CorrectPosition()
        {
            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>();
            TestPositionedItem item1 = new TestPositionedItem { Identifier = "Item 1" };
            TestPositionedItem item2 = new TestPositionedItem { Identifier = "Item 2" };

            positionableList.Insert(item1);
            positionableList.Insert(item2);

            Assert.AreEqual(1, item1.Ordinal);
            Assert.AreEqual(2, item2.Ordinal);
        }

        [Test]
        public void PositionableList_Instantiate_With_Existing_List_Converts_Successfully()
        {
            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                new TestPositionedItem { Identifier = "Item 1" },
                new TestPositionedItem { Identifier = "Item 2" },
                new TestPositionedItem { Identifier = "Item 3" },
                new TestPositionedItem { Identifier = "Item 4" },
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);

            Assert.AreEqual(items.Count, positionableList.Count);
            Assert.AreSame(items[0], positionableList.FirstItem);
            Assert.AreSame(items[items.Count - 1], positionableList.LastItem);
        }

        [Test]
        public void PositionableList_Insert4_Move_4_Back_To_3_Has_Correct_Order()
        {
            TestPositionedItem item3 = new TestPositionedItem { Identifier = "Item 3" };
            TestPositionedItem item4 = new TestPositionedItem { Identifier = "Item 4" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                new TestPositionedItem { Identifier = "Item 1" },
                new TestPositionedItem { Identifier = "Item 2" },
                item3,
                item4,
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);
            positionableList.MoveUp(item4);

            Assert.AreEqual(3, item4.Ordinal);
            Assert.AreEqual(4, item3.Ordinal);
        }

        [Test]
        public void PositionableList_Insert4_Move_4_Back_To_3_Indexer_Works_Correctly()
        {
            TestPositionedItem item3 = new TestPositionedItem { Identifier = "Item 3" };
            TestPositionedItem item4 = new TestPositionedItem { Identifier = "Item 4" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                new TestPositionedItem { Identifier = "Item 1" },
                new TestPositionedItem { Identifier = "Item 2" },
                item3,
                item4,
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);
            positionableList.MoveUp(item4);

            Assert.AreSame(item4, positionableList[2]);
            Assert.AreSame(item3, positionableList[3]);
        }

        [Test]
        public void PositionableList_Insert4_Move_Last_Down_Remains_In_SamePosition()
        {
            TestPositionedItem item3 = new TestPositionedItem { Identifier = "Item 3" };
            TestPositionedItem item4 = new TestPositionedItem { Identifier = "Item 4" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                new TestPositionedItem { Identifier = "Item 1" },
                new TestPositionedItem { Identifier = "Item 2" },
                item3,
                item4,
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);
            positionableList.MoveDown(item4);

            Assert.AreEqual(4, item4.Ordinal);
        }

        [Test]
        public void PositionableList_Insert4_Move_First_Up_Remains_In_SamePosition()
        {
            TestPositionedItem item1 = new TestPositionedItem { Identifier = "Item 1" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                item1,
                new TestPositionedItem { Identifier = "Item 2" },
                new TestPositionedItem { Identifier = "Item 3" },
                new TestPositionedItem { Identifier = "Item 4" }
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);
            positionableList.MoveUp(item1);

            Assert.AreEqual(1, item1.Ordinal);
        }

        [Test]
        public void PositionableList_Insert4_Move_ToSpecific_Position_Successful()
        {
            TestPositionedItem item1 = new TestPositionedItem { Identifier = "Item 1" };
            TestPositionedItem item2 = new TestPositionedItem { Identifier = "Item 2" };
            TestPositionedItem item3 = new TestPositionedItem { Identifier = "Item 3" };
            TestPositionedItem item4 = new TestPositionedItem { Identifier = "Item 4" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                item1,
                item2,
                item3,
                item4,
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);
            positionableList.MoveTo(item4, 2);

            Assert.AreEqual(1, item1.Ordinal);
            Assert.AreEqual(3, item2.Ordinal);
            Assert.AreEqual(4, item3.Ordinal);
            Assert.AreEqual(2, item4.Ordinal);
        }

        [Test]
        public void PositionableList_Insert4_Remove_Successful()
        {
            TestPositionedItem item1 = new TestPositionedItem { Identifier = "Item 1" };
            TestPositionedItem item2 = new TestPositionedItem { Identifier = "Item 2" };
            TestPositionedItem item3 = new TestPositionedItem { Identifier = "Item 3" };
            TestPositionedItem item4 = new TestPositionedItem { Identifier = "Item 4" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                item1,
                item2,
                item3,
                item4,
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);
            positionableList.Remove(item2);

            Assert.AreEqual(3, positionableList.Count);
            Assert.AreEqual(1, item1.Ordinal);
            Assert.AreEqual(2, item3.Ordinal);
            Assert.AreEqual(3, item4.Ordinal);
        }

        [Test]
        public void PositionableList_CanMove_Last_Item_Behaves_Correctly()
        {
            TestPositionedItem item1 = new TestPositionedItem { Identifier = "Item 1" };
            TestPositionedItem item2 = new TestPositionedItem { Identifier = "Item 2" };
            TestPositionedItem item3 = new TestPositionedItem { Identifier = "Item 3" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                item1,
                item2,
                item3,
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);

            Assert.IsTrue(positionableList.CanMoveUp(item3));
            Assert.IsFalse(positionableList.CanMoveDown(item3));
        }

        [Test]
        public void PositionableList_CanMove_First_Item_Behaves_Correctly()
        {
            TestPositionedItem item1 = new TestPositionedItem { Identifier = "Item 1" };
            TestPositionedItem item2 = new TestPositionedItem { Identifier = "Item 2" };
            TestPositionedItem item3 = new TestPositionedItem { Identifier = "Item 3" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                item1,
                item2,
                item3,
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);

            Assert.IsFalse(positionableList.CanMoveUp(item1));
            Assert.IsTrue(positionableList.CanMoveDown(item1));
        }

        [Test]
        public void PositionableList_CanMove_Middle_Item_Behaves_Correctly()
        {
            TestPositionedItem item1 = new TestPositionedItem { Identifier = "Item 1" };
            TestPositionedItem item2 = new TestPositionedItem { Identifier = "Item 2" };
            TestPositionedItem item3 = new TestPositionedItem { Identifier = "Item 3" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                item1,
                item2,
                item3,
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);

            Assert.IsTrue(positionableList.CanMoveUp(item2));
            Assert.IsTrue(positionableList.CanMoveDown(item2));
        }

        [Test]
        public void PositionableList_ExistsInList_Finds_ExistingItem()
        {
            TestPositionedItem item1 = new TestPositionedItem { Identifier = "Item 1" };
            TestPositionedItem item2 = new TestPositionedItem { Identifier = "Item 2" };
            TestPositionedItem item3 = new TestPositionedItem { Identifier = "Item 3" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                item1,
                item2,
                item3,
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);

            Assert.IsTrue(positionableList.ExistsInList(item2));
        }

        [Test]
        public void PositionableList_ExistsInList_DoesNotFind_Item()
        {
            TestPositionedItem item1 = new TestPositionedItem { Identifier = "Item 1" };
            TestPositionedItem item2 = new TestPositionedItem { Identifier = "Item 2" };
            TestPositionedItem item3 = new TestPositionedItem { Identifier = "Item 3" };

            List<TestPositionedItem> items = new List<TestPositionedItem>
            {
                item1,
                item2,
            };

            PositionableList<TestPositionedItem> positionableList = new PositionableList<TestPositionedItem>(items);

            Assert.IsFalse(positionableList.ExistsInList(item3));
        }
    }
}
