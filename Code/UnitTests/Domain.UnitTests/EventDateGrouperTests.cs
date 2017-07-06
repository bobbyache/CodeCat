using CygSoft.CodeCat.Domain.TopicSections.SearchableEventDiary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests
{
    [TestFixture]
    [Category("EventDiary")]
    [Category("Tests.UnitTests")]
    public class EventDateGrouperTests
    {
        [Test]
        public void EventDateGrouper_WhenEventDateIsToday_ThenReturn_Today()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 3, 3, 14, 23, 1), new DateTime(2017, 3, 3, 8, 00, 59));
            Assert.AreEqual("Today", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIsYesterday_ThenReturn_Yesterday()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 3, 3, 14, 23, 1), new DateTime(2017, 3, 2, 8, 00, 59));
            Assert.AreEqual("Yesterday", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIsTomorrow_ThenReturn_Tomorrow()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 3, 3, 14, 23, 1), new DateTime(2017, 3, 4, 8, 00, 59));
            Assert.AreEqual("Tomorrow", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIsThisWeekMondayAndTodayIsThursday_ThenReturn_Monday()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 7, 6, 14, 23, 1), new DateTime(2017, 7, 3, 8, 00, 59));
            Assert.AreEqual("Monday", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIsThisWeekTuesdayAndTodayIsThursday_ThenReturn_Tuesday()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 7, 6, 14, 23, 1), new DateTime(2017, 7, 4, 8, 00, 59));
            Assert.AreEqual("Tuesday", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIsThisWeekMondayAndTodayIsSunday_ThenReturn_Monday()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 7, 9, 14, 23, 1), new DateTime(2017, 7, 3, 8, 00, 59));
            Assert.AreEqual("Monday", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIs_PreviousWeek_Return_LastWeek()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 7, 9, 14, 23, 1), new DateTime(2017, 7, 2, 8, 00, 59));
            Assert.AreEqual("Last Week", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIs_2WeeksAgo_Return_2WeeksAgo()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 7, 27, 14, 23, 1), new DateTime(2017, 7, 13, 8, 00, 59));
            Assert.AreEqual("2 Weeks Ago", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIs_3WeeksAgo_Return_3WeeksAgo()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 7, 27, 14, 23, 1), new DateTime(2017, 7, 6, 8, 00, 59));
            Assert.AreEqual("3 Weeks Ago", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIs_4WeeksAgo_Return_4WeeksAgo()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 7, 27, 14, 23, 1), new DateTime(2017, 6, 29, 8, 00, 59));
            Assert.AreEqual("4 Weeks Ago", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIs_LastMonth_Return_LastMonth()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 7, 27, 14, 23, 1), new DateTime(2017, 6, 23, 8, 00, 59));
            Assert.AreEqual("Last Month", group);
        }

        [Test]
        public void EventDateGrouper_WhenEventDateIs_OlderThanLastMonth_Return_Older()
        {
            string group = EventDateGrouper.GetGroup(new DateTime(2017, 7, 31, 14, 23, 1), new DateTime(2017, 5, 15, 8, 00, 59));
            Assert.AreEqual("Older", group);
        }
    }
}
